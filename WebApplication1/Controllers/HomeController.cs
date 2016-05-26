using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About(string code)
        {
            using (var client = new HttpClient())
            {
                var ret = await client.GetStringAsync("https://graph.facebook.com/v2.3/oauth/access_token?client_id=619387381484849&redirect_uri=http://webapplication16742.azurewebsites.net/home/about&client_secret=1f22ecd5cfa27759fbf126531994531c&code=" + code);
                ViewBag.Message = ret;
            }

            return View();
        }
    }
}