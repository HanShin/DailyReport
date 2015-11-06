using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class LineList
    {
        public string OID { get; set; }
        public string PIPELINE { get; set; }
        public string DWGNO { get; set; }
        public string DESIGN_PRESS { get; set; }
        public string OPER_PRESS { get; set; }
        public string TEST_PRESS { get; set; }
        public string DESIGN_TEMP { get; set; }
        public string OPER_TEMP { get; set; }
        public string INSUL_CODE { get; set; }
        public string INSUL_THK { get; set; }
        public string PAINT_CODE { get; set; }
        public string RTPT { get; set; }
        public string PWHT { get; set; }
        public string TEST_MEDIUM { get; set; }
        public string STEAM_TRACE { get; set; }
        public string STEAM_JACKETING { get; set; }
        public string ELECTRIC_TRACE { get; set; }
        public string STRESS_ANALYSIS { get; set; }
        public string CLEANING { get; set; }
        public string PIPING_PID { get; set; }
        public string PIPING_PLAN { get; set; }
        public string PIPING_STRESS { get; set; }
    }
}