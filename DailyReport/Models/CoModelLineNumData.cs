using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class CoModelLineNumData
    {
        public CoModelLineNumData(string col, int percentage, int quantity, int total)
        {
            Col = col;
            Percentage = percentage;
            Quantity = quantity;
            Total = total;
        }
        public string Col { get; set; }
        public int Percentage { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}