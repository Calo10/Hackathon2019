using System;
namespace GFP.Models
{
    public class TresuryBatchValidationModel
    {
        public string EntityID { get; set; }
        public string TotalAmount { get; set; }
        public string BatchId { get; set; }
        public string TresuryValidated { get; set; }
    }

}
