using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gitreader.data.git
{
    
    public class GitUser
    {
        public string Login { get; set; }
        public string Type { get; set; }
        public int id { get; set; }
        public string Url { get; set; }
        public string Html_url { get; set; }

    }
}