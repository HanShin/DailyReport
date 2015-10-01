using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class DR_MODELPROGRESS_RUN
    {
        public string OID { get; set; }
        public string RUN_NAME { get; set; }
        public string SPEC { get; set; }
        public string FLUID_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_DATE { get; set; }
    }
}