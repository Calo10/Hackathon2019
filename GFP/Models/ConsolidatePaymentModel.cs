using System;
namespace GFP.Models
{
    public class ConsolidatePaymentModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string IBAN { get; set; }
        public string total_amount { get; set; }
    }
}
