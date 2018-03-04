using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class LogModel
    {
        public string EventType { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int LogmaID { get; set; }
    }

    public class LogModelWeb
    {
        public string EventType { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int LogweID { get; set; }
    }
}