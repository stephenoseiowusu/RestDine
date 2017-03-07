using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDine.Models
{
    public class Users
    {
        public int ID { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String name { get; set; }
        public String creditcard { get; set; }
        public Users(int ID, String email, String password, String name, String creditcard = "0000000000000000")
        {
            this.ID = ID;
            this.email = email;
            this.password = password;
            this.name = name;
            this.creditcard = creditcard;
        }
    }
}