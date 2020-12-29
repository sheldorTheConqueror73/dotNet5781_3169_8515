using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User :DOobject
    {
        public string password { get; set; }
        public string name { get; set; }
        public Clearance accessLevel { get; set; }
        public bool enabled { get; set; }
        public string fullname { get; set; }
        public string mail { get; set; }
        public User()
        {
            password = "";
            name = "";
            accessLevel = Clearance.None;
            fullname = "";
            mail = "";
            enabled = true;
        }
        public User(string password, string name, Clearance access, string fullName, String mail)
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
