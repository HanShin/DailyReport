﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class LineListItem
    {
        public DateTime? Date { get; set; }
        public string Modeler { get; set; }
        public int Count { get; set; }
    }
}