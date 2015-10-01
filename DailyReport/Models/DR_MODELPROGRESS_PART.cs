using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class DR_MODELPROGRESS_PART
    {
        public string OID { get; set; }
        public string PART_NAME { get; set; }
        public string COMMODITY_CODE { get; set; }
        public string SHORT_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_DATE { get; set; }
    }
}