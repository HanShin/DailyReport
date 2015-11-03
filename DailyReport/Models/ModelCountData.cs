using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelCountData
    {
        public DateTime? Date { get; set; }
        public IEnumerable<string> Modeler { get; set; }
        public int Count { get; set; }
    }
}