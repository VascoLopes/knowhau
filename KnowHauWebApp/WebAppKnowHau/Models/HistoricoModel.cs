﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class HistoricoModel
    {
        public int historicID { get; set; }
        public int contentID { get; set; }
        public string userMAIL { get; set; }
        public DateTime date { get; set; }
        public object CONTENT { get; set; }
        public object USERM { get; set; }
    }
}