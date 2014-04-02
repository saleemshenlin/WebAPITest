using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiWithEF.Models;
using WebApiWithEF.DAL;
using WebApi.OutputCache.V2;

namespace WebApiWithEF.Controllers
{
    /// <summary>
    /// poi的Web API Controller
    /// 用于对数据库进行增删查改
    /// </summary>
    public class POIController : ApiController
    {
        /// <summary>
        /// poi数据层的实例化
        /// </summary>
        private POIContext db = new POIContext();

        // GET api/POI
        // .Skip((1 - 1) * 5).Take(5) 分页
        /// <summary>
        /// 获取状态为非删除的poi的列表
        /// 按poi的修改时间降序排列
        /// 链接 GET ~/api/POI 
        /// </summary>
        /// <remarks>分页要添加.Skip((1 - 1) * 5).Take(5) </remarks>
        /// <returns>多个poi信息的IEnumerable</returns>
        public IEnumerable<POI> GetPOI()
        {
            IEnumerable<POI> pois = db.POIs.SqlQuery("select * from POI where Status = 1 order by Updated desc").AsEnumerable();
            return pois;
        }

        /// <summary>
        /// 根据poi的id获取poi信息
        /// 链接 GET ~/api/POI/{id}
        /// </summary>
        /// <param name="id">poi的id</param>
        /// <returns>POI类的单个poi信息</returns>
        public POI GetPOI(int id)
        {
            POI poi = db.POIs.Find(id);
            if (poi == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return poi;
        }

        // PUT api/POI/5
        /// <summary>
        /// 根据poi的id更新poi信息
        /// 链接 PUT ~/api/POI/{id}
        /// </summary>
        /// <param name="id">poi的id</param>
        /// <param name="poi">需要更新的poi信息</param>
        /// <remarks>如果是删除poi，则更新poi的Status字段</remarks>
        /// <returns>HTTP响应消息，更新成功或者失败</returns>
        public HttpResponseMessage PutPOI(int id, POI poi)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != poi.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            poi.Updated = DateTime.Now;
            db.Entry(poi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        
        /// <summary>
        /// 新建poi信息
        /// 链接 POST ~/api/POI
        /// </summary>
        /// <param name="poi">新建的poi信息</param>
        /// <returns>HTTP响应消息，添加成功或者失败</returns>
        public HttpResponseMessage PostPOI(POI poi)
        {
            if (ModelState.IsValid)
            {
                poi.Status = 1;
                poi.Updated = DateTime.Now;
                poi.Created = DateTime.Now;
                db.POIs.Add(poi);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, poi);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = poi.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /// <summary>
        /// 彻底从数据库删除poi（无使用）
        /// </summary>
        /// <param name="id">poi的id</param>
        /// <remarks>如果是删除poi，则请更新poi的Status字段</remarks>
        /// <returns>HTTP响应消息，删除成功或者失败</returns>
        public HttpResponseMessage DeletePOI(int id)
        {
            POI poi = db.POIs.Find(id);
            if (poi == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.POIs.Remove(poi);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, poi);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}