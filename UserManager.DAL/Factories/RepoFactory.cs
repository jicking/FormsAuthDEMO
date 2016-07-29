using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.DAL.Abstracts;
using UserManager.DAL.Repo;

namespace UserManager.DAL.Factories
{
    public class RepoFactory
    {
        public static IUserRepo CreateUserRepo()
        {
            return new UserRepo();
        }
    }
}
