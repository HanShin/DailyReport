using System;
using System.Data.Entity;

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

    public class DR_MODELPROGRESS_LINE_DBContext : DbContext 
    {
        public DbSet<DR_MODELPROGRESS_LINE> DR_MODELPROGRESS_LINES { get; set; } 
    }
}