using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    /// <summary>
    /// 标记方法注入
    /// </summary>
    [AttributeUsage (AttributeTargets.Method)]//标记方法注入
    public class ZhaoxiMethodAttribute : Attribute
    {
    }
}
