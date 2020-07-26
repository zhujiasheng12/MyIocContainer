using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    public class ZhaoxiContainer:IZhaoxiContainer
    {

        private Dictionary<string, ZhaoxiContainerRegistModel> ZhaoxiContainerDictionary = new Dictionary<string, ZhaoxiContainerRegistModel>();
        private Dictionary<string, object[]> ZhaoxiContainerValueDictionary = new Dictionary<string, object[]>();

        private Dictionary<string, object> ZhaoxiContainerScopeDictionary = new Dictionary<string, object>();

        private string GetKey(string fullName,string shortName) => $"{fullName}___{shortName}";

        public IZhaoxiContainer CreateChildContainer()
        {
            return new ZhaoxiContainer(this.ZhaoxiContainerDictionary, this.ZhaoxiContainerValueDictionary,new Dictionary<string, object> ());
        }
        public ZhaoxiContainer() { }

        private ZhaoxiContainer(Dictionary<string, ZhaoxiContainerRegistModel> zhaoxiContainerDictionary, Dictionary<string, object[]> zhaoxiContainerValueDictionary, Dictionary<string, object> zhaoxiContainerScopeDictionary)
        {
            this.ZhaoxiContainerDictionary = zhaoxiContainerDictionary;
            this.ZhaoxiContainerValueDictionary = zhaoxiContainerValueDictionary;
            this.ZhaoxiContainerScopeDictionary = zhaoxiContainerScopeDictionary;
        }
        /// <summary>
        /// 加个参数区分生命周期--而且注册关系得保存生命周期
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="shortName"></param>
        /// <param name="paraList"></param>
        public void Register<TFrom, TTo>(string shortName = null, object[] paraList = null,LifetimeType lifetimeType =LifetimeType.Transient) where TTo : TFrom
        {
            this.ZhaoxiContainerDictionary.Add(this.GetKey(typeof (TFrom).FullName,shortName), new ZhaoxiContainerRegistModel() { 
            Lifetime =lifetimeType ,
            TargetType =typeof (TTo)
            });
            if(paraList !=null&&paraList .Length >0)
                this.ZhaoxiContainerValueDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), paraList);
        }

        /// <summary>
        /// 递归--可以完成不限层级的东西
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <returns></returns>
        public TFrom Resolve<TFrom>(string shortName = null)
        {
            return (TFrom)this.ResolveObject(typeof(TFrom),shortName);
        }

        /// <summary>
        /// 递归--可以完成不限层级的东西
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <returns></returns>
        private object ResolveObject(Type abstractType, string shortName = null)
        {
            string key = this.GetKey(abstractType .FullName,shortName);
            var model= this.ZhaoxiContainerDictionary[key];
            #region 检测生命周期
            switch (model.Lifetime)
            {
                case LifetimeType.Transient:
                    Console.WriteLine("Transient Do Nothing Before...");
                    break;
                case LifetimeType.Singleton:
                    if (model.SingletonInstance == null)
                    {
                        break;
                    }
                    else
                    {
                        return model.SingletonInstance;
                    }
                case LifetimeType.Scope:
                    if (this.ZhaoxiContainerScopeDictionary.ContainsKey(key))
                    {
                        return this.ZhaoxiContainerScopeDictionary[key];
                    }
                    else
                    {
                        break;
                    }
                case LifetimeType.PerThread:
                    object oValue = CustomCallContext<object>.GetData($"{key}{Thread.CurrentThread.ManagedThreadId}");
                    if (oValue == null)
                    {
                        break;
                    }
                    else
                    {
                        return oValue;
                    }
                default:
                    break;
            }
            #endregion
            Type type = model.TargetType;

            #region 选择合适的构造函数
            ConstructorInfo ctor = null;
            
            //2.选择特性标记的，如果没有则去选择参数最多的
            ctor = type.GetConstructors().FirstOrDefault(c => c.IsDefined(typeof(ZhaoxiConstructorAttribute),true));
            if (ctor == null)
            {
                //1.参数个数最多的
                ctor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();//参数最多的
            }
            #endregion


            #region 准备构造函数的参数
            List<object> paraList = new List<object>();

            object[] paraConstant = this.ZhaoxiContainerValueDictionary.ContainsKey (key) ? this.ZhaoxiContainerValueDictionary[key]:null;

            int iIndex = 0;
            foreach (var para in ctor.GetParameters())//获取构造函数的参数
            {
                if (para.IsDefined(typeof(ZhaoxiParameterConstantAttribute), true))
                {
                    paraList.Add(paraConstant[iIndex]);
                    iIndex++;
                }
                else
                {
                    Type paraType = para.ParameterType;//获取参数的类型 项目中是
                    string paraShortName = this.GetShortName(para);

                    object paraInstance = this.ResolveObject(paraType,paraShortName);
                    paraList.Add(paraInstance);//创建目标类型实例
                }
            }

            #endregion
            object oInstance = Activator.CreateInstance(type, paraList.ToArray());
            
            #region 属性注入
            foreach (var prop in type.GetProperties().Where (p=>p.IsDefined (typeof(ZhaoxiPropertyInjectAttribute),true)))
            {
                Type propType = prop.PropertyType;

                string propShortName = this.GetShortName(prop);

                object propInstance = this.ResolveObject(propType,propShortName);
                prop.SetValue(oInstance, propInstance);
            }
            #endregion

            #region 方法注入
            foreach (var meth in type.GetMethods().Where(m => m.IsDefined(typeof(ZhaoxiMethodAttribute), true)))
            {
                List<object> methParaList = new List<object>();

                //object[] methParaConstant = this.ZhaoxiContainerValueDictionary.ContainsKey(key) ? this.ZhaoxiContainerValueDictionary[key] : null;

                foreach (var methPara in meth.GetParameters())//获取函数的参数
                {
                    //if (methPara.IsDefined(typeof(ZhaoxiParameterConstantAttribute), true))
                    //{
                    //    methParaList.Add(paraConstant[iIndex]);
                    //    iIndex++;
                    //}
                    //else
                    {
                        Type methParaType = methPara.ParameterType;//获取参数的类型 
                        string paraShortName = this.GetShortName(methPara);
                        object methParaInstance = this.ResolveObject(methParaType, paraShortName);
                        methParaList.Add(methParaInstance);//创建目标类型实例
                    }
                }
                meth.Invoke(oInstance, methParaList.ToArray());

            }
            #endregion

            #region Lifetime
            switch (model.Lifetime)
            {
                case LifetimeType.Transient:
                    Console.WriteLine("Transient Do Nothing After...");
                    break;
                case LifetimeType.Singleton:
                    model.SingletonInstance = oInstance;
                    break;
                case LifetimeType.Scope:
                    this.ZhaoxiContainerScopeDictionary[key] = oInstance;
                    break;
                case LifetimeType.PerThread:
                    CustomCallContext<object>.SetData($"{key}{Thread.CurrentThread .ManagedThreadId}", oInstance);
                    break;
                default:
                    break;
            }
            #endregion
            return oInstance;
        }


        private string GetShortName(ICustomAttributeProvider  provider)
        {
            if (provider.IsDefined(typeof(ZhaoxiParameterShortNameAttribute), true))
            {
                var attribute =(ZhaoxiParameterShortNameAttribute) (provider.GetCustomAttributes(typeof(ZhaoxiParameterShortNameAttribute), true)[0]);
                return attribute.ShortName;
            }
            else
                return null;
        }

    }
}
