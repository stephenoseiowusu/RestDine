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
            return Ok(newBrand.Name);
        }
        // POST: api/Restaurants
        [ResponseType(typeof(MD.Restaurant))]
        public async Task<IHttpActionResult> PostRestaurant(MD.Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurant);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = restaurant.ID }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [ResponseType(typeof(MD.Restaurant))]
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
        }

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