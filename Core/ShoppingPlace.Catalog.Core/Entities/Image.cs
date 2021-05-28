using Core.Base;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Core.Entities
{
    public class Image : BaseEntity
    {
        public string PublicUrl { get; set; }
        public string PublicThumblUrl { get; set; }
        [MaxLength(100)]
        public string FileName { get; set; }
        public int Order { get; set; }
        public bool IsDefault { get; set; }

        public EntityType? Type { get; set; }
        public int? EntityId { get; set; }
    }
}