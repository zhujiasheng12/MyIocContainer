using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    /// <summary>
    /// 标记构造函数注入
    /// </summary>
    [AttributeUsage (AttributeTargets.Constructor)]//标记构造函数注入
    public class ZhaoxiConstructorAttribute:Attribute
    {
    }
}
