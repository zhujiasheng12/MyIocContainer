using System;
using System.Threading;
using System.Threading.Tasks;
using Zhaoxi.IOCDI.BLL;
using Zhaoxi.IOCDI.DAL;
using Zhaoxi.IOCDI.Framework;
using Zhaoxi.IOCDI.Framework.CustomAOP;
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
                    //IZhaoxiContainer container = new ZhaoxiContainer();
                    //container.Register<IUserDAL, UserDAL>();
                    //container.Register<IUserDAL, UserDALMySql>("MySql");//单接口多实现，实质就是保存不同的key
                    //container.Register<IUserBLL, UserBLL>();
                    //container.Register<ITestServiceA, TestServiceA>();
                    ////container.Register<ITestServiceB, TestServiceB>();
                    //container.Register<ITestServiceB, TestServiceB>(paraList:new object[] {"jack", 3});

                    //container.Register<ITestServiceC, TestServiceC>();
                    //container.Register<ITestServiceD, TestServiceD>();
                    //container.Register<ITestServiceE, TestServiceE>();
                    ////ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
                    ////IUserDAL userDAL = container.Resolve<IUserDAL>();
                    ////IUserDAL userDALMySql = container.Resolve<IUserDAL>("MySql");
                    //IUserBLL userBLL = container.Resolve<IUserBLL>();
                }
                #endregion


                #region 0224
                {
                    //手写IOC容器--支持构造函数注入，属性注入，方法注入，---无限极注入---单接口多实现---指定参数值构造
                    //加点料，对象都是我创建的，加点对象生命周期管理--是否重用对象
                    IZhaoxiContainer container = new ZhaoxiContainer();
                    //1,瞬时（最基本的）。
                    {
                        //瞬时
                        //container.Register<ITestServiceA, TestServiceA>(lifetimeType :LifetimeType .Transient);
                        //ITestServiceA a1 = container.Resolve<ITestServiceA>();
                        //ITestServiceA a2 = container.Resolve<ITestServiceA>();
                        //Console.WriteLine(object.ReferenceEquals(a1, a2));//F的原因是每次都在重新构造
                    }

                    //2，单例。
                    {
                        //container.Register<ITestServiceA, TestServiceA>(lifetimeType: LifetimeType.Singleton);
                        //ITestServiceA a1 = container.Resolve<ITestServiceA>();
                        //ITestServiceA a2 = container.Resolve<ITestServiceA>();
                        //Console.WriteLine(object.ReferenceEquals(a1, a2));//F的原因是每次都在重新构造
                    }
                    //3，作用域单例  就是Http请求时，一个请求处理过程中，创建得都是同一个实例，不同得请求处理过程中，创建得就是不同得实例
                    {
                        //得区分请求，HTTP请求---AspNet.Core内置kestrel，初始化一个容器实例，然后每次来一个很Http请i去，就clone一个，或者叫创建一个子容器{包含注册关系}，然后一个请求就一个子容器实例，那么就可以做到请求单例了（其实就是子容器单例）。
                        //container.Register<ITestServiceA, TestServiceA>(lifetimeType: LifetimeType.Scope);
                        //ITestServiceA a1 = container.Resolve<ITestServiceA>();
                        //ITestServiceA a2 = container.Resolve<ITestServiceA>();

                        //IZhaoxiContainer container1 = container.CreateChildContainer();
                        //ITestServiceA a11 = container1.Resolve<ITestServiceA>();
                        //ITestServiceA a12 = container1.Resolve<ITestServiceA>();

                        //IZhaoxiContainer container2 = container.CreateChildContainer();
                        //ITestServiceA a21 = container2.Resolve<ITestServiceA>();
                        //ITestServiceA a22 = container2.Resolve<ITestServiceA>();
                        //Console.WriteLine(object.ReferenceEquals(a1, a2));//F的原因是每次都在重新构造
                        //Console.WriteLine(object.ReferenceEquals(a11, a12));//F的原因是每次都在重新构造
                        //Console.WriteLine(object.ReferenceEquals(a21, a22));//F的原因是每次都在重新构造


                        //Console.WriteLine(object.ReferenceEquals(a11, a21));//F的原因是每次都在重新构造
                        //Console.WriteLine(object.ReferenceEquals(a11, a22));//F的原因是每次都在重新构造
                        //Console.WriteLine(object.ReferenceEquals(a12, a21));//F的原因是每次都在重新构造
                        //Console.WriteLine(object.ReferenceEquals(a12, a22));//F的原因是每次都在重新构造
                    }

                    {
                        //container.Register<ITestServiceA, TestServiceA>(lifetimeType: LifetimeType.PerThread);
                        //ITestServiceA a1 = container.Resolve<ITestServiceA>();
                        //ITestServiceA a2 = container.Resolve<ITestServiceA>();
                        //ITestServiceA a3 = null;
                        //ITestServiceA a4 = null;
                        //ITestServiceA a5 = null;
                        //Task.Run(() =>
                        //{
                        //    Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId} a3");
                        //     a3 = container.Resolve<ITestServiceA>();
                        //});

                        //Task.Run(() =>
                        //{
                        //    Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId} a4");
                        //    a4 = container.Resolve<ITestServiceA>();
                        //}).ContinueWith (t=> {
                        //    Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId} a5");
                        //    a5 = container.Resolve<ITestServiceA>();
                        //});
                        //Thread.Sleep(1000);
                        //Console.WriteLine(object.ReferenceEquals(a1, a2));
                        //Console.WriteLine(object.ReferenceEquals(a1, a3));
                        //Console.WriteLine(object.ReferenceEquals(a1, a4));
                        //Console.WriteLine(object.ReferenceEquals(a1, a5));

                        //Console.WriteLine(object.ReferenceEquals(a3, a4));
                        //Console.WriteLine(object.ReferenceEquals(a3, a5));

                        //Console.WriteLine(object.ReferenceEquals(a4, a5));
                    }

                }
                #endregion


                #region 0225
                {
                    //IOC+AOP
                    //CustomAOPTest.Show();

                    //interface with target--帮你AOP一下
                    //IOC容器，可以基于抽象完成对象的实例化
                    IZhaoxiContainer container = new ZhaoxiContainer();
                    container.Register<ITestServiceA, TestServiceA>(lifetimeType :LifetimeType.Singleton);
                    container.Register<ITestServiceB, TestServiceB>(paraList: new object[] { "jack", 3 }, lifetimeType: LifetimeType.Singleton);

                    container.Register<ITestServiceC, TestServiceC>(lifetimeType: LifetimeType.Singleton);
                    container.Register<ITestServiceD, TestServiceD>(lifetimeType: LifetimeType.Singleton);
                    container.Register<ITestServiceE, TestServiceE>(lifetimeType: LifetimeType.Singleton);
                    ITestServiceA a1 = container.Resolve<ITestServiceA>();
                    a1.Show();
                    a1.Show1();
                    ITestServiceB b1 = container.Resolve<ITestServiceB>();
                    b1.Show();

                    //a1 = (ITestServiceA)a1.AOP(typeof(ITestServiceA));
                    //a1.Show();
                    //a1.Show1();
                    //现在有的工具是1，  1生2，2生3，3生万物
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
