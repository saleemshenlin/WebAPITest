using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITest.Models
{
    public class POI
    {
        public int Id { get; set; }
        public int C_ID { get; set; }
        public string Name { get; set; }
        public string D_Name { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
        public string Tele { get; set; }
        public string Abstract { get; set; }
        public string Ticket { get; set; }
        public string Type { get; set; }
        public string Geometry { get; set; }
    }
}