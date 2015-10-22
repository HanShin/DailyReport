using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelLineNumData
    {
         public ModelLineNumData(int wOperator, int sOperator, int kOperator, int total,
            int wPercentage, int sPercentage, int kPercentage , int tPercentage,
            int mPercentage, int mTotal)
        {
            Woperator = wOperator;
            Soperator = sOperator;
            Koperator = kOperator;
            Total = total;
            Wpercentage = wPercentage;
            Spercentage = sPercentage;
            Kpercentage = kPercentage;
            Tpercentage = tPercentage;
            Mpercentage = mPercentage;
            Mtotal = mTotal;
        }
        public int Woperator { get; set; }
        public int Soperator { get; set; }
        public int Koperator { get; set; }
        public int Total { get; set; }
        public int Wpercentage { get; set; }
        public int Spercentage { get; set; }
        public int Kpercentage { get; set; }
        public int Tpercentage { get; set; }
        public int Mpercentage { get; set; }
        public int Mtotal { get; set; }
    }
}