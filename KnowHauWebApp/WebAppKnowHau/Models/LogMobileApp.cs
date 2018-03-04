using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class LogMobileApp
    {
        public string eventtype { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public int logmaID { get; set; }
    }
}