using affiliate_server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Affiliates.Server
{
    /// <summary>
    /// Summary description for BannerH
    /// </summary>
    public class BannerH : IHttpHandler
    {
        private AffiliateDBEntities db = new AffiliateDBEntities();

        public void ProcessRequest(HttpContext context)
        {

            var urlReferrer = context.Request.UrlReferrer;
            //var gg = context.Request.Cookies;
            HttpResponse r = context.Response;
            try
            {
                string bannerID = context.Request.QueryString["Banner"];
                string affiliateID = context.Request.QueryString["Aff"];

                if (bannerID != string.Empty && affiliateID != string.Empty)
                {
                    int affID = Convert.ToInt32(affiliateID);
                    int bannrID = Convert.ToInt32(bannerID);
                    try
                    {
                        AffiliatesBanner ab = db.AffiliatesBanners.Where(x => (x.AffiliateID == affID) && (x.BannerID == bannrID)).FirstOrDefault<AffiliatesBanner>();
                       if (ab.Impressions == null)
                           ab.Impressions = 1;
                        else
                        ab.Impressions += 1;
                       ab.SummaryDate = DateTime.Now;
                        db.Entry(ab).State = EntityState.Modified;
                  


                    }
                    catch
                    {
                        if ((db.Affiliates.Find(affID) == null) || (db.AffiliateBanners.Find(bannrID) == null))
                        {
                          
                            throw;
                        }
                        AffiliatesBanner ab = new AffiliatesBanner();
                        ab.AffiliateID = affID;
                        ab.BannerID = bannrID;
                        ab.Impressions = 1;
                       // ab.SummaryDate = DateTime.Now;
                        db.AffiliatesBanners.Add(ab);
                    

                    }
                    AffiliateBannerView abv = new AffiliateBannerView();
                    abv.AffiliateID = affID;
                    abv.BannerID = bannrID;
                    // abv.UrlReferrer = context.Request.UrlReferrer.OriginalString;
                    abv.ViewDate = new DateTime();
                    //db.AffiliateBannerViews.Add(abv);

                    try
                    {
                        db.SaveChanges();
                        AffiliateBanner banner = db.AffiliateBanners.Find(Convert.ToInt32(bannerID));
                        string ext = Path.GetExtension(banner.BannerPath);
                        r.ContentType = "image/" + ext;
                        var pUrl = new Uri(banner.BannerPath).PathAndQuery;
                        r.WriteFile("~/" + pUrl);
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;
                    }

                }
            }
            catch
            {
                try { 
                string bannerID = context.Request.QueryString["Banner"];

                if (bannerID != string.Empty)
                {
                    AffiliateBanner banner = db.AffiliateBanners.Find(Convert.ToInt32(bannerID));
                    string ext = Path.GetExtension(banner.BannerPath);
                    r.ContentType = "image/" + ext;
                    var pUrl = new Uri(banner.BannerPath).PathAndQuery;
                    r.WriteFile("~/" + pUrl);
                }
                }
                catch
                {
                    r.ContentType = "image/jpg";
                    r.WriteFile("~/Images/error.jpg");
                }



            }
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