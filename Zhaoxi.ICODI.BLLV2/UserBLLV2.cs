using System;
using Zhaoxi.IOCDI.IBLL;
using Zhaoxi.IOCDI.IDAL;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.BLLV2
{
    public class UserBLLV2:IUserBLL
    {
        private IUserDAL _iUserDal = null;
        public UserBLLV2(IUserDAL userDAL)
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
            return this._iUserDal.Find(u => u.Account.Equals(account));
        }
    }
}
