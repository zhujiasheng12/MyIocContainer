using System;
using Zhaoxi.IOCDI.Framework.CustomContainer;
using Zhaoxi.IOCDI.IBLL;
using Zhaoxi.IOCDI.IDAL;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.BLL
{
    public class UserBLL : IUserBLL
    {
        private IUserDAL _iUserDal = null;

        [ZhaoxiPropertyInject]
        public IUserDAL UserDAL { get; set; }

        public IUserDAL UserDALMySql { get; set; }

        [ZhaoxiPropertyInject]
        [ZhaoxiParameterShortName("MySql")]
        public IUserDAL UserDALMySql2 { get; set; }

        public UserBLL([ZhaoxiParameterShortName("MySql")]IUserDAL userDAL)
        {
            this.UserDALMySql = userDAL;
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
