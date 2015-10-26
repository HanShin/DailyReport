using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelLineNumData
    {
        public ModelLineNumData(int sQuantity, int wQuantity, int kQuantity, int total,
           int wPercentage, int sPercentage, int kPercentage, int tPercentage,
           int mPercentage, int mTotal)
        {
            Squantity = sQuantity;
            Wquantity = wQuantity;
            Kquantity = kQuantity;
            Total = total;
            Wpercentage = wPercentage;
            Spercentage = sPercentage;
            Kpercentage = kPercentage;
            Tpercentage = tPercentage;
            Mpercentage = mPercentage;
            Mtotal = mTotal;
        }
        public int Squantity { get; set; }
        public int Wquantity { get; set; }
        public int Kquantity { get; set; }
        public int Total { get; set; }
        public int Wpercentage { get; set; }
        public int Spercentage { get; set; }
        public int Kpercentage { get; set; }
        public int Tpercentage { get; set; }
        public int Mpercentage { get; set; }
        public int Mtotal { get; set; }

        // ////카테고리 그룹으로 나눌때
        //public ModelLineNumData(string col, int percentage, int mTotal, int total)
        //{
        //    Col = col;
        //    Percentage = percentage;
        //    Mtotal = mTotal;
        //    Total = total;
        //}
        //public string Col { get; set; }
        //public int Percentage { get; set; }
        //public int Mtotal { get; set; }
        //public int Total { get; set; }
    }
}