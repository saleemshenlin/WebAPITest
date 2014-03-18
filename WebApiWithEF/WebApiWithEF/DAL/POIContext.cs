using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApiWithEF.Models;

namespace WebApiWithEF.DAL
{
    public class POIContext : DbContext
    {
        public POIContext()
            : base("name=POIContext")
        {
        }
        public  DbSet <POI> POIs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}