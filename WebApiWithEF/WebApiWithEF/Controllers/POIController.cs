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
    public class POIController : ApiController
    {
        private POIContext db = new POIContext();

        // GET api/POI
        // .Skip((1 - 1) * 5).Take(5) 分页
        // 使用Strathweb.CacheOutput.WebApi2 缓存机制
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<POI> GetPOI()
        {
            IEnumerable<POI> pois = db.POIs.SqlQuery("select * from POI where Status = 1 order by Updated desc").AsEnumerable();
            return pois;
        }

        // GET api/POI?type=type
        // 根据类型获取pois
        //[Route("api/poitype/{id}")]
        // 使用Strathweb.CacheOutput.WebApi2 缓存机制
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<POI> GetPOIByType(Int32 type)
        {
            IEnumerable<POI> pois = db.POIs.SqlQuery("select * from POI where Status = 1 and C_ID = " + type + " order by Updated desc").AsEnumerable();
            return pois;
        }

        // GET api/POI/5
        // 使用Strathweb.CacheOutput.WebApi2 缓存机制
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
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

        // POST api/POI
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

        // DELETE api/POI/5
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