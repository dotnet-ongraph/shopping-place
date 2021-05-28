using Core.Base;

namespace Catalog.Core.Entities
{
    public class Tag : BaseEntity
    {
        public EntityType EntityType { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}