using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomAOP
{
    /// <summary>
    /// 普通类
    /// </summary>
    public class CommonClass
    {
        public virtual void MethodInterceptor()
        {
            Console.WriteLine("This is Interceptor");
        }
        public void MethodNoInterceptor()
        {
            Console.WriteLine("This is without Interceptor");
        }
    }
}
