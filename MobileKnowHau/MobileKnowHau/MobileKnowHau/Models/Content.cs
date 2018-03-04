using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKnowHau.Models
{
    public class Content
    {
        public int contentID { get; set; }
        public string contentmsg { get; set; }
        public string beaconID { get; set; }
        public object BEACON { get; set; }
        public List<object> HISTORICs { get; set; }

        public static implicit operator List<object>(Content v)
        {
            throw new NotImplementedException();
        }
    }
}
