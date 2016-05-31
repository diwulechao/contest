using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public static string connectString = "Server=tcp:sqlserverwuditest.database.windows.net,1433;Data Source=sqlserverwuditest.database.windows.net;Initial Catalog=sqlinstancewuditest;Persist Security Info=False;User ID=diwulechao;Password=pwdforwudisQl3;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public ActionResult Index(string user)
        {
            if (string.IsNullOrEmpty(user))
                return View();
            ViewBag.User = user;

            var accountinfo = TokenController.getAllInternal(user);
            foreach (var p in accountinfo.accountlist)
            {
                p.token = "";
            }

            ViewBag.Data = accountinfo;
            return View("IndexWithUser");
        }

    }
}