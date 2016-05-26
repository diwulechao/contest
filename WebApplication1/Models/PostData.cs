using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PostData
    {
        public List<SingleData> data;
        public PagingData paging;
    }

    public class SingleData
    {
        public string message;
        public string created_time;
        public string id;
    }
    public class PagingData
    {
        public string previous;
        public string next;
    }
}