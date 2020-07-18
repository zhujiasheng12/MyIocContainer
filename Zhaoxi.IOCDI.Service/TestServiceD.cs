using System;
using Zhaoxi.IOCDI.Framework.CustomContainer;
using Zhaoxi.IOCDI.Interface;

namespace Zhaoxi.IOCDI.Service
{
    public class TestServiceD : ITestServiceD
    {

        public TestServiceD(int d)
        {
            Console.WriteLine($"this is T{this.GetType().Name} 被构造，参数是：{d.ToString ()}");
        }
        /// <summary>
        /// 标记个特性，1.修改类--重载构造方法--不好,破坏封装
        /// </summary>
        [ZhaoxiConstructor]
        public TestServiceD():this(3)
        {
            Console.WriteLine($"this is T{this.GetType().Name} 被构造,无参数");
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
