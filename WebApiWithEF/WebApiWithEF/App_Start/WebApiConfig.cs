using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using WebApiWithEF.Models;

namespace WebApiWithEF
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Configure HTTP Caching using Entity Tags (ETags)
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["POIContext"].ConnectionString;
            var eTagStore = new CacheCow.Server.EntityTagStore.SqlServer.SqlServerEntityTagStore(connString);
            var cacheCowCacheHandler = new CacheCow.Server.CachingHandler(eTagStore);
            cacheCowCacheHandler.AddLastModifiedHeader = false;
            config.MessageHandlers.Add(cacheCowCacheHandler);
            //var cacheCowCacheHandler = new CacheCow.Server.CachingHandler();
            //config.MessageHandlers.Add(cacheCowCacheHandler);
            // Attribute routing.
            //config.MapHttpAttributeRoutes();
            // Convention-based routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //OData 方法
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<POI>("POIOData");
            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());

            // 取消注释下面的代码行可对具有 IQueryable 或 IQueryable<T> 返回类型的操作启用查询支持。
            // 若要避免处理意外查询或恶意查询，请使用 QueryableAttribute 上的验证设置来验证传入查询。
            // 有关详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=279712。
            //config.EnableQuerySupport();

            //Configure the Media-Type Formatters
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}