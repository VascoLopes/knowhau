using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class Utilizador
    {
        public string name { get; set; }
        public string genre { get; set; }
        public string username { get; set; }
        public DateTime birthdate { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<object> HISTORICs { get; set; }
    }
}