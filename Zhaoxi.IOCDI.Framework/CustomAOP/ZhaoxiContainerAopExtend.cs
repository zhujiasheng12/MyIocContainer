using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomAOP
{
    /// <summary>
    /// 为ZhaoxiContainer做个AOP扩展//接口注入，interface全部方法
    /// </summary>
    public static class ZhaoxiContainerAopExtend
    {
        public static object AOP(this object t,Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();
            IOCInterceptor interceptor = new IOCInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfaceType, t, interceptor);
            return t;
        }
    }


    #region attribute Interceptor

    public abstract  class BaseInterceptorAttribute : Attribute
    {
        public abstract Action Do(IInvocation invocation, Action action);
        
    }
    public class LogBeforeAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LogBeforeAttribute Start  {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                //写个日志--参数检查--能做的事已经很多了
                action.Invoke();
                Console.WriteLine($"This is LogBeforeAttribute End  {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            };
        }
    }

    public class LogAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LogAfterAttribute Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                action.Invoke();
                Console.WriteLine($"This is LogAfterAttribute End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            };
        }
    }

    public class MonitorAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Stopwatch stopwatch = new Stopwatch();
                Console.WriteLine($"This is MonitorAttribute  Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                stopwatch.Start();
                //真实逻辑
                action.Invoke();
                stopwatch.Stop();
                Console.WriteLine($"本次方法花费时间：{stopwatch.ElapsedMilliseconds }ms");
                Console.WriteLine($"This is MonitorAttribute  End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");

            };
        }
    }

    public class LoginAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LoginAttribute  Start {DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss fff")}");
                action.Invoke();
                Console.WriteLine($"This is LoginAttribute  End {DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss fff")}");
            };
        }
    }
    #endregion

    /// <summary>
    /// 切面逻辑--写死，不能满足灵活需求
    /// </summary>
    public class IOCInterceptor : StandardInterceptor
    {

        protected override void PreProceed(IInvocation invocation)
        {
            //Console.WriteLine($"调用前的拦截器，方法名是：{invocation.Method.Name}");
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            var method = invocation.Method;
            Action action = () => base.PerformProceed(invocation);

            if (method.IsDefined(typeof(BaseInterceptorAttribute), true))
            {
                foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>().ToArray ().Reverse())
                {
                    action = attribute.Do(invocation,action);
                }
                
            }
            //那就说明前面不能执行具体动作--前面只能是组装动作--配置管道模型--委托
            //base.PerformProceed(invocation);//真实逻辑
            action.Invoke();
            //1.能解决哪些方法不用AOP的问题
            //2.可以把切面逻辑转移到特性里面去

            //Console.WriteLine($"拦截的方法返回时调用的拦截器，方法名是：{invocation.Method.Name}");
            //base.PerformProceed(invocation);
        }

        protected override void PostProceed(IInvocation invocation)
        {
            //Console.WriteLine($"调用后的拦截器，方法名是：{invocation.Method.Name}");
        }
    }
}
