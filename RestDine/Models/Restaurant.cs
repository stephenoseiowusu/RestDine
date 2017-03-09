using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDine.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public int Location_ID { get; set; }
        public int Brand_ID { get; set; }
        public Restaurant ( int Location_ID, int Brand_ID, int id)
        {
            this.ID = id;
            this.Location_ID = Location_ID;
            this.Brand_ID = Brand_ID;
        }
    }
}