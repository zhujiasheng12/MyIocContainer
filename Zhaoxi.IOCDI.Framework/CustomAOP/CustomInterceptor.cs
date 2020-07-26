using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomAOP
{
    public class CustomInterceptor:StandardInterceptor
    {

        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用前的拦截器，方法名是：{invocation .Method .Name}");
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine($"拦截的方法返回时调用的拦截器，方法名是：{invocation.Method.Name}");
            base.PerformProceed(invocation);
        }

        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用后的拦截器，方法名是：{invocation.Method.Name}");
        }
    }
}
