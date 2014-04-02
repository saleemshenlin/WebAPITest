using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApiWithEF.Controllers
{
    /// <summary>
    /// WebAPI后台管理Controller
    /// 通过调用Web API管理poi的增删查改
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 后台管理首页
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to 旅游数据管理";

            return View();
        }
        /// <summary>
        /// 后台管理关于页(无实用)
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        /// <summary>
        /// 后台管理联系页(无实用)
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 后台管理poi列表页
        /// 包括增删查改poi
        /// </summary>
        /// <remarks>参数apiUri：绑定API的Url，绑定到页面供js调用</remarks>
        /// <remarks>参数odataApiUri：oData Url暂时无使用</remarks>
        /// <returns>页面</returns>
        public ActionResult POIManage()
        {
            //绑定API的Url
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "POI", });
            //oData Url
            string odataApiUri = "/odata/POIOData";
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }
        /// <summary>
        /// 后台管理poi新增页
        /// 新建poi
        /// </summary>
        /// <remarks>参数apiUri：绑定API的Url，绑定到页面供js调用</remarks>
        /// <returns>页面</returns>
        public ActionResult POICreate()
        {
            //绑定API的Url
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "POI", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }
        /// <summary>
        /// 后台管理poi编辑页
        /// 编辑已有信息
        /// </summary>
        /// <remarks>参数apiUri：绑定API的Url，绑定到页面供js调用</remarks>
        /// <remarks>需要传入poi的id，以便前端调用api查询</remarks>
        /// <returns>页面</returns>
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
        /// <param name="upImg">上传图片</param>
        /// <remarks>上传图片宽度大于720px，并且高度大于540px</remarks>
        /// <remarks>没有超过尺寸直接退回</remarks>
        /// <remarks>超过尺寸对图进行切割720*540</remarks>
        /// <remarks>保存到"~/Update/"</remarks>
        /// <returns>json格式的图片链接</returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {
            string fileName = MD5Create(System.IO.Path.GetFileName(upImg.FileName.Substring(0, upImg.FileName.Length - 4)) + DateTime.Now.ToString("yyyyMMddHHmmss"));
            string newName = fileName + ".jpg";
            string newMiniName = fileName + "_mini.jpg";
            string filePhysicalPath = Server.MapPath("~/Update/" + newName);
            string fileMiniPath = Server.MapPath("~/Update/" + newMiniName);
            string pic = "", error = "";
            Image oImg = Image.FromStream(upImg.InputStream);
            int sourceWidth = oImg.Width;
            int sourceHeight = oImg.Height;
            if (sourceHeight < 540 || sourceWidth < 720)
            {
                error = "上传图片宽度大于720px，并且高度大于540px！";
            }
            else
            {
                float nPercentH = ((float)540 / (float)sourceHeight);
                int destX = (int)((sourceWidth * nPercentH) - 720);
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
            }
            return Json(new
            {
                pic = pic,
                error = error
            });
        }
        /// <summary>
        /// 为图片的名称进行MD5加密
        /// </summary>
        /// <param name="STR">为待加密的string</param>
        /// <returns>加密的图片名称</returns>
        private string MD5Create(string STR)
        {
            string pwd = "";
            //pwd为加密结果
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(STR));
            //这里的UTF8是编码方式，你可以采用你喜欢的方式进行，比如UNcode等等
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString();
            }
            return pwd;
        }
    }
}
