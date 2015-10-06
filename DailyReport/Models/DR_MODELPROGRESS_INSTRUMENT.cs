using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class DR_MODELPROGRESS_INSTRUMENT
    {
        public string OID { get; set; }
        public string INST { get; set; }
        public double E { get; set; }
        public double N { get; set; }
        public double EL { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime? DATECREATED { get; set; }
        public string LASTMODIFIEDBY { get; set; }
        public DateTime? DATELASTMODIFIED { get; set; }
    }
}