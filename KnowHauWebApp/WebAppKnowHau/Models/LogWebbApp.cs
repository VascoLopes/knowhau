using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class LogWebbApp
    {
        public string eventtype { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public int logwaID { get; set; }
    }
}