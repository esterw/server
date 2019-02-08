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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AffiliatesBannersController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliatesBanners
        public IQueryable<AffiliatesBanner> GetAffiliatesBanners()
        {
            return db.AffiliatesBanners;
        }

        // GET: api/AffiliatesBanners/5
        [ResponseType(typeof(AffiliatesBanner))]
        public IHttpActionResult GetAffiliatesBanner(int id)
        {
            AffiliatesBanner affiliatesBanner = db.AffiliatesBanners.Find(id);
            if (affiliatesBanner == null)
            {
                return NotFound();
            }

            return Ok(affiliatesBanner);
        }

        // PUT: api/AffiliatesBanners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliatesBanner(int id, AffiliatesBanner affiliatesBanner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliatesBanner.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliatesBanner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliatesBannerExists(id))
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

        // POST: api/AffiliatesBanners
        [ResponseType(typeof(AffiliatesBanner))]
        public IHttpActionResult PostAffiliatesBanner(AffiliatesBanner affiliatesBanner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffiliatesBanners.Add(affiliatesBanner);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = affiliatesBanner.ID }, affiliatesBanner);
        }

        // DELETE: api/AffiliatesBanners/5
        [ResponseType(typeof(AffiliatesBanner))]
        public IHttpActionResult DeleteAffiliatesBanner(int id)
        {
            AffiliatesBanner affiliatesBanner = db.AffiliatesBanners.Find(id);
            if (affiliatesBanner == null)
            {
                return NotFound();
            }

            db.AffiliatesBanners.Remove(affiliatesBanner);
            db.SaveChanges();

            return Ok(affiliatesBanner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliatesBannerExists(int id)
        {
            return db.AffiliatesBanners.Count(e => e.ID == id) > 0;
        }
    }
}