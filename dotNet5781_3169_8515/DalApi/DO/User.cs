using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        public string id { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public Clearance accessLevel { get; set; }
        public User()
        {
            id = "";
            password = "";
            name = "";
            accessLevel = Clearance.None;
        }

    }
}
