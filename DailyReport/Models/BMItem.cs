using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.Models
{
    public class BMItem
    {
        public string JOB_NO { get; set; }
        public string DWG_NO { get; set; }
        public string CLASS_CD { get; set; }
        public string SYMBOL_CD { get; set; }
        public string BM_CD { get; set; }
        public string MAIN_THK { get; set; }
        public string SUB_THK { get; set; }
        public string SIZE_UNIT { get; set; }
        public string MAIN_SIZE { get; set; }
        public string SUB_SIZE { get; set; }
        public int DWG_BM_QTY { get; set; }
        public string FS_GUBN { get; set; }
        public string INDUSTRYCOMMODITYCODE { get; set; }
        public string PARTNUMBER { get; set; }
        public string LONGMATERIALDESCRIPTON { get; set; }
    }
}