using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class afterloginController : ApiController
    {
        public IHttpActionResult GetProduct(string code)
        {
            return Ok(code);
        }

    }
}
