﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    /// <summary>
    /// 标记属性注入
    /// </summary>
    [AttributeUsage (AttributeTargets.Property)]//标记属性注入
    public class ZhaoxiPropertyInjectAttribute : Attribute
    {
    }
}
