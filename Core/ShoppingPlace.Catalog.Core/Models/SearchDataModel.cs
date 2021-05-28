using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Models
{
    class SearchDataModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public decimal quantity { get; set; }
    }
}
