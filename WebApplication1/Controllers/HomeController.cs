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

        public async Task<ActionResult> cb(string code, string useremail,string provider)
        {
            using (var client = new HttpClient())
            {
                var s1 = await client.GetStringAsync("https://graph.facebook.com/v2.3/oauth/access_token?client_id=619387381484849&redirect_uri=http://webapplication16742.azurewebsites.net/home/cb?useremail=" + useremail + "&client_secret=1f22ecd5cfa27759fbf126531994531c&code=" + code);
                TokenClass token = JsonConvert.DeserializeObject<TokenClass>(s1);
                //accesstoken = token.access_token;
                var ac = new WebApplication1.Models.AccountInfo.SubAccount();
                ac.provider = provider;
                ac.token = token.access_token;
                ac.useremail = useremail;
                ac.userid = "unknow";

                TokenController.addTokenInternal(ac);
            }

            return Redirect("/home/index?user=" + useremail);
        }
    }
}