using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.DAL.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string LastName { get; set; }


        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

        public string ValidationCode { get; set; }

    }
}
