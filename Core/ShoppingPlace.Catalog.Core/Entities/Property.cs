using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Core.Entities
{
    public class Property : BaseEntity
    {
        [ForeignKey("PropertyType")]
        public int PropertyTypeId { get; set; }
        public PropertyType Type { get; set; }
        public string Value { get; set; }

        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductTypeId { get; set; }
    }
}