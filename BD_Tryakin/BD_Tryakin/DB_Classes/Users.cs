using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BD_Tryakin.DB_Classes
{
    public class Users
    {
        public Users() { Products = new HashSet<Products>(); }
        public Users(string login_user, string password_user, string name_user, string surname_user, string address_user)
        {
            Login_User = login_user;
            Password_User = password_user;
            Name_User = name_user;
            Surname_User = surname_user;
            Address_User = address_user;
            IsAdmin_User = false;
        }

        [Key]
        public string Login_User { get; set; }

        public string Password_User { get; set; }
        public string Name_User { get; set; }
        public string Surname_User { get; set; }
        public string Address_User { get; set; }
        public bool IsAdmin_User { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
