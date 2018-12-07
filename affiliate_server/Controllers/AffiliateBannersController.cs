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
    public class AffiliateBannersController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliateBanners
        public IQueryable<AffiliateBanner> GetAffiliateBanners()
        {
            return db.AffiliateBanners;
        }

        // GET: api/AffiliateBanners/5
        [ResponseType(typeof(AffiliateBanner))]
        public IHttpActionResult GetAffiliateBanner(int id)
        {
            AffiliateBanner affiliateBanner = db.AffiliateBanners.Find(id);
            if (affiliateBanner == null)
            {
                return NotFound();
            }

            return Ok(affiliateBanner);
        }

        // PUT: api/AffiliateBanners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliateBanner(int id, AffiliateBanner affiliateBanner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliateBanner.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliateBanner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateBannerExists(id))
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

        // POST: api/AffiliateBanners
        [ResponseType(typeof(AffiliateBanner))]
        public IHttpActionResult PostAffiliateBanner(AffiliateBanner affiliateBanner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffiliateBanners.Add(affiliateBanner);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AffiliateBannerExists(affiliateBanner.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = affiliateBanner.ID }, affiliateBanner);
        }

        // DELETE: api/AffiliateBanners/5
        [ResponseType(typeof(AffiliateBanner))]
        public IHttpActionResult DeleteAffiliateBanner(int id)
        {
            AffiliateBanner affiliateBanner = db.AffiliateBanners.Find(id);
            if (affiliateBanner == null)
            {
                return NotFound();
            }

            db.AffiliateBanners.Remove(affiliateBanner);
            db.SaveChanges();

            return Ok(affiliateBanner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateBannerExists(int id)
        {
            return db.AffiliateBanners.Count(e => e.ID == id) > 0;
        }
    }
}