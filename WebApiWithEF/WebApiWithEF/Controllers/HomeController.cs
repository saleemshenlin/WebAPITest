using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiWithEF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to 旅游数据管理";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult POIManage()
        {
            //绑定API的Url
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "POI", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }
        public ActionResult POICreate()
        {
            //绑定API的Url
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "POI", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }

        public ActionResult POIEdit()
        {
            //绑定API的Url
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "POI", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
           
            string PoiId = Request.QueryString["id"];
            ViewBag.PoiId = PoiId;
            return View();
        }

        /// <summary>
        /// 验证C_ID
        /// </summary>
        //public class PriceAttribute : ValidationAttribute
        //{
        //    public double MinPrice { get; set; }

        //    public override bool IsValid(object value)
        //    {
        //        if (value == null)
        //        {
        //            return true;
        //        }
        //        var price = (double)value;
        //        if (price < MinPrice)
        //        {
        //            return false;
        //        }
        //        double cents = price - Math.Truncate(price);
        //        if (cents < 0.99 || cents >= 0.995)
        //        {
        //            return false;
        //        }

        //        return true;
        //    }

        //}

        //public class PriceValidator : DataAnnotationsModelValidator<PriceAttribute>
        //{
        //    double _minPrice;
        //    string _message;

        //    public PriceValidator(ModelMetadata metadata, ControllerContext context
        //      , PriceAttribute attribute)
        //        : base(metadata, context, attribute)
        //    {
        //        _minPrice = attribute.MinPrice;
        //        _message = attribute.ErrorMessage;
        //    }

        //    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        //    {
        //        var rule = new ModelClientValidationRule
        //        {
        //            ErrorMessage = _message,
        //            ValidationType = "price"
        //        };
        //        rule.ValidationParameters.Add("min", _minPrice);

        //        return new[] { rule };
        //    }
        //}
    }
}
