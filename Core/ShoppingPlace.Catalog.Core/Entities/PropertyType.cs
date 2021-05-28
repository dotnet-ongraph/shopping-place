using Core.Base;

namespace Catalog.Core.Entities
{
    public class PropertyType : BaseEntity
    {
        public string Prefix { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}