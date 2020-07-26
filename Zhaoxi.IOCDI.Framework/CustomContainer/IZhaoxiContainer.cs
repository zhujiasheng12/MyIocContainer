using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    public  interface IZhaoxiContainer
    {
        public void Register<TFrom, TTo>(string shortName = null,object[] paraList=null, LifetimeType lifetimeType = LifetimeType.Transient) where TTo :TFrom;

        public TFrom Resolve<TFrom>(string shortName = null);
        IZhaoxiContainer CreateChildContainer();
    }
}
