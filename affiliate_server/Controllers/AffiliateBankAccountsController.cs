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
    public class AffiliateBankAccountsController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliateBankAccounts
        public IQueryable<AffiliateBankAccount> GetAffiliateBankAccounts()
        {
            return db.AffiliateBankAccounts;
        }

        // GET: api/AffiliateBankAccounts/5
        [ResponseType(typeof(AffiliateBankAccount))]
        public IHttpActionResult GetAffiliateBankAccount(int id)
        {
            AffiliateBankAccount affiliateBankAccount = db.AffiliateBankAccounts.Find(id);
            if (affiliateBankAccount == null)
            {
                return NotFound();
            }

            return Ok(affiliateBankAccount);
        }

        // PUT: api/AffiliateBankAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliateBankAccount(int id, AffiliateBankAccount affiliateBankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliateBankAccount.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliateBankAccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateBankAccountExists(id))
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

        // POST: api/AffiliateBankAccounts
        [ResponseType(typeof(AffiliateBankAccount))]
        public IHttpActionResult PostAffiliateBankAccount(AffiliateBankAccount affiliateBankAccount)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AffiliateBankAccountExists(affiliateBankAccount.ID))
            {
               return PutAffiliateBankAccount(affiliateBankAccount.ID, affiliateBankAccount);
            }

            db.AffiliateBankAccounts.Add(affiliateBankAccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = affiliateBankAccount.ID }, affiliateBankAccount);
        }

        // DELETE: api/AffiliateBankAccounts/5
        [ResponseType(typeof(AffiliateBankAccount))]
        public IHttpActionResult DeleteAffiliateBankAccount(int id)
        {
            AffiliateBankAccount affiliateBankAccount = db.AffiliateBankAccounts.Find(id);
            if (affiliateBankAccount == null)
            {
                return NotFound();
            }

            db.AffiliateBankAccounts.Remove(affiliateBankAccount);
            db.SaveChanges();

            return Ok(affiliateBankAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateBankAccountExists(int id)
        {
            return db.AffiliateBankAccounts.Count(e => e.ID == id) > 0;
        }
    }
}