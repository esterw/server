//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace affiliate_server.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AffiliatesCommission
    {
        public int ID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public string Link { get; set; }
        public Nullable<decimal> CostPerLead { get; set; }
        public Nullable<decimal> RevenueSharePercentages { get; set; }
        public Nullable<decimal> RevenueShareFromAmount { get; set; }
        public Nullable<decimal> CostPerAcquisition { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> AvailableDateFrom { get; set; }
        public Nullable<System.DateTime> AvailableDateTill { get; set; }
        public Nullable<int> AffiliateID { get; set; }
        public string ProductLink { get; set; }
        public string Logo { get; set; }
    
        public virtual Affiliate Affiliate { get; set; }
    }
}
