using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        /// 上传图片
        /// </summary>
        /// <param name="upImg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {
            string fileName = System.IO.Path.GetFileName(upImg.FileName.Substring(0, upImg.FileName.Length-4));
            string newName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            string newMiniName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_mini.jpg";
            string filePhysicalPath = Server.MapPath("~/Update/" + newName);
            string fileMiniPath = Server.MapPath("~/Update/" + newMiniName);
            string pic = "", error = "";
            Image oImg = Image.FromStream(upImg.InputStream);
            Bitmap nImg = new Bitmap(720, 540);
            Graphics graphics = Graphics.FromImage(nImg);
            graphics.Clear(Color.White);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.DrawImage(oImg, new Rectangle(0, 0, 720, 540));
            graphics.Dispose();
            oImg.Dispose();
            Image miniImg = ResetImageSize(oImg, 200, 150);
            try
            {
                nImg.Save(filePhysicalPath);
                nImg.Dispose();
                miniImg.Save(fileMiniPath);
                miniImg.Dispose();
                //upImg.SaveAs(filePhysicalPath);
                pic = "/Update/" + fileMiniPath;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new
            {
                pic = pic,
                error = error
            });
        }

        /// <summary>
        /// 一个空回调方法,ResetImageSize()方法调用
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool ImageCallback()
        {
            return false;
        }


        /// <summary>
        /// 返回此 Image 对象的缩略图  
        /// </summary>
        /// <param name="AtImage">要缩放的图象对象</param>
        /// <param name="ImageWidth">指定新的图象宽度</param>
        /// <param name="ImageHeight">指定新的图象高度</param>
        /// <returns></returns>
        public static Image ResetImageSize(Image AtImage, int ImageWidth, int ImageHeight)
        {
            if (AtImage == null) return null;
            Image.GetThumbnailImageAbort MyCallback = new Image.GetThumbnailImageAbort(ImageCallback);
            Image Img = null;
            Img = AtImage.GetThumbnailImage(ImageWidth, ImageHeight, MyCallback, IntPtr.Zero);
            return Img;
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
