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
        public static string accesstoken;

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About(string code)
        {
            using (var client = new HttpClient())
            {
                var s1 = await client.GetStringAsync("https://graph.facebook.com/v2.3/oauth/access_token?client_id=619387381484849&redirect_uri=http://webapplication16742.azurewebsites.net/home/about&client_secret=1f22ecd5cfa27759fbf126531994531c&code=" + code);
                TokenClass token = JsonConvert.DeserializeObject<TokenClass>(s1);
                accesstoken = token.access_token;
                var tp = await client.GetStringAsync("https://graph.facebook.com/v2.6/me/posts?access_token="+accesstoken);
                PostData data = JsonConvert.DeserializeObject<PostData>(tp);
                var ret = "";
                if (data.data.Count >= 1)
                {
                    ret = data.data[0].message;
                }
                else ret = accesstoken;

                ViewBag.Message = ret;
            }

            return View();
        }
    }
}