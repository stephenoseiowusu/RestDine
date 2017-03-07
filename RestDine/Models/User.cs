using RestDineLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDine.Models
{
    public class User
    {
        public int ID { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String name { get; set; }
        public String creditcard { get; set; }
        public Location Loc { get; set; }
        public User(String email, String password, String name, long X, long Y, String creditcard = "0000000000000000", int ID = 0)
        {
            this.ID = ID;
            this.email = email;
            this.password = password;
            this.name = name;
            this.creditcard = creditcard;
            this.Loc = new Location();
            this.Loc.LongX = X;
            this.Loc.LongY = Y;
        }
    }
}