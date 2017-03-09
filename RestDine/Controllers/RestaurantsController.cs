using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ED = RestDineLib;
using MD = RestDine.Models;

namespace RestDine.Controllers
{
    public class RestaurantsController : ApiController
    {
        private ED.FastFoodFinderEntities1 db = new ED.FastFoodFinderEntities1();

        // GET: api/Restaurants
        public IQueryable<ED.Restaurant> GetRestaurants()
        {
            return db.Restaurants;
        }

        // GET: api/Restaurants/5
        [ResponseType(typeof(MD.Restaurant))]
        public async Task<IHttpActionResult> GetRestaurant(int id)
        {
            ED.Restaurant restaurant = await db.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Restaurants/5
       /* [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRestaurant(int id, MD.Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.ID)
            {
                return BadRequest();
            }

            db.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        } */
        [HttpGet]
        public async Task<IHttpActionResult> GetBrandID([FromUri]String BrandName)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = from tempresult in db.Brands
                         where tempresult.Name == BrandName
                         select tempresult;
            if (result.ToList().Count > 0)
            {
                return Ok(result.First().ID);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Location([FromUri] long X, [FromUri]long Y,[FromUri] String UnitNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ED.Location locate = new ED.Location();
            locate.LongX = X;
            locate.LongY = Y;
            locate.Unit_Number = UnitNumber;
            db.Locations.Add(locate);
            await db.SaveChangesAsync();
            var result = from tempresult in db.Locations
                         where tempresult.LongX == X && tempresult.LongY == Y
                         select tempresult;
            return Ok(result.ToArray()[0].ID);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Location([FromUri] long X, [FromUri]long Y)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ED.Location locate = new ED.Location();
            locate.LongX = X;
            locate.LongY = Y;
            db.Locations.Add(locate);
            await db.SaveChangesAsync();
            var result = from tempresult in db.Locations
                         where tempresult.LongX == X && tempresult.LongY == Y
                         select tempresult;
            return Ok(result.ToArray()[0].ID);
        }
        [ResponseType(typeof(MD.Brand))]
        public async Task<IHttpActionResult> PostBrand(MD.Brand Tempbrand)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = from tempresult in db.Brands
                         where tempresult.Name == Tempbrand.BrandName
                         select tempresult;
            if(result.ToList().Count > 0)
            {
                return Ok();
            }

            ED.Brand newBrand = ConvertModelToEntity.ConvertBrandToBrand(Tempbrand);
            db.Brands.Add(newBrand);
            await db.SaveChangesAsync(); 
            return Created("GetBrandID",newBrand.Name);
        }
        // POST: api/Restaurants
        [HttpPost]
        [ResponseType(typeof(MD.Restaurant))]
        public async Task<IHttpActionResult> PostRestaurant(MD.Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ED.Restaurant rest = ConvertModelToEntity.ConvertRestaurantToRestaurant(restaurant);
            db.Restaurants.Add(rest);
            await db.SaveChangesAsync();
            return Created("GetRestaurant",rest.Brand_ID);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRestaurant([FromUri]String restname)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = from x in db.Restaurants
                          where x.Brand.Name == restname
                          select x;

           // ED.Restaurant rest = ConvertModelToEntity.ConvertRestaurantToRestaurant(results);
          
            return Ok();
        }

        // DELETE: api/Restaurants/5
        /*   [ResponseType(typeof(MD.Restaurant))]
           public async Task<IHttpActionResult> DeleteRestaurant(int id)
           {
               Restaurant restaurant = await db.Restaurants.FindAsync(id);
               if (restaurant == null)
               {
                   return NotFound();
               }

               db.Restaurants.Remove(restaurant);
               await db.SaveChangesAsync();

               return Ok(restaurant);
           }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantExists(int id)
        {
            return db.Restaurants.Count(e => e.ID == id) > 0;
        }
    }
}