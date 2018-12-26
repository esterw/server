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
using Newtonsoft.Json.Linq;

namespace affiliate_server.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AffiliateTicketContentsController : ApiController
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        // GET: api/AffiliateTicketContents
        public IQueryable<AffiliateTicketContent> GetAffiliateTicketContents()
        {
            return db.AffiliateTicketContents;
        }

        // GET: api/AffiliateTicketContents/5
        [ResponseType(typeof(AffiliateTicketContent))]
        public IHttpActionResult GetAffiliateTicketContent(int id)
        {
            AffiliateTicketContent affiliateTicketContent = db.AffiliateTicketContents.Find(id);
            if (affiliateTicketContent == null)
            {
                return NotFound();
            }

            return Ok(affiliateTicketContent);
        }

        // PUT: api/AffiliateTicketContents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAffiliateTicketContent(int id, AffiliateTicketContent affiliateTicketContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != affiliateTicketContent.ID)
            {
                return BadRequest();
            }

            db.Entry(affiliateTicketContent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffiliateTicketContentExists(id))
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

        // POST: api/AffiliateTicketContents
        [ResponseType(typeof(AffiliateTicketContent))]
        public IHttpActionResult PostAffiliateTicketContent(AffiliateTicketContent affiliateTicketContent)
        {

            //AffiliateTicketContent aff = new AffiliateTicketContent();
            //aff.TicketID = affiliateTicketContent.TicketID;
            //aff.Subject= affiliateTicketContent.Subject;
            //aff.Content = affiliateTicketContent.Content;
            //AffiliateTicketContent affiliateTicketContent = new AffiliateTicketContent();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AffiliateTicketContents.Add(affiliateTicketContent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = affiliateTicketContent.ID }, affiliateTicketContent);
        }

        // DELETE: api/AffiliateTicketContents/5
        [ResponseType(typeof(AffiliateTicketContent))]
        public IHttpActionResult DeleteAffiliateTicketContent(int id)
        {
            AffiliateTicketContent affiliateTicketContent = db.AffiliateTicketContents.Find(id);
            if (affiliateTicketContent == null)
            {
                return NotFound();
            }

            db.AffiliateTicketContents.Remove(affiliateTicketContent);
            db.SaveChanges();

            return Ok(affiliateTicketContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AffiliateTicketContentExists(int id)
        {
            return db.AffiliateTicketContents.Count(e => e.ID == id) > 0;
        }
    }
}