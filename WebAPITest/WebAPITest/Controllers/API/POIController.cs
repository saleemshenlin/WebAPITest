using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    public class POIController : ApiController
    {
        //POI[] pois = new POI[]{
        //    new POI {Id = 1, Name = "大庆油田历史陈列馆",C_ID=1,D_Name="萨尔图区",Address="大庆市萨尔图区中七路32号",Time="8:00-17:00",Tele="0459-5813777",Abstract="大庆油田历史陈列馆占地面积15900多平方米，陈展面积4200多平方米",Ticket="5元"},
        //    new POI {Id = 2, Name = "大庆曼哈维大酒店",C_ID=2,D_Name="萨尔图区",Address="大庆市高新区新风路9号",Tele="0459-8166666",Abstract="大庆曼哈维大酒店是大庆唯一五星级国际标准综合酒店，地处高新技术开发区",Ticket="1958元（豪华行政套房）",Type="五星级"},
        //    new POI {Id = 3, Name = "一口猪（新村店）",C_ID=3,D_Name="萨尔图区",Address="黑龙江省大庆市萨尔图区纬二路180号",Time="9:00-22:00",Tele="0459-4621801",Abstract="公司成立于1995年，公司拥有国家专业品牌“一口猪”的全部知识产权。",Ticket="60元"},
        //    new POI {Id = 4, Name = "沃尔玛经六街店",C_ID=4,D_Name="萨尔图区",Address="大庆市萨尔图区经六街60号",Time="8:00-22:00",Tele="0459-8992888",Abstract="第一家沃尔玛购物广场于1988年3月1日在美国密苏里州的华盛顿开业。"}
        //};

        //public IEnumerable<POI> GetAllPois()
        //{
        //    return pois;
        //}

        //public IHttpActionResult GetPoi(int id)
        //{
        //    var poi = pois.FirstOrDefault((p) => p.Id == id);
        //    if (poi == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(poi);
        //}
        /// <summary>
        /// 为了得到所有的产品信息列表
        /// </summary>
        static readonly IPOIRepository repository = new POIRepository();
        public IEnumerable<POI> GetAllProducts()
        {
            return repository.GetAllPOIs();
        }
        /// <summary>
        /// 通过产品ID获取一个产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public POI GetByID(int id)
        {
            POI item = repository.GetByID(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        /// <summary>
        /// 按照类别查找产品信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<POI> GetProductsByCategory(string category)
        {
            return repository.GetAllPOIs().Where(
                p => string.Equals(p.C_ID.ToString(), category, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// 添加一个新的产品
        /// 这个方法的名字以“Post”开头，为了创建一个新的产品，这个客户端将发送一个HTTP Post请求。
        /// 这个方法采用类型为Product的参数。在Web Api中复杂类型的参数是从请求消息体中反序列化得来的。
        /// 因此，我们期望客户端发送xml或者Json格式的一个产品对象的序列化表现形式。
        /// 此实现会工作，但它还很不完整。理想情况下，我们希望的 HTTP 响应，包括以下内容：
        /// 响应代码：在默认情况下，这个Web API框架设置响应状态码为200（OK）。但是根据这个HTTP/1.1协议，当POST请求在创建一个资源时，这个服务端应该回复状态201(Created)。
        /// 位置：当服务端创建一个资源时，它应该在响应的Location标头中包含这个资源的URI。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage PostProduct(POI item)
        {
            item = repository.AddPOI(item);
            var response = Request.CreateResponse<POI>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        /// <summary>
        /// 通过PUT更新产品
        /// ID参数是从URI中获得的，product参数是从请求正文反序列化得来的
        /// </summary>
        /// <param name="id"></param>
        /// <param name="poi"></param>
        public void PutPOI(int id, POI poi)
        {
            poi.Id = id;
            if (!repository.UpdatePOI(poi))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        /// <summary>
        /// 删除产品
        /// 如果删除请求成功，它可以返回状态 200 (OK) 与实体的描述该状态 ；如果删除仍然挂起，则返回状态 202 （已接受）；或状态与没有实体正文 204 （无内容）。
        /// 在这种情况下， DeleteProduct方法具有void返回类型，因此 ASP.NET Web API 自动转换此状态代码 204 （无内容）。
        /// </summary>
        /// <param name="id"></param>
        public void DeletePOI(int id)
        {
            POI item = repository.GetByID(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.RemovePOI(id);
        }

    }
}
