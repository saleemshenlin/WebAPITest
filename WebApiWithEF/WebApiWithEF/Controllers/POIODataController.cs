using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebApiWithEF.Models;
using WebApiWithEF.DAL;

namespace WebApiWithEF.Controllers
{
    /*
    若要为此控制器添加路由，请将这些语句合并到 WebApiConfig 类的 Register 方法中。请注意 OData URL 区分大小写。

    using System.Web.Http.OData.Builder;
    using WebApiWithEF.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<POI>("POIOData");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class POIODataController : ODataController
    {
        private POIContext db = new POIContext();

        // GET odata/POIOData
        [Queryable]
        public IQueryable<POI> GetPOIOData()
        {
            return db.POIs;
        }

        // GET odata/POIOData(5)
        [Queryable]
        public SingleResult<POI> GetPOI([FromODataUri] int key)
        {
            return SingleResult.Create(db.POIs.Where(poi => poi.Id == key));
        }

        // PUT odata/POIOData(5)
        public IHttpActionResult Put([FromODataUri] int key, POI poi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != poi.Id)
            {
                return BadRequest();
            }

            db.Entry(poi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POIExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(poi);
        }

        // POST odata/POIOData
        public IHttpActionResult Post(POI poi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.POIs.Add(poi);
            db.SaveChanges();

            return Created(poi);
        }

        // PATCH odata/POIOData(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<POI> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            POI poi = db.POIs.Find(key);
            if (poi == null)
            {
                return NotFound();
            }

            patch.Patch(poi);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POIExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(poi);
        }

        // DELETE odata/POIOData(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            POI poi = db.POIs.Find(key);
            if (poi == null)
            {
                return NotFound();
            }

            db.POIs.Remove(poi);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool POIExists(int key)
        {
            return db.POIs.Count(e => e.Id == key) > 0;
        }
    }
}
