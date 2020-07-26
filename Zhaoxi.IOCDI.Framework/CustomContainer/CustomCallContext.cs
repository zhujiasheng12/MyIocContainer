using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Zhaoxi.IOCDI.Framework.CustomContainer
{
    public class CustomCallContext<T>
    {
        public static ConcurrentDictionary<string, AsyncLocal<T>> CallContextData = new ConcurrentDictionary<string, AsyncLocal<T>>();
        /// <summary>
        /// 添加线程数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public static void SetData(string name, T data)
        {
            CallContextData.GetOrAdd(name, o => new AsyncLocal<T>()).Value = data;
        }
        /// <summary>
        /// 获取线程数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetData(string name)
        {
            return CallContextData.TryGetValue(name, out AsyncLocal<T> data) ? data.Value : default(T);
        }
    }
}
