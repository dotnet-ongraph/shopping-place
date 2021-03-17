using Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PropertyTypeId")]
        public virtual ProductType Type { get; set; }
        public decimal MRP { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
