using System;
using Zhaoxi.IOCDI.IBLL;
using Zhaoxi.IOCDI.IDAL;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.BLL
{
    public class UserBLL : IUserBLL
    {
        private IUserDAL _iUserDal = null;
        public UserBLL(IUserDAL userDAL)
        {
            this._iUserDal = userDAL;
        }

        public void LastLogin(UserModel user)
        {
            user.LoginTime = DateTime.Now;
            this._iUserDal.Updata(user);
        }

        public UserModel Login(string account)
        {
            return this._iUserDal.Find(u=>u.Account .Equals (account));
        }
    }
}
