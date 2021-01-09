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
        public string accessLevel { get; set; }
        public bool enabled { get; set; }
        public string fullname { get; set; }
        public string mail { get; set; }
        public User()
        {
            password = "";
            name = "";
            accessLevel ="None";
            fullname = "";
            mail = "";
            enabled = true;
        }
    }
}
