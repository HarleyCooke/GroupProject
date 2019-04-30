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
using BSAPI.Models;

namespace BSAPI.Controllers
{
    public class UsersController : ApiController
    {
        private ballstatsEntities db = new ballstatsEntities();

        // GET: api/Users
        public IQueryable<USER> GetUSERS()
        {
            return db.USERS;
        }

        // GET: api/Users/5
        [ResponseType(typeof(USER))]
        public async Task<IHttpActionResult> GetUSER(string id)
        {
            USER uSER = await db.USERS.FindAsync(id);
            if (uSER == null)
            {
                return NotFound();
            }

            return Ok(uSER);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSER(string id, USER uSER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSER.UserName)
            {
                return BadRequest();
            }

            db.Entry(uSER).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USERExists(id))
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
        [ResponseType(typeof(USER))]
        public async Task<IHttpActionResult> PostUSER(USER uSER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //DO THE HASH, SALT BUT HOLD THE PEPPER

            db.USERS.Add(uSER);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (USERExists(uSER.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uSER.UserName }, uSER);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(USER))]
        public async Task<IHttpActionResult> DeleteUSER(string id)
        {
            USER uSER = await db.USERS.FindAsync(id);
            if (uSER == null)
            {
                return NotFound();
            }

            db.USERS.Remove(uSER);
            await db.SaveChangesAsync();

            return Ok(uSER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERExists(string id)
        {
            return db.USERS.Count(e => e.UserName == id) > 0;
        }
    }
}