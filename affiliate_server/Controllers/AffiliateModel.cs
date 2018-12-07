using System;

namespace affiliate_server.Controllers
{
    public class AffiliateModel
    {
        public int NetworkID { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Revenue { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string PayoutCurrency { get; set; }
        public decimal MinimalPayout { get; set; }
        public string DomainAffiliate { get; set; }
        public string URLOfGame { get; set; }
        public string URLOfMediaResource { get; set; }
        public int CommissionPlanID { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string Swift { get; set; }
        public string City { get; set; }
        public int PaymentSystemID { get; set; }
        public string AffiliateLink { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public int BankAccountID { get; set; }
        public int SkrillAccountID { get; set; }
        public int NetlleterAccoutID { get; set; }
        public bool IsBankAccount { get; set; }
        public bool IsSkrillAccount { get; set; }
        public bool IsNetlleterAccount { get; set; }
        public int SiteID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AffiliateUserID { get; set; }
    }
}