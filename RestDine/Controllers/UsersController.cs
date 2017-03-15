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
using RestDineLib;
using ED = RestDineLib;
using MD = RestDine.Models;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Core.Objects;
using System.Web.Http.Cors;

namespace RestDine.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private FastFoodFinderEntities2 db = new FastFoodFinderEntities2();

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult>GetFavorite([FromUri] String Hash, [FromUri]String username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Security.GetHashString(username).Equals(Hash))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var result = from x in db.Users
                         where x.Email == username
                         select x;
            if(result.ToArray()[0].Restaurants.ToArray().Count() > 0)
            {
                return Ok(result.ToArray()[0].Restaurants);
            }
            else
            {
                return NotFound();
            }
            

        }
        [HttpPut]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> insertFavorite([FromUri]String Hash,[FromUri]String Username,[FromUri]int id, [FromUri] long X, [FromUri] long Y)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!Security.GetHashString(Username).Equals(Hash))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var restInt = from restInteger in db.Locations
                           where restInteger.LongX == X & restInteger.LongY == Y
                           select restInteger.ID;
            
            ObjectParameter output = new ObjectParameter("OutputParameterName", typeof(int));
            db.AddUserFavorite(id, restInt.First(),output);
            await db.SaveChangesAsync();
            if(output.Value.Equals( 1))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return StatusCode(HttpStatusCode.Conflict);
            }
        }
        [HttpPut]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> upDateUser([FromUri]String Hash,[FromUri] int id,[FromUri]String username, MD.User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Security.GetHashString(username).Equals(Hash))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            ED.User use = ConvertModelToEntity.ConvertNewToUser(user);
            ED.User newUser = db.Users.SingleOrDefault(u => u.ID == use.ID);
            newUser.Password = use.Password;
            newUser.Email = use.Email;
            newUser.Creditcard = use.Creditcard;
            newUser.Name = use.Name;
            db.Entry(newUser).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
       }
        // GET: api/Users
      
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> createUser(MD.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = from u in db.Users
                      where u.Email.ToLower().Equals(user.email.ToLower())
                      select u;
            if(res.ToArray().Length > 0)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }
            ED.User newuser = ConvertModelToEntity.ConvertNewToUser(user);
            db.Users.Add(newuser);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.Created);
        }
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetInfo([FromUri]String hash,[FromUri]String username)
        {
            if(Security.GetHashString(username) != hash)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = from tempuser in db.Users
                       where tempuser.Email == username
                       select tempuser;

            return Content(HttpStatusCode.OK, user);
        }
        [HttpPut]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult>UpdateLastLocation([FromUri]String hash,[FromUri]String username,[FromUri]long x, [FromUri]long y)
        {
            if (Security.GetHashString(username) != hash)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = from tempuser in db.Users
                       where tempuser.Email == username
                       select tempuser;
            user.First().Locations.First().LongX = x;
            user.First().Locations.First().LongY = y;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Content(HttpStatusCode.OK, user);
        }
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Login([FromUri]String username,[FromUri]String password)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = from tempuser in db.Users
                        where tempuser.Email == username
                        && tempuser.Password == password
                        select tempuser;
            if (users.ToList().Count > 0 )
            {
             return Content(HttpStatusCode.OK,Security.GetHashString(username));
            }
            else
            {
             return StatusCode(HttpStatusCode.NotFound);
            }

        }

        /*
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
    /*    [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }*/

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}