using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDine.Models
{
    public class Brand
    {
        public String BrandName { get; set; }
        public String description { get; set; }
        public int ID { get; set; }
        public Brand(String name, String description, int ID = 0)
        {
            this.BrandName = name;
            this.description = description;
            this.ID = ID;
        }
    }
}