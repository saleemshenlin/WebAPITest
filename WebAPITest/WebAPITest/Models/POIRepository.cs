using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITest.Models
{
    public class POIRepository:IPOIRepository
    {
        private List<POI> pois = new List<POI>();
        private int _nextId = 1;

        public POIRepository(){
            AddPOI( new POI {Id = 1, Name = "大庆油田历史陈列馆",C_ID=1,D_Name="萨尔图区",Address="大庆市萨尔图区中七路32号",Time="8:00-17:00",Tele="0459-5813777",Abstract="大庆油田历史陈列馆占地面积15900多平方米，陈展面积4200多平方米",Ticket="5元"});
            AddPOI( new POI {Id = 2, Name = "大庆曼哈维大酒店",C_ID=2,D_Name="萨尔图区",Address="大庆市高新区新风路9号",Tele="0459-8166666",Abstract="大庆曼哈维大酒店是大庆唯一五星级国际标准综合酒店，地处高新技术开发区",Ticket="1958元（豪华行政套房）",Type="五星级"});
            AddPOI( new POI {Id = 3, Name = "一口猪（新村店）",C_ID=3,D_Name="萨尔图区",Address="黑龙江省大庆市萨尔图区纬二路180号",Time="9:00-22:00",Tele="0459-4621801",Abstract="公司成立于1995年，公司拥有国家专业品牌“一口猪”的全部知识产权。",Ticket="60元"});
            AddPOI( new POI {Id = 4, Name = "沃尔玛经六街店", C_ID = 4, D_Name = "萨尔图区", Address = "大庆市萨尔图区经六街60号", Time = "8:00-22:00", Tele = "0459-8992888", Abstract = "第一家沃尔玛购物广场于1988年3月1日在美国密苏里州的华盛顿开业。" });
        }

         public IEnumerable<POI> GetAllPOIs()
        {
            return pois;
        }

         public POI GetByID(int id)
         {
             return pois.Find(p => p.Id == id);
         }

         public POI AddPOI(POI item)
         {
             if (item == null)
             {
                 throw new ArgumentNullException("item");
             }
             item.Id = _nextId++;
             pois.Add(item);
             return item;
         }

         public void RemovePOI(int id)
         {
             pois.RemoveAll(p => p.Id == id);
         }

         public bool UpdatePOI(POI item)
         {
             if (item == null)
             {
                 throw new ArgumentNullException("item");
             }
             int index = pois.FindIndex(p => p.Id == item.Id);
             if (index == -1)
             {
                 return false;
             }
             pois.RemoveAt(index);
             pois.Add(item);
             return true;
         }
        
            
    }
}