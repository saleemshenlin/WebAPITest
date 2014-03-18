using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPITest.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public POI Poi { get; set; }
        public Order Order { get; set; }
    }
}