using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelCountData
    {
        public ModelCountData()
        {
            Date = new List<string>();
            Modeler = new List<ModelerData>();
        }

        public List<string> Date { get; set; }
        public List<ModelerData> Modeler { get; set; }
    }
}