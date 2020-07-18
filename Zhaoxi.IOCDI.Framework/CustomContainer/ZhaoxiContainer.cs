using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    public class ZhaoxiContainer:IZhaoxiContainer
    {

        private Dictionary<string, Type> ZhaoxiContainerDictionary = new Dictionary<string, Type>();

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            this.ZhaoxiContainerDictionary.Add(typeof(TFrom).FullName, typeof(TTo));
        }

        /// <summary>
        /// 递归--可以完成不限层级的东西
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <returns></returns>
        public TFrom Resolve<TFrom>()
        {
            return (TFrom)this.ResolveObject(typeof(TFrom));
        }


        /// <summary>
        /// 递归--可以完成不限层级的东西
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <returns></returns>
        private object ResolveObject(Type abstractType)
        {
            string key = abstractType.FullName;
            Type type = this.ZhaoxiContainerDictionary[key];

            #region 选择合适的构造函数
            ConstructorInfo ctor = null;
            
            //2.选择特性标记的，如果没有则去选择参数最多的
            ctor = type.GetConstructors().FirstOrDefault(c => c.IsDefined(typeof(ZhaoxiConstructorAttribute),true));
            if (ctor == null)
            {
                //1.参数个数最多的
                ctor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();//参数最多的
            }
           // ctor = type.GetConstructors()[0];//获取构造函数//直接第一个
            #endregion


            #region 准备构造函数的参数
            List<object> paraList = new List<object>();
            foreach (var para in ctor.GetParameters())//获取构造函数的参数
            {
                Type paraType = para.ParameterType;//获取参数的类型 项目中是IUserDAL
                object paraInstance = this.ResolveObject(paraType);
                //string paraKey = paraType.FullName;//IUserDAL的完整名称
                //Type paraTargetType = this.ZhaoxiContainerDictionary[paraKey];//IUserDAL的目标类型UserDAL名称
                //object paraInstance = Activator.CreateInstance(paraTargetType);
                paraList.Add(paraInstance);//创建目标类型实例UserDAL
            }
            #endregion
            object oInstance = Activator.CreateInstance(type, paraList.ToArray());

            #region 属性注入
            foreach (var prop in type.GetProperties().Where (p=>p.IsDefined (typeof(ZhaoxiPropertyInjectAttribute),true)))
            {
                Type propType = prop.PropertyType;
                object propInstance = this.ResolveObject(propType);
                prop.SetValue(oInstance, propInstance);
            }
            #endregion

            #region 方法注入

            foreach (var meth in type.GetMethods().Where(p => p.IsDefined(typeof(ZhaoxiMethodAttribute), true)))
            {
                List<object> methParaList = new List<object>();
                foreach (var methPara in meth.GetParameters())//获取构造函数的参数
                {
                    Type methParaType = methPara.ParameterType;//获取参数的类型 项目中是IUserDAL
                    object methParaInstance = this.ResolveObject(methParaType);
                    methParaList.Add(methParaInstance);//创建目标类型实例UserDAL
                }
                meth.Invoke(oInstance, methParaList.ToArray());

            }
            #endregion
            return oInstance;
        }
    }
}
