using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Zhaoxi.IOCDI.Framework
{
    /// <summary>
    /// 固定读取根目录下的appsettings.json
    /// </summary>
    public class ConfigurationManager
    {
        private static IConfigurationRoot _iConfiguration;
        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
            _iConfiguration = builder.Build();
        }
        public static string GetNode(string nodeName)
        {
            return _iConfiguration[nodeName];
        }
    }
}
