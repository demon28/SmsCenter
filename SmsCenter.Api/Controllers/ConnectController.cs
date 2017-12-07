using SmsCenter.Facade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;

namespace SmsCenter.Api.Controllers
{
    public class ConnectController : Controller
    {
        // GET: Connect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sms(string interfacename)
        {
            //Log.Info("接收到请求");
            StreamReader reader = new StreamReader(Request.InputStream, Encoding.UTF8);
            string data = reader.ReadToEnd();
            //Log.Info("interfacename=" + interfacename + "; data=" + data);
            ApiRequestHandler api = new ApiRequestHandler();
            var result = api.Handler(interfacename, data);
            return Json(result);
        }
    }
}