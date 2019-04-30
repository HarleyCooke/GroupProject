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
    public class PlayersController : ApiController
    {
        private ballstatsEntities db = new ballstatsEntities();

        // GET: api/Players
        public IQueryable<PLAYER> GetPLAYER()
        {
            return db.PLAYER;
        }

        // GET: api/Players/5
        [ResponseType(typeof(PLAYER))]
        public async Task<IHttpActionResult> GetPLAYER(int id)
        {
            PLAYER pLAYER = await db.PLAYER.FindAsync(id);
            if (pLAYER == null)
            {
                return NotFound();
            }

            return Ok(pLAYER);
        }

        [ResponseType(typeof(PLAYER))]
        [Route("api/Players/{id1},{id2}")]
        public async Task<IHttpActionResult> GetPlayers(int id1, int id2)
        {
            int[] ids = new int[2] { id1, id2};
            List<PLAYER> players = new List<PLAYER>(15);
            foreach (var id in ids)
            {
                PLAYER player = await db.PLAYER.FindAsync(id);
                if (player == null)
                {
                    return NotFound();
                }
                players.Add(player);
            }

            return Ok(players);
        }

        // GET: api/Players/{multiple}
        [ResponseType(typeof(PLAYER))]
        [Route("api/Players/{id1},{id2},{id3},{id4},{id5},{id6},{id7},{id8},{id9},{id10},{id11},{id12},{id13},{id14},{id15}")]
        public async Task<IHttpActionResult> GetPlayers(int id1, int id2, int id3, int id4, int id5, int id6, int id7, int id8, int id9, int id10, int id11, int id12, int id13, int id14, int id15)
        {
            int[] ids = new int[15] { id1, id2, id3, id4, id5, id6, id7, id8, id9, id10, id11, id12, id13, id14, id15 };
            List<PLAYER> players = new List<PLAYER>(15);
            foreach (var id in ids)
            {
                PLAYER player = await db.PLAYER.FindAsync(id);
                if (player == null)
                {
                    return NotFound();
                }
                players.Add(player);
            }

            return Ok(players);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PLAYERExists(int id)
        {
            return db.PLAYER.Count(e => e.ID == id) > 0;
        }
    }
}