using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace Zhaoxi.IOCDI.Framework
{
    public class ObjectFactory
    {
        //public static  IUserDAL CreateDal()
        //{
        //    IUserDAL userDAL = null;
        //    //不能依赖细节，但是又要创建对象
        //    string config = ConfigurationManager.GetNode("IUserDAL");

        //    Assembly assembly = Assembly.Load(config.Split (',')[1]);
        //    Type type = assembly.GetType(config.Split(',')[0]);
        //    userDAL = (IUserDAL)Activator.CreateInstance(type);
        //    return userDAL;
        //}
        ////如果实现没有引用DLL,而是copyDLL,load失败

        //public static IUserBLL CreateBLL(IUserDAL userDAL)
        //{
        //    IUserBLL userBLL= null;
        //    //不能依赖细节，但是又要创建对象
        //    string config = ConfigurationManager.GetNode("IUserBLL");

        //    Assembly assembly = Assembly.Load(config.Split(',')[1]);
        //    Type type = assembly.GetType(config.Split(',')[0]);
        //    userBLL = (IUserBLL)Activator.CreateInstance(type,new object[] { userDAL});
        //    return userBLL;
        //}
    }
}
