using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA = RestDineLib;
using MD = RestDine.Models;
using ED = RestDineLib;

namespace RestDine.Controllers
{
    public class ConvertModelToEntity
    {

        private static ED.FastFoodFinderEntities2 db = new ED.FastFoodFinderEntities2();

        public static DA.User ConvertNewToUser(MD.User temp)
        {
            DA.User tempo = new DA.User();
            tempo.Name = temp.name;
            tempo.Password = temp.password;
            // tempo.ID = temp.ID;
            tempo.Email = temp.email;
            tempo.Creditcard = temp.creditcard;
            tempo.Locations.Add(temp.Loc);
            return tempo;
        }
        public static MD.User ConvertUserBack(DA.User user)
        {
            MD.User use;
            if ( user.Locations.Count > 0)
            {
               use  = new MD.User(user.Email, user.Password, user.Name, (long)user.Locations.Last().LongX, (long)user.Locations.Last().LongY, user.Creditcard, user.ID);
            }
            else
            {
                use = new MD.User(user.Email, user.Password, user.Name, 0, 0, user.Creditcard, user.ID);
            }
           
            return use;
        }
        public static List<MD.Restaurant> convert(ICollection<DA.Restaurant> a)
        {
            List<MD.Restaurant> temp = new List<MD.Restaurant>();
            foreach (DA.Restaurant b in a)
            {
              /*  var xa = from x in db.Restaurants
                         where x.ID == b.ID
                         select x.Brand_ID;
                var xd = from x in db.Brands
                         where x.ID == xa.ToArray()[0]
                         select x.Name;*/
                MD.Restaurant temps = new MD.Restaurant(b.Location_ID, b.Brand_ID, b.ID/*, xd.ToArray()[0].ToString()*/);
                temp.Add(temps);
            }
            return temp;
        }

        public static DA.Brand ConvertBrandToBrand(MD.Brand temp)
        {
            DA.Brand brand = new DA.Brand();
            brand.Description = temp.description;
            brand.Name = temp.BrandName;
            return brand;
        }
        public static DA.Restaurant ConvertRestaurantToRestaurant(MD.Restaurant temp)
        {
            DA.Restaurant rest = new DA.Restaurant();
            rest.Location_ID = temp.Location_ID;
            rest.Brand_ID = temp.Brand_ID;
            return rest;
        }
    }
}