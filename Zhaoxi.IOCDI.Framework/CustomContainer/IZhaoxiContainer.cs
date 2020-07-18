﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    public  interface IZhaoxiContainer
    {
        public void Register<TFrom, TTo>() where TTo :TFrom;

        public TFrom Resolve<TFrom>();
    }
}