using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class Admin
    {
        public string email { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public List<object> BAs { get; set; }
    }
}