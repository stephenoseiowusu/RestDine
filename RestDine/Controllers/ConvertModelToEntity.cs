using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA = RestDineLib;
using MD = RestDine.Models;
namespace RestDine.Controllers
{
    public class ConvertModelToEntity
    {
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
            MD.User use = new MD.User(user.Creditcard, user.Password, user.Name, (long)user.Locations.Last().LongX, (long)user.Locations.Last().LongY, user.Creditcard);
            return use;
        }
        public static List<MD.Restaurant> convert(ICollection<DA.Restaurant> a)
        {
            List<MD.Restaurant> temp = new List<MD.Restaurant>();
            foreach (DA.Restaurant b in a)
            {
                MD.Restaurant temps = new MD.Restaurant(b.Location_ID, b.Brand_ID, b.ID);
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