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
    
    public partial class SubAffiliate
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string UserName { get; set; }
        public string URL { get; set; }
        public Nullable<int> AffiliateID { get; set; }
    
        public virtual Affiliate Affiliate { get; set; }
    }
}
