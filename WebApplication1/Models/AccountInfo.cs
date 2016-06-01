using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AccountInfo
    {
        public string useremail;
        public List<SubAccount> accountlist;

        public class SubAccount
        {
            public string useremail;
            public string token;
            public string userid;
            public string username;
            public string id;
            public string provider;
        }
    }
}