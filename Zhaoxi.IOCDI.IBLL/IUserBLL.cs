using System;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.IBLL
{
    public interface IUserBLL
    {
        UserModel Login(string account);

        void LastLogin(UserModel user);
    }
}
