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
        public  static DA.User ConvertNewToUser(MD.User temp)
        {
            DA.User tempo = new  DA.User();
            tempo.Name = temp.name;
           // tempo.ID = temp.ID;
            tempo.Email = temp.email;
            tempo.Creditcard = temp.creditcard;
            tempo.Locations.Add(temp.Loc);
            return tempo;
        } 
    }
}