using Core.Base;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Core.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
