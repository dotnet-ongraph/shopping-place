using Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Core.Entities
{
    public class Image : BaseEntity
    {
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public string PublicUrl { get; set; }
        public string PublicThumblUrl { get; set; }
        [MaxLength(100)]
        public string FileName { get; set; }
        public int Order { get; set; }
        public EntityType? EntityType { get; set; }
        public int? EntityId { get; set; }
        public bool IsDefault { get; set; }
    }
}
