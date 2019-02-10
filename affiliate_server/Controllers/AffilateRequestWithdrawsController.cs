using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using affiliate_server.Models;

namespace affiliate_server.Controllers
{
     
    public class AffilateRequestWithdrawsController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffilateRequestWithdraws
        public IQueryable<AffilateRequestWithdraw> GetAffilateRequestWithdraws()
        {
            return db.AffilateRequestWithdraws;
        }

        // GET: api/AffilateRequestWithdraws/5
        [ResponseType(typeof(AffilateRequestWithdraw))]
        public IHttpActionResult GetAffilateRequestWithdraw(int id)
        {
            AffilateRequestWithdraw affilateRequestWithdraw = db.AffilateRequestWithdraws.Find(id);
            if (affilateRequestWithdraw == null)
            {
                return NotFound();
            }

            return Ok(affilateRequestWithdraw);
        }

        // PUT: api/AffilateRequestWithdraws/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffilateRequestWithdraw(int id, AffilateRequestWithdraw affilateRequestWithdraw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affilateRequestWithdraw.ID)
            {
                return BadRequest();
            }

            db.Entry(affilateRequestWithdraw).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffilateRequestWithdrawExists(id))
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

        // POST: api/AffilateRequestWithdraws
        [ResponseType(typeof(AffilateRequestWithdraw))]
        public IHttpActionResult PostAffilateRequestWithdraw(AffilateRequestWithdraw affilateRequestWithdraw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffilateRequestWithdraws.Add(affilateRequestWithdraw);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AffilateRequestWithdrawExists(affilateRequestWithdraw.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = affilateRequestWithdraw.ID }, affilateRequestWithdraw);
        }

        // DELETE: api/AffilateRequestWithdraws/5
        [ResponseType(typeof(AffilateRequestWithdraw))]
        public IHttpActionResult DeleteAffilateRequestWithdraw(int id)
        {
            AffilateRequestWithdraw affilateRequestWithdraw = db.AffilateRequestWithdraws.Find(id);
            if (affilateRequestWithdraw == null)
            {
                return NotFound();
            }

            db.AffilateRequestWithdraws.Remove(affilateRequestWithdraw);
            db.SaveChanges();

            return Ok(affilateRequestWithdraw);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffilateRequestWithdrawExists(int id)
        {
            return db.AffilateRequestWithdraws.Count(e => e.ID == id) > 0;
        }
    }
}