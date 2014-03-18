using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewAPITest.Models
{
    public class OrdersContextInitializer : DropCreateDatabaseIfModelChanges<OrdersContext>
    {
        protected override void Seed(OrdersContext context)
        {
            var pois = new List<POI>(){
            new POI { Name = "大庆油田历史陈列馆",C_ID=1,D_Name="萨尔图区",Address="大庆市萨尔图区中七路32号",Time="8:00-17:00",Tele="0459-5813777",Abstract="大庆油田历史陈列馆占地面积15900多平方米，陈展面积4200多平方米",Ticket="5元"},
            new POI {Name = "大庆曼哈维大酒店",C_ID=2,D_Name="萨尔图区",Address="大庆市高新区新风路9号",Tele="0459-8166666",Abstract="大庆曼哈维大酒店是大庆唯一五星级国际标准综合酒店，地处高新技术开发区",Ticket="1958元（豪华行政套房）",Type="五星级"},
            new POI {Name = "一口猪（新村店）",C_ID=3,D_Name="萨尔图区",Address="大庆市萨尔图区纬二路180号",Time="9:00-22:00",Tele="0459-4621801",Abstract="公司成立于1995年，公司拥有国家专业品牌“一口猪”的全部知识产权。",Ticket="60元"},
            new POI {Name = "沃尔玛经六街店",C_ID=4,D_Name="萨尔图区",Address="大庆市萨尔图区经六街60号",Time="8:00-22:00",Tele="0459-8992888",Abstract="第一家沃尔玛购物广场于1988年3月1日在美国密苏里州的华盛顿开业。"}
            };

            pois.ForEach(p => context.POIs.Add(p));
            context.SaveChanges();

            var order = new Order() { Customer = "Bob" };
            var od = new List<OrderDetail>()
            {
                new OrderDetail() { Poi = pois[0], Quantity = 2, Order = order},
                new OrderDetail() { Poi = pois[1], Quantity = 4, Order = order }
            };
            context.Orders.Add(order);
            od.ForEach(o => context.OrderDetails.Add(o));

            context.SaveChanges();

        }
    }
}