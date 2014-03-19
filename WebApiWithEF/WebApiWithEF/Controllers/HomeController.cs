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
            string fileName = System.IO.Path.GetFileName(upImg.FileName.Substring(0, upImg.FileName.Length - 4));
            string newName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            string newMiniName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_mini.jpg";
            string filePhysicalPath = Server.MapPath("~/Update/" + newName);
            string fileMiniPath = Server.MapPath("~/Update/" + newMiniName);
            string pic = "", error = "";
            Image oImg = Image.FromStream(upImg.InputStream);
            int sourceWidth = oImg.Width;
            int sourceHeight = oImg.Height;
            float nPercentH = ((float)540 / (float)sourceHeight);
            int destX = (int)( (sourceWidth * nPercentH) - 720);
            Bitmap nImg = new Bitmap(720, 540);
            Graphics graphics = Graphics.FromImage(nImg);
            graphics.Clear(Color.White);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.DrawImage(oImg, new Rectangle(0, 0, 720, 540), new Rectangle(destX, 0, sourceHeight * 4 / 3, sourceHeight), GraphicsUnit.Pixel);
            graphics.Dispose();
            oImg.Dispose();
            Bitmap miniImg = new Bitmap(nImg, 200, 150);
            try
            {
                nImg.Save(filePhysicalPath);
                nImg.Dispose();
                miniImg.Save(fileMiniPath);
                miniImg.Dispose();
                //upImg.SaveAs(filePhysicalPath);
                pic = "/Update/" + newMiniName;
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
    }
}
