using System;
using Zhaoxi.IOCDI.Framework.CustomAOP;

namespace Zhaoxi.IOCDI.Interface
{
    public interface ITestServiceB
    {
        [LogBefore]
        [LogAfter]
        [Login]
        [Monitor]
        void Show();
    }
}
