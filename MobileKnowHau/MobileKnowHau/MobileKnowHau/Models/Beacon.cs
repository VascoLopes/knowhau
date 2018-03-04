using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKnowHau.Models
{
    public class Beacon
    {
        public string beaconID { get; set; }
        public int majorvalue { get; set; }
        public int minorvalue { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public List<object> BAs { get; set; }
        public List<object> CONTENTs { get; set; }
    }
}
