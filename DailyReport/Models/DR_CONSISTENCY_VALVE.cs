using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class DR_CONSISTENCY_VALVE
    {
        public string OID { get; set; }
        public string PIPELINE { get; set; }
        public string ITEMNAME { get; set; }
        public string INDUSTRYCOMMODITYCODE { get; set; }
        public string NPD { get; set; }
        public string LONGMATERIALDESCRIPTION { get; set; }
    }
}