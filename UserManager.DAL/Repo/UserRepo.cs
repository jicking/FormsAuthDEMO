using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.DAL.Abstracts;
using UserManager.DAL.DAL;
using UserManager.DAL.Models;

namespace UserManager.DAL.Repo
{
    public class UserRepo : IUserRepo
    {
        AppDBContext db;

        public UserRepo()
        {
            db = new AppDBContext();
        }

        public UserAccount Get(string email, string password)
        {
            try
            {
                UserAccount user = db.Users.Where(q => q.Email.ToLower() == email.ToLower() && q.Password == password).FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckIfEmailExists(string email)
        {
            try
            {
                UserAccount user = db.Users.Where(q => q.Email.ToLower() == email.ToLower()).FirstOrDefault();
                if (user == null)
                    return false;

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<UserAccount> GetAll()
        {
            try
            {
                IEnumerable<UserAccount> users = db.Users.ToList();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Add(UserAccount user)
        {
            try
            {
                int newId = 0;

                //SET DEFAULT PROPERIES
                user.Id = 0;
                user.ValidationCode = GenerateRandomString(6);

                db.Users.Add(user);
                db.SaveChanges();
                newId = user.Id;
                return newId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserAccount user)
        {
            throw new NotImplementedException();
        }

        #region HELPERS
        private static Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        } 
        #endregion
    }
}
