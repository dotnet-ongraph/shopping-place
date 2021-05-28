using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfillment.Core.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int LineItemId { get; set; }
        public string Sku { get; set; }
        public string Product { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
       


    }
}
