using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class Content
    {
        public int contentID { get; set; }
        public string contentmsg { get; set; }
        public string beaconID { get; set; }
        public object BEACON { get; set; }
        public List<object> HISTORICs { get; set; }
    }
}