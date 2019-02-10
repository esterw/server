using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using affiliate_server.Models;

namespace affiliate_server.Controllers
{
    public class AffiliateRequestWithdrawlsController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliateRequestWithdrawls
        public IQueryable<AffiliateRequestWithdrawl> GetAffiliateRequestWithdrawls()
        {
            return db.AffiliateRequestWithdrawls;
        }

        // GET: api/AffiliateRequestWithdrawls/5
        [ResponseType(typeof(AffiliateRequestWithdrawl))]
        public IHttpActionResult GetAffiliateRequestWithdrawl(int id)
        {
            AffiliateRequestWithdrawl affiliateRequestWithdrawl = db.AffiliateRequestWithdrawls.Find(id);
            if (affiliateRequestWithdrawl == null)
            {
                return NotFound();
            }

            return Ok(affiliateRequestWithdrawl);
        }

        // PUT: api/AffiliateRequestWithdrawls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliateRequestWithdrawl(int id, AffiliateRequestWithdrawl affiliateRequestWithdrawl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliateRequestWithdrawl.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliateRequestWithdrawl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateRequestWithdrawlExists(id))
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

        // POST: api/AffiliateRequestWithdrawls
        [ResponseType(typeof(AffiliateRequestWithdrawl))]
        public IHttpActionResult PostAffiliateRequestWithdrawl(AffiliateRequestWithdrawl affiliateRequestWithdrawl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffiliateRequestWithdrawls.Add(affiliateRequestWithdrawl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = affiliateRequestWithdrawl.ID }, affiliateRequestWithdrawl);
        }

        // DELETE: api/AffiliateRequestWithdrawls/5
        [ResponseType(typeof(AffiliateRequestWithdrawl))]
        public IHttpActionResult DeleteAffiliateRequestWithdrawl(int id)
        {
            AffiliateRequestWithdrawl affiliateRequestWithdrawl = db.AffiliateRequestWithdrawls.Find(id);
            if (affiliateRequestWithdrawl == null)
            {
                return NotFound();
            }

            db.AffiliateRequestWithdrawls.Remove(affiliateRequestWithdrawl);
            db.SaveChanges();

            return Ok(affiliateRequestWithdrawl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateRequestWithdrawlExists(int id)
        {
            return db.AffiliateRequestWithdrawls.Count(e => e.ID == id) > 0;
        }
    }
}