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
    
    public partial class AffiliateTicket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AffiliateTicket()
        {
            this.AffiliateTicketContents = new HashSet<AffiliateTicketContent>();
        }
    
        public int ID { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTime> LastResponse { get; set; }
        public string Actions { get; set; }
        public byte[] IsReadByAffiliate { get; set; }
        public Nullable<int> AffiliateID { get; set; }
    
        public virtual Affiliate Affiliate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AffiliateTicketContent> AffiliateTicketContents { get; set; }
    }
}
