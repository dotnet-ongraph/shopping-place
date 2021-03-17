using Core.Base;
using System;

namespace Fulfillment.Core.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public string Sku { get; set; }
        public decimal OrderedQuantity { get; set; }




    }
}
