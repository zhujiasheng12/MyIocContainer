using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Zhaoxi.IOCDI.IDAL;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.DAL
{
    public class UserDALMySql:IUserDAL
    {
        public UserModel Find(Expression<Func<UserModel, bool>> expression)
        {
            return new UserModel()
            {
                Id = 7,
                Account = "Administrator",
                Name = "Eleven-MySql",
                Email = "57265177@qq.com",
                Password = "123456677",
                Role = "Admin",
                LoginTime = DateTime.Now
            };
        }


        public void Updata(UserModel userModel)
        {
            Console.WriteLine("MySql数据库更新！");
        }

    }
}
