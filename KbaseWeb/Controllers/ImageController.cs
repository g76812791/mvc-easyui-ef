using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KbaseData;
using Entity;
using System.IO;
using KbaseWeb.Filters;
using System.Drawing;


namespace KbaseWeb.Controllers
{
    public class ImageController : Controller
    {
        BaseDal<image> imgdal = new BaseDal<image>();
        [HttpPost]
        public ActionResult Add(string command, string type, HttpPostedFile upload)
        {
            var uploads = Request.Files["upload"];
            image img = new image();
            using (MemoryStream stream = new MemoryStream())
            {
                uploads.InputStream.CopyTo(stream);
                img.Img = stream.ToArray();
            }
            img.Name = uploads.FileName;
            img.ContentType = uploads.ContentType;
            var res = imgdal.Add(img);
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
            var url = "/Image/get.do?id=" + res.ID;
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }
       
        [HttpGet]
        public FileResult Get(int id)
        {
            var respond = imgdal.GetOne(p => p.ID == id);
            if (respond != null)
            {
                return File(respond.Img, respond.ContentType);
            }
            else
                return null;
        }
    }
}
