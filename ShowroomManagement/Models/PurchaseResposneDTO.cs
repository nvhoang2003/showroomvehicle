using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowroomManagement.Models
{
    public class PurchaseResposneDTO
    {
        public int purchaseId { get; set; }
        public int vehicleId { get; set; }
        public string fullName { get; set; }
        public DateTime datePurchase { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string modelNumber { get; set; }
        public string color { get; set; }
    }
}