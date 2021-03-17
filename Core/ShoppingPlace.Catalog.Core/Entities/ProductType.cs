using Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Core.Entities
{
    public class ProductType : BaseEntity
    {
        [MaxLength(50)]
        public string Prefix { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
