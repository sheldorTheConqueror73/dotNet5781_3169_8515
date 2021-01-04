using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User : BOobject
    {
        public string password { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
        public bool enabled { get; set; }

        public string fullname { get; set; }
        public string mail { get; set; }
        public User()
        {
            password = "";
            name = "";
            accessLevel = "User";
            fullname = "";
            mail = "";
            enabled = true;
        }
        public User(string password, string name, string access,string fullName,String mail)
        {
            this.password = password;
            this.name = name;
            accessLevel = access;
            enabled = true;
            this.fullname = fullName;
            this.mail = mail;
        }

    }
}
