using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public async Task<ActionResult> fb(string code, string useremail)
        {
            using (var client = new HttpClient())
            {
                var redirecturl = HttpUtility.UrlEncode("http://webapplication16742.azurewebsites.net/auth/fb/" + HttpUtility.UrlEncode(useremail));
                var s1 = await client.GetStringAsync("https://graph.facebook.com/v2.3/oauth/access_token?client_id=619387381484849&redirect_uri=" + redirecturl + "&client_secret=1f22ecd5cfa27759fbf126531994531c&code=" + code);
                TokenClass token = JsonConvert.DeserializeObject<TokenClass>(s1);
                //accesstoken = token.access_token;
                var ac = new WebApplication1.Models.AccountInfo.SubAccount();
                ac.provider = "fb";
                ac.token = token.access_token;
                ac.useremail = useremail;
                ac.userid = "unknow";

                TokenController.addTokenInternal(ac);
            }

            return Redirect("/home/index?user=" + useremail);
        }
    }
}