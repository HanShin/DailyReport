using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelerData
    {
        public ModelerData()
        {
            ModelCount = new List<int>();
        }
        public string Name { get; set; }
        public List<int> ModelCount
        {
            get;
            set;
        }
    }
}