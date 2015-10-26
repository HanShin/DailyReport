﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class ModelLineNumData
    {
        public ModelLineNumData(string col, int percentage, int mTotal, int total)
        {
            Col = col;
            Percentage = percentage;
            Mtotal = mTotal;
            Total = total;
        }
        public string Col { get; set; }
        public int Percentage { get; set; }
        public int Mtotal { get; set; }
        public int Total { get; set; }
    }
}