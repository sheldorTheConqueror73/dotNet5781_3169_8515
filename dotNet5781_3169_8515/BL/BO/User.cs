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
        public Clearance accessLevel { get; set; }
        public bool enabled { get; set; }

        public User()
        {
            password = "";
            name = "";
            accessLevel = Clearance.None;
            enabled = true;
        }
        public User(string password, string name, Clearance access)
        {
            this.password = password;
            this.name = name;
            accessLevel = access;
            enabled = true;
        }

    }
}
