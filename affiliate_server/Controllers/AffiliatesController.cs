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
    public class AffiliatesController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/Affiliates
        public IQueryable<Affiliate> GetAffiliates()
        {
            return db.Affiliates;
        }

        // GET: api/Affiliates/5
        [ResponseType(typeof(Affiliate))]
        public IHttpActionResult GetAffiliate(int id)
        {
            Affiliate affiliate = db.Affiliates.Find(id);
            if (affiliate == null)
            {
                return NotFound();
            }

            return Ok(affiliate);
        }

        // GET: api/Affiliates?email=""&password=""
        [ResponseType(typeof(Affiliate))]
        public IHttpActionResult LoginAffiliate(string email, string password)
        {
            Affiliate affiliate = db.Affiliates.Where(X => (X.Email == email && X.Password == password)).FirstOrDefault();
            if (affiliate == null)
            {
                return NotFound();
            }

            return Ok(affiliate);
        }

        // PUT: api/Affiliates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliate(int id, Affiliate affiliate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliate.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateExists(id))
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

        // POST: api/Affiliates
        [ResponseType(typeof(Affiliate))]
        public IHttpActionResult PostAffiliate(Affiliate affiliate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            affiliate.Balance = 0;
            db.Affiliates.Add(affiliate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AffiliateExists(affiliate.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = affiliate.ID }, affiliate);
        }

        // DELETE: api/Affiliates/5
        [ResponseType(typeof(Affiliate))]
        public IHttpActionResult DeleteAffiliate(int id)
        {
            Affiliate affiliate = db.Affiliates.Find(id);
            if (affiliate == null)
            {
                return NotFound();
            }

            db.Affiliates.Remove(affiliate);
            db.SaveChanges();

            return Ok(affiliate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateExists(int id)
        {
            return db.Affiliates.Count(e => e.ID == id) > 0;
        }
    }
}