using Core.Base;

namespace Fulfillment.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
    }
}
