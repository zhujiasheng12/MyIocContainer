using System;
using Zhaoxi.IOCDI.BLL;
using Zhaoxi.IOCDI.DAL;
using Zhaoxi.IOCDI.Framework;
using Zhaoxi.IOCDI.Framework.CustomContainer;
using Zhaoxi.IOCDI.IBLL;
using Zhaoxi.IOCDI.IDAL;
using Zhaoxi.IOCDI.Interface;
using Zhaoxi.IOCDI.Model;
using Zhaoxi.IOCDI.Service;

namespace Zhaoxi.IOCDI.ConsoleProject
{
    /// <summary>
    /// why how when what
    /// 
    /// why IOC--DIP--该DIP
    /// 
    /// 贯彻到底，从头到尾
    /// 
    /// 通过贯彻DIP，做到了依赖抽象而不是依赖细节，代码也能跑起来
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("****************** this is zhujiasheng12 的体验IOC实例 ******************");
                #region 0217
                {
                    ////DIP--抽象不能实例化--也不能依赖细节--第三方
                    //IUserDAL userDAL = ObjectFactory.CreateDal();
                    //IUserBLL userBLL = ObjectFactory.CreateBLL(userDAL);
                    //UserModel model = userBLL.Login("Administrator");
                    //Console.WriteLine(model.Name);

                }
                #endregion
                #region #0218
                {
                    ////需求是上层仅依赖与抽象，就能完成对象的获取---需要写一个第三方工具---工厂就是做这个的
                    ////常规IOC容器：（第三方--业务无关）容器对象--注册--生成
                    //IZhaoxiContainer container = new ZhaoxiContainer();
                    //container.Register<IUserDAL, UserDAL>();
                    //container.Register<IUserBLL, UserBLL>();
                    //container.Register<ITestServiceA, TestServiceA>();
                    //container.Register<ITestServiceB, TestServiceB>();
                    //container.Register<ITestServiceC, TestServiceC>();
                    //container.Register<ITestServiceD, TestServiceD>();
                    //container.Register<ITestServiceE, TestServiceE>();


                    //ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
                    ////IUserDAL userDAL = container.Resolve<IUserDAL>();
                    ////IUserBLL userBLL = container.Resolve<IUserBLL>();
                    ////为啥依赖抽象，而不是依赖细节--依赖倒置原则，
                }
                #endregion 
                {
                    //多参数     递归解决

                    //多构造函数(AspNet Core使用超集,即构造函数中有一个构造函数包含其它所有构造函数参数，不然就是报错)商用容器一般使用参数个数最多的
                    //多构造数  特性标记与选择最大集解决 


                    //其它注入方式    属性注入  方法注入

                    //单接口多实现    
                    //1.注册的时候加个名称name，保存到字典,Resolve时传入name识别

                    //如果实现类的构造函数是int类型，如3  怎么办呢
                    //1.修改类--重载构造方法--不好,破坏封装
                    //2.无侵入，注册时保存个参数，Resolve时使用下

                    //为啥不用自带的呢？   只支持构造函数注入
                }

                #region 0219
                {
                    //
                    IZhaoxiContainer container = new ZhaoxiContainer();
                    container.Register<IUserDAL, UserDAL>();
                    container.Register<IUserDAL, UserDALMySql>("MySql");//单接口多实现，实质就是保存不同的key
                    container.Register<IUserBLL, UserBLL>();
                    container.Register<ITestServiceA, TestServiceA>();
                    //container.Register<ITestServiceB, TestServiceB>();
                    container.Register<ITestServiceB, TestServiceB>(paraList:new object[] {"jack", 3});

                    container.Register<ITestServiceC, TestServiceC>();
                    container.Register<ITestServiceD, TestServiceD>();
                    container.Register<ITestServiceE, TestServiceE>();
                    //ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
                    //IUserDAL userDAL = container.Resolve<IUserDAL>();
                    //IUserDAL userDALMySql = container.Resolve<IUserDAL>("MySql");
                    IUserBLL userBLL = container.Resolve<IUserBLL>();
                }
                #endregion
                Console.WriteLine("****************** this is zhujiasheng12 的体验IOC实例  End ******************");

            }
            catch (Exception ex)
            {
                Console.WriteLine("****************** this is zhujiasheng 的体验IOC实例  Error ******************");
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
