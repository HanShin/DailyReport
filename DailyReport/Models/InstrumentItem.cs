using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class InstrumentItem
    {
        public DateTime? Date { get; set; }
        public int CreatedCount { get; set; }
        public string CreatedItem { get; set; }
        public int ModifiedCount { get; set; }
        public string ModifiedItem { get; set; }
    }
}