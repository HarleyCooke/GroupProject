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
    public class UserTeamsController : ApiController
    {
        private ballstatsEntities db = new ballstatsEntities();

        // GET: api/UserTeams
        public IQueryable<USERTEAM> GetUSERTEAM()
        {
            return db.USERTEAM;
        }

        // GET: api/UserTeams/5
        [ResponseType(typeof(USERTEAM))]
        public async Task<IHttpActionResult> GetUSERTEAM(int id)
        {
            USERTEAM userteam = await db.USERTEAM.FindAsync(id);
            if (userteam == null)
            {
                return NotFound();
            }

            return Ok(userteam);
        }

        // PUT: api/UserTeams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSERTEAM(int id, USERTEAM uSERTEAM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSERTEAM.ID)
            {
                return BadRequest();
            }

            db.Entry(uSERTEAM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USERTEAMExists(id))
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

        // POST: api/UserTeams
        [ResponseType(typeof(USERTEAM))]
        public async Task<IHttpActionResult> PostUSERTEAM(USERTEAM uSERTEAM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USERTEAM.Add(uSERTEAM);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSERTEAM.ID }, uSERTEAM);
        }

        // DELETE: api/UserTeams/5
        [ResponseType(typeof(USERTEAM))]
        public async Task<IHttpActionResult> DeleteUSERTEAM(int id)
        {
            USERTEAM uSERTEAM = await db.USERTEAM.FindAsync(id);
            if (uSERTEAM == null)
            {
                return NotFound();
            }

            db.USERTEAM.Remove(uSERTEAM);
            await db.SaveChangesAsync();

            return Ok(uSERTEAM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERTEAMExists(int id)
        {
            return db.USERTEAM.Count(e => e.ID == id) > 0;
        }
    }
}