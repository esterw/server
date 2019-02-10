using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using affiliate_server.Models;
using System.Web.Http.Cors;

namespace Affiliates.Server
{
    /// <summary>
    /// Summary description for Click
    /// </summary>
     
    public class Click : IHttpHandler
    {

       private AffiliateDBEntities db = new AffiliateDBEntities();

        public void ProcessRequest(HttpContext context)
        {

       // var urlReferrer = context.Request.UrlReferrer;
            HttpResponse r = context.Response;
            string bannerID = context.Request.QueryString["Banner"];
            string affiliateID = context.Request.QueryString["Aff"];
            if (bannerID != string.Empty && affiliateID != string.Empty)
            {
                int affID = Convert.ToInt32(affiliateID);
                int bannrID = Convert.ToInt32(bannerID);
                Affiliate aff = db.Affiliates.Where(x => x.ID == affID).FirstOrDefault();

                try
                {
                    AffiliatesBanner ab = db.AffiliatesBanners.Where(x => (x.AffiliateID == affID) && (x.BannerID == bannrID)).FirstOrDefault<AffiliatesBanner>();
                    if (ab.Clicks == null)
                        ab.Clicks = 1;
                    else
                        ab.Clicks  = ab.Clicks+ 1;
                  //  ab.SummaryDate = DateTime.Now;
                    db.Entry(ab).State = EntityState.Modified;
                 
                }
                catch
                {
                    AffiliatesBanner ab = new AffiliatesBanner();
                    ab.AffiliateID = affID;
                    ab.BannerID = bannrID;
                    ab.Clicks = 1;
                  //  ab.SummaryDate = DateTime.Now;
                    db.AffiliatesBanners.Add(ab);
                }
                try
                {
                    DateTime dt = DateTime.Now.Date;
                    AffiliateRevenueReport arr = db.AffiliateRevenueReports.Where(x => x.AffiliateID == affID && x.AffiliateDate == dt).FirstOrDefault<AffiliateRevenueReport>();
                    if (arr.Visits == null)
                        arr.Visits = 1;
                    else
                        arr.Visits = arr.Visits + 1;
                    if (arr.Profit == null)
                        arr.Profit = aff.AffiliatesCommissions.FirstOrDefault().CostPerLead;
                    else
                        arr.Profit = arr.Profit + aff.AffiliatesCommissions.FirstOrDefault().CostPerLead; ;
                    db.Entry(arr).State = EntityState.Modified;

                }
                catch
                {
                    AffiliateRevenueReport arr = new AffiliateRevenueReport();
                    arr.AffiliateID = affID;
                    arr.AffiliateDate = DateTime.Now.Date;
                    arr.Visits = 1;
                    arr.Profit = aff.AffiliatesCommissions.FirstOrDefault().CostPerLead;
                    db.AffiliateRevenueReports.Add(arr);
                }
                AffiliateBannerClick abc = new AffiliateBannerClick();
                abc.AffiliateID = affID;
                abc.BannerID = bannrID;
                if (context.Request.UrlReferrer != null)
                    abc.UrlReferrer = context.Request.UrlReferrer.OriginalString;
                else
                    abc.UrlReferrer = "Direct Link";
                abc.ClickDate = DateTime.Now;
                db.AffiliateBannerClicks.Add(abc);
                try
                {
                    if(aff.AffiliatesCommissions.FirstOrDefault().CostPerLead != null)
                    {
                        aff.Balance += aff.AffiliatesCommissions.FirstOrDefault().CostPerLead;

                    } else if (aff.AffiliatesCommissions.FirstOrDefault().CostPerAcquisition != null)
                    {

                    }
                    else if (aff.AffiliatesCommissions.FirstOrDefault().RevenueSharePercentages != null)
                    {

                    }
                    db.Entry(aff).State = EntityState.Modified;

                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
            }

            r.Redirect("http://localhost:4200");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}