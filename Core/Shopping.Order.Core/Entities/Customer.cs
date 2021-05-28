using Core.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fulfillment.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string EmailId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return FirstName + " " + LastName; } }
        [ForeignKey("CustomerId")]
        public List<CustomerAddress> AllAddresses { get; set; }
        public int MobileNumber { get; set; }
    }
}
