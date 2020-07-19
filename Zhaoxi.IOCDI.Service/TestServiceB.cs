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

        public int _iIndex { get; set; }
        /// <summary>
        /// 构造函数注入，有参数
        /// </summary>
        /// <param name="testServiceA"></param>
        [ZhaoxiConstructor]
        public TestServiceB(ITestServiceA testServiceA1, [ZhaoxiParameterConstant] string sIndex,ITestServiceA testServiceA2,[ZhaoxiParameterConstant]int iIndex)
        {
            _iIndex = iIndex;
            Console.WriteLine($"{this.GetType().Name} 被构造。。。！参数是：{sIndex}  {iIndex}");
        }
        /// <summary>
        /// 构造函数参数顺序不同
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="testServiceA"></param>
        public TestServiceB( [ZhaoxiParameterConstant]int iIndex, ITestServiceA testServiceA)
        {
            _iIndex = iIndex;
            Console.WriteLine($"{this.GetType().Name} 被构造。。。！参数是：{iIndex}");
        }

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="testServiceA"></param>
        //[ZhaoxiConstructor]
        public TestServiceB(int iIndex)
        {
            _iIndex = iIndex;

            Console.WriteLine($"{this.GetType().Name} 被构造。。。！参数为：{iIndex}");
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
