using System;
using Zhaoxi.IOCDI.Interface;

namespace Zhaoxi.IOCDI.Service
{
    public class TestServiceA : ITestServiceA
    {
        public void Show()
        {
            Console.WriteLine($"this is TestServiceA A123456");
        }
    }
}
