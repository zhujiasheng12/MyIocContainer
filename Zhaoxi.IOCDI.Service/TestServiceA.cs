using System;
using Zhaoxi.IOCDI.Interface;

namespace Zhaoxi.IOCDI.Service
{
    public class TestServiceA : ITestServiceA
    {
        public TestServiceA()
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine($"this is TestServiceA A123456");
        }
    }
}
