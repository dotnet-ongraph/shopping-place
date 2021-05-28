using Core.Base;
using System;
using System.Collections.Generic;

namespace Fulfillment.Core.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public Customer Customer { get; set; }
        public CustomerAddress ShippingAddress { get; set; }
        public CustomerAddress BillingAddress { get; set; }
        public DateTime? ShipByDate { get; set; }
        public string Remarks { get; set; }
    }
}
