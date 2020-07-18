using System;
using System.Linq.Expressions;
using Zhaoxi.IOCDI.Model;

namespace Zhaoxi.IOCDI.IDAL
{
    public interface IUserDAL
    {
        UserModel Find(Expression<Func<UserModel, bool>> expression);

        void Updata(UserModel userModel);
    }
}
