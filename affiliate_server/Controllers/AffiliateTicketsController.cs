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
    public class AffiliateTicketsController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliateTickets
        public IQueryable<AffiliateTicket> GetAffiliateTickets()
        {
            return db.AffiliateTickets;
        }

        // GET: api/AffiliateTickets/5
        [ResponseType(typeof(AffiliateTicket))]
        public IHttpActionResult GetAffiliateTicket(int id)
        {
            AffiliateTicket affiliateTicket = db.AffiliateTickets.Find(id);
            if (affiliateTicket == null)
            {
                return NotFound();
            }

            return Ok(affiliateTicket);
        }

        // PUT: api/AffiliateTickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliateTicket(int id, AffiliateTicket affiliateTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliateTicket.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliateTicket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateTicketExists(id))
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

        // POST: api/AffiliateTickets
        [ResponseType(typeof(AffiliateTicket))]
        public IHttpActionResult PostAffiliateTicket(AffiliateTicket affiliateTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffiliateTickets.Add(affiliateTicket);

            db.SaveChanges();

            AffiliateTicketContent afCont = new AffiliateTicketContent();
            afCont.TicketID = affiliateTicket.ID;
            afCont.Subject = affiliateTicket.Subject;
            afCont.CreatedDate = affiliateTicket.CreatedDate ;
            afCont.CreatedBy = affiliateTicket.CreatedBy;
            db.AffiliateTicketContents.Add(afCont);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = affiliateTicket.ID }, affiliateTicket);
        }

        // DELETE: api/AffiliateTickets/5
        [ResponseType(typeof(AffiliateTicket))]
        public IHttpActionResult DeleteAffiliateTicket(int id)
        {
            AffiliateTicket affiliateTicket = db.AffiliateTickets.Find(id);
            if (affiliateTicket == null)
            {
                return NotFound();
            }

            db.AffiliateTickets.Remove(affiliateTicket);
            db.SaveChanges();

            return Ok(affiliateTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateTicketExists(int id)
        {
            return db.AffiliateTickets.Count(e => e.ID == id) > 0;
        }
    }
}