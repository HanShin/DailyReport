using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelCountData
    {
        public ModelCountData()
        {
            ModelCount = new List<int>();
        }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public List<int> ModelCount
        {
            get;
            private set;
        }
    }
}