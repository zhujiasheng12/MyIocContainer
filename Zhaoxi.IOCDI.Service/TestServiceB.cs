using System;
using System.Security.Cryptography.X509Certificates;
using Zhaoxi.IOCDI.Framework.CustomContainer;
using Zhaoxi.IOCDI.Interface;

namespace Zhaoxi.IOCDI.Service
{
    public class TestServiceB : ITestServiceB
    {

        /// <summary>
        /// 希望属性也能初始化，属性注入
        /// </summary>
        [ZhaoxiPropertyInjectAttribute]
        public ITestServiceD TestServiceD { get; set; }

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="testServiceA"></param>
        public TestServiceB(ITestServiceA testServiceA)
        {

            
            Console.WriteLine($"{this.GetType().Name} 被构造。。。！");
        }

        private ITestServiceE _iTestServiceE = null;
        /// <summary>
        /// 方法注入
        /// </summary>
        /// <param name="testServiceE"></param>
        [ZhaoxiMethodAttribute]
        public void Init(ITestServiceE testServiceE)
        {
            _iTestServiceE = testServiceE;
            Console.WriteLine($"this is {testServiceE.GetType ().Name}被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine($"this is TestServiceB B123456");
        }
    }
}
