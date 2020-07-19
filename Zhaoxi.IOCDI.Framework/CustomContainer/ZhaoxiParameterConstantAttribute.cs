using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    /// <summary>
    /// 这个是常量参数
    /// </summary>
    [AttributeUsage (AttributeTargets.Parameter)]//标记参数注入
    public class ZhaoxiParameterConstantAttribute : Attribute
    {
    }
}
