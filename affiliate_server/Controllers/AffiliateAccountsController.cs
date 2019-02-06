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
    public class AffiliateAccountsController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliateAccounts
        public IQueryable<AffiliateAccount> GetAffiliateAccounts()
        {
            return db.AffiliateAccounts;
        }

        // GET: api/AffiliateAccounts/5
        [ResponseType(typeof(AffiliateAccount))]
        public IHttpActionResult GetAffiliateAccount(int id)
        {
            AffiliateAccount affiliateAccount = db.AffiliateAccounts.Find(id);
            if (affiliateAccount == null)
            {
                return NotFound();
            }

            return Ok(affiliateAccount);
        }

        // PUT: api/AffiliateAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliateAccount(int id, AffiliateAccount affiliateAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliateAccount.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliateAccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateAccountExists(id))
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

        // POST: api/AffiliateAccounts
        [ResponseType(typeof(AffiliateAccount))]
        public IHttpActionResult PostAffiliateAccount(AffiliateAccount affiliateAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffiliateAccounts.Add(affiliateAccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = affiliateAccount.ID }, affiliateAccount);
        }

        // DELETE: api/AffiliateAccounts/5
        [ResponseType(typeof(AffiliateAccount))]
        public IHttpActionResult DeleteAffiliateAccount(int id)
        {
            AffiliateAccount affiliateAccount = db.AffiliateAccounts.Find(id);
            if (affiliateAccount == null)
            {
                return NotFound();
            }

            db.AffiliateAccounts.Remove(affiliateAccount);
            db.SaveChanges();

            return Ok(affiliateAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateAccountExists(int id)
        {
            return db.AffiliateAccounts.Count(e => e.ID == id) > 0;
        }
    }
}