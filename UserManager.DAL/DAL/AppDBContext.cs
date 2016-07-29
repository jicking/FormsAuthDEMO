using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.DAL.Models;

namespace UserManager.DAL.DAL
{
    class AppDBContext:DbContext
    {
        public AppDBContext():base("name=DefaultConnection")
        {

        }

        public DbSet<UserAccount> Users { get; set; }
    }
}
