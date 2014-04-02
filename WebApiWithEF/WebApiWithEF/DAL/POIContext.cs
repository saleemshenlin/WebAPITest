using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApiWithEF.Models;

namespace WebApiWithEF.DAL
{
    /// <summary>
    /// Entity Framework下的Code First 形式<br>
    /// 自动创建的poi数据库<br>
    /// 根据POI类型定义数据库的表结构<br>
    /// </summary>
    public class POIContext : DbContext
    {
        /// <summary>
        /// 数据连接名称
        /// 与web.config文件中connectionStrings对应
        /// </summary>
        public POIContext()
            : base("name=POIContext")
        {
        }
        /// <summary>
        /// 连接数据库查询
        /// </summary>
        public  DbSet <POI> POIs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}