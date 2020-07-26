using System;
using Zhaoxi.IOCDI.Framework.CustomAOP;

namespace Zhaoxi.IOCDI.Interface
{
    public interface ITestServiceA
    {
        [LogBefore]
        [LogAfter]
        [Login]
        [Monitor]
        void Show();

        void Show1();
    }
}
