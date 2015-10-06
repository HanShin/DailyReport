using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class DR_MODELPROGRESS_LINE
    {
        public string OID { get; set; }
        public string PIPELINE { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime? DATECREATED { get; set; }
        public string LASTMODIFIEDBY { get; set; }
        public DateTime? DATELASTMODIFIED { get; set; }
        public string PROJECT { get; set; }
    }
}