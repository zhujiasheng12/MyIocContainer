using System;
using System.Linq.Expressions;
using Zhaoxi.IOCDI.Framework.CustomContainer;
using Zhaoxi.IOCDI.IDAL;
using Zhaoxi.IOCDI.Interface;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.DAL
{
    public class UserDAL : IUserDAL
    {
        private ITestServiceB _iTestServiceB = null;
        private ITestServiceA _iTestServiceA = null;
        private ITestServiceC _iTestServiceC = null;
        private ITestServiceD _iTestServiceD = null;
        private ITestServiceE _iTestServiceE = null;
        [ZhaoxiConstructor]
        public UserDAL(ITestServiceA testServiceA, ITestServiceB testServiceB)
        {
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
        }

        public UserDAL(ITestServiceA testServiceA, ITestServiceB testServiceB, ITestServiceC testServiceC, ITestServiceD testServiceD, ITestServiceE testServiceE)
        {
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
            this._iTestServiceC = testServiceC;
            this._iTestServiceD = testServiceD;
            this._iTestServiceE = testServiceE;
        }

        public UserDAL()
        {
            this._iTestServiceA = null;
            this._iTestServiceB = null;
        }

        public UserModel Find(Expression<Func<UserModel, bool>> expression)
        {
            return new UserModel()
            {
                Id = 7,
                Account = "Administrator",
                Name = "Eleven",
                Email = "57265177@qq.com",
                Password = "123456677",
                Role = "Admin",
                LoginTime = DateTime.Now
            };
        }

        public void Updata(UserModel userModel)
        {
            Console.WriteLine("SqlServer数据库更新！");
        }
    }
}
