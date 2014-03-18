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
using NewAPITest.Models;

namespace NewAPITest.Controllers
{
    //只有Admin才能访问
    [Authorize(Roles = "Administrator")]
    public class AdminController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET api/Admin
        public IEnumerable<POI> GetPOIs()
        {
            return db.POIs.AsEnumerable();
        }

        // GET api/Admin/5
        public POI GetPOI(int id)
        {
            POI poi = db.POIs.Find(id);
            if (poi == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return poi;
        }

        // PUT api/Admin/5
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

        // POST api/Admin
        public HttpResponseMessage PostPOI(POI poi)
        {
            if (ModelState.IsValid)
            {
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

        // DELETE api/Admin/5
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