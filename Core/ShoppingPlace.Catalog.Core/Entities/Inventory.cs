using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Core.Entities
{
    public class Inventory : BaseEntity
    {
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public decimal SaleableQuantity { get; set; }
        public decimal SoldQuantity { get; set; }
        public decimal NonSaleableQuantity { get; set; }
    }
}
