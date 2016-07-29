using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.DAL.Models;

namespace UserManager.DAL.Abstracts
{
    public interface IUserRepo
    {
        UserAccount Get(string email, string password);
        IEnumerable<UserAccount> GetAll();

        bool CheckIfEmailExists(string email);


        int Add(UserAccount user);
        void Update(UserAccount user);
        void Delete(int id);
    }
}
