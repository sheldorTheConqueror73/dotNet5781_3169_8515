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
            id = "";
            password = "";
            name = "";
            accessLevel = Clearance.None;
        }
        public User(string id,string password string name, Clearance access)
        {
            id = "";
            password = "";
            name = "";
            accessLevel = Clearance.None;
        }

    }
}
