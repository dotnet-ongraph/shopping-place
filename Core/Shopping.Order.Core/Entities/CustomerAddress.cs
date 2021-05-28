using Core.Base;

namespace Fulfillment.Core.Entities
{
    public class CustomerAddress : BaseEntity
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
    }
}
