﻿using System;
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

            // בדיקה שהבקשה אכן מכילה את הנתונים הנדרשים דדי להחזיר את הקובץ הנדרש
            if (bannerID != string.Empty && affiliateID != string.Empty)
            {
                int affID = Convert.ToInt32(affiliateID);
                int bannrID = Convert.ToInt32(bannerID);

                // מציאת השותף האחראי על הקישור
                Affiliate aff = db.Affiliates.Where(x => x.ID == affID).FirstOrDefault();

                try
                {
                    // עדכון קליקים לבאנר של השותף
                    AffiliatesBanner ab = db.AffiliatesBanners.Where(x => 
                    (x.AffiliateID == affID) && (x.BannerID == bannrID)).FirstOrDefault<AffiliatesBanner>();
                    if (ab.Clicks == null)
                        ab.Clicks = 1;
                    else
                        ab.Clicks  = ab.Clicks+ 1;
                    db.Entry(ab).State = EntityState.Modified;
                 
                }
                catch
                {
                    // אם נפל - זאת רומרת שעדיין לא קיימים קליקים לבאנר הנוכחי
                    // אז נוצר עבור השותף שיוך לבאנר עם הקליק
                    AffiliatesBanner ab = new AffiliatesBanner();
                    ab.AffiliateID = affID;
                    ab.BannerID = bannrID;
                    ab.Clicks = 1;
                    db.AffiliatesBanners.Add(ab);
                }
                try
                {
                    // השמת התאריך של היום נוכחי
                    DateTime dt = DateTime.Now.Date;

                    // איתור דו"ח באנרים נוספים שקיימים בתאריך זה
                    AffiliateRevenueReport arr = db.AffiliateRevenueReports.Where(x => 
                    x.AffiliateID == affID && x.AffiliateDate == dt).FirstOrDefault<AffiliateRevenueReport>();
                    // עדכון מס המבקרים לדו"ח
                    if (arr.Visits == null)
                        arr.Visits = 1;
                    else
                        arr.Visits = arr.Visits + 1;
                    //  עדכון רווח עבור הקליק ע"פ הסכום שהוקצה לו ע"י מנהלי האתר
                    if (arr.Profit == null)
                        arr.Profit = aff.AffiliatesCommissions.FirstOrDefault().CostPerLead;
                    else
                        arr.Profit = arr.Profit + aff.AffiliatesCommissions.FirstOrDefault().CostPerLead; ;
                    db.Entry(arr).State = EntityState.Modified;

                }
                catch
                {
                    // אם לא נמצא דו"ח על התאריך של היום הנוכחי
                    // אז יוצר דו"ח עם התאריך של היום הנוכחי
                    AffiliateRevenueReport arr = new AffiliateRevenueReport();
                    arr.AffiliateID = affID;
                    arr.AffiliateDate = DateTime.Now.Date;
                    arr.Visits = 1;
                    arr.Profit = aff.AffiliatesCommissions.FirstOrDefault().CostPerLead;
                    db.AffiliateRevenueReports.Add(arr);
                }

                //-הוספת נתוני הקליק ל
                //Datebase
                // תאריך ומאיזה אתר הגיע
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

                    // נסיון לשמור את הנתונים ב-
                    //database
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
                { // אם לא הצליח לשמור את הנתונים יוצא ולא מחזיר כלום ללקוח

                    throw;
                }
            }

            // אם הצליח  לשמור את הנתונים - מעביר את המבקש לאתר אותו השותף מפרסם
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