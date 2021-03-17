using Core.Base;

namespace Catalog.Core.Entities
{
    public class Property : BaseEntity
    {
        public PropertyType Type { get; set; }
        public string Value { get; set; }
    }
}
