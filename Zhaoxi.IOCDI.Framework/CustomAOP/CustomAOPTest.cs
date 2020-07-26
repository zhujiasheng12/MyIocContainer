using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;


namespace Zhaoxi.IOCDI.Framework.CustomAOP
{
    public class CustomAOPTest
    {
        public static void Show()
        {
            ProxyGenerator generator = new ProxyGenerator();
            CustomInterceptor interceptor = new CustomInterceptor();
            CommonClass test = generator.CreateClassProxy<CommonClass>(interceptor);//类型注入--必须是virtual虚方法
            Console.WriteLine("当前类型：{0}，父类型：{1}",test.GetType (),test.GetType ().BaseType );
            Console.WriteLine();
            test.MethodInterceptor();
            Console.WriteLine();
            test.MethodNoInterceptor();
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
