using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DailyReportOracleDB.Dal;
using DailyReport.Models;

namespace DailyReport.Controllers
{
    public partial class GridController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new DailyReport.Models.OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = DateTime.Now.AddDays(i),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult BulkBM()
        {
            return View();
        }

       

        public ActionResult Consistency()
        {
            return View();
        }

        

        public ActionResult Consistency_Read([DataSourceRequest]DataSourceRequest request)
        {
            var tempItem = db.DR_CONSISTENCY.Take(500).ToList();
            List<ConsistencyItem> result = new List<ConsistencyItem>();
            foreach (var item in tempItem)
            {
                ConsistencyItem consistency = new ConsistencyItem();
                consistency.PIPE_LINE = item.LINENO;
                foreach (var subitem in item.DR_CONSISTENCY_VALVE)
                {
                    if (string.IsNullOrEmpty(subitem.LONGMATERIALDESCRIPTION))
                    {
                        continue;
                    }
                    consistency.ITEMS += ",";
                    consistency.ITEMS +=  subitem.LONGMATERIALDESCRIPTION.Split(' ').First();
                    //consistency.ITEMS += subitem.LONGMATERIALDESCRIPTION.Substring(0, subitem.LONGMATERIALDESCRIPTION.IndexOf(" "));
                    subitem.DR_CONSISTENCY = null;
                }

                foreach (var subitem in item.DR_CONSISTENCY_INSTRUMENT)
                {
                    consistency.ITEMS += ",";
                    consistency.ITEMS += "SP_" + subitem.INS;
                    subitem.DR_CONSISTENCY = null;
                }

                foreach (var subitem in item.DR_CONSISTENCY_SPECIALTY)
                {
                    consistency.ITEMS += ",";
                    consistency.ITEMS += "SP_" + subitem.TAG;
                    subitem.DR_CONSISTENCY = null;
                }
                result.Add(consistency);
            }
            return Json(result.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        public ActionResult BulkBM_Read([DataSourceRequest]DataSourceRequest request)
        {
            // Item을 그룹화하여 Select
            var result = (from bulkbm in db.DR_BULKBM
                          group bulkbm by new { bulkbm.DWGNO, bulkbm.SPECNAME, bulkbm.INDUSTRYCOMMODITYCODE, bulkbm.PART, bulkbm.LONGMATERIALDESCRIPTION, bulkbm.COMMENTS, bulkbm.PARTNUMBER, bulkbm.FABRICATIONTYPE} into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new BMItem
                          {
                              // SEP용 JOB_NO = 110670 (다른 프로젝트들이 추가될때 변경)
                              JOB_NO = "110670",
                              DWG_NO = g.Key.DWGNO,
                              CLASS_CD = g.Key.SPECNAME,
                              SYMBOL_CD = g.Key.COMMENTS,
                              BM_CD = g.Key.INDUSTRYCOMMODITYCODE.Substring(0,8),
                              MAIN_THK = g.Key.INDUSTRYCOMMODITYCODE.Substring(8,2),
                              SUB_THK = g.Key.INDUSTRYCOMMODITYCODE.Substring(10,2),
                              SIZE_UNIT = "in",
                              DWG_BM_QTY = dataCount,
                              FS_GUBN = g.Key.FABRICATIONTYPE,
                              INDUSTRYCOMMODITYCODE = g.Key.INDUSTRYCOMMODITYCODE,
                              PARTNUMBER = g.Key.PARTNUMBER,
                              LONGMATERIALDESCRIPTON = g.Key.LONGMATERIALDESCRIPTION
                          }).ToList();

            foreach (var item in result)
            {
                // FS_GUBN은 고정값 왜 이렇게 들어가는지는 모름
                switch (item.FS_GUBN)
                {
                    case "7": item.FS_GUBN = "F";
                        break;
                    case "15": item.FS_GUBN = "S";
                        break;

                }
                string ItemType = item.PARTNUMBER.Substring(8, 2);
                string schCode = getSchCode(ItemType);
                item.PARTNUMBER = item.PARTNUMBER.Replace(item.INDUSTRYCOMMODITYCODE, "");
                item.PARTNUMBER = item.PARTNUMBER.Replace(schCode, "");

                int portCount = getPortCount(item.LONGMATERIALDESCRIPTON);



            }
            return Json(result.ToDataSourceResult(request)) ;
        }

        /// <summary>
        /// Type에 따라 Port가 몇개인지 체크 대부분의 타입은 2개
        /// </summary>
        /// <param name="descrip"></param>
        /// <returns></returns>
        public static int getPortCount(string descrip)
        {
            int portCount = 2;
            if (descrip.Contains("CAP") || descrip.Contains("PLUG") || descrip.Contains("BLIND FLANGE"))
            {
                portCount = 1;
            }
            else if (descrip.Contains("TEE")  || descrip.Contains("LATERAL"))
            {
                portCount = 3;
            }
            return portCount;
        }


        /// <summary>
        /// Type에 따라 schCode를 가져온다.
        /// </summary>
        /// <param name="ItemType"></param>
        /// <returns></returns>
        public static string getSchCode(string ItemType)
        {
            string schCode = string.Empty;
            switch (ItemType)
            {
                case "1M": schCode = "S-40";
                    break;
                case "1S": schCode = "S-80";
                    break;
                case "1E": schCode = "S-160";
                    break;
                case "MM": schCode = "GRV-10.3";
                    break;
                case "1J": schCode = "S-30";
                    break;
                case "M2": schCode = "GRE";
                    break;
                case "1W": schCode = "S-40S";
                    break;
                case "1U": schCode = "S-XXS";
                    break;
                case "17": schCode = "S-STD";
                    break;
                case "1G": schCode = "S-20";
                    break;
                case "1V": schCode = "S-10S";
                    break;
                case "18": schCode = "S-10";
                    break;
                case "1Y": schCode = "S-80S";
                    break;
                case "1R": schCode = "S-80";
                    break;
                case "1T": schCode = "S-XS";
                    break;
                case "1H": schCode = "S-20";
                    break;
                case "1L": schCode = "S-40";
                    break;
                case "1Q": schCode = "S-60";
                    break;
                case "LV": schCode = "GRV-10";
                    break;
                case "IW": schCode = "S-40S";
                    break;
                case "6B": schCode = string.Empty;
                    break;
                case "1N": schCode = "S-40";
                    break;
                case "TA": schCode = "GRE";
                    break;
                case "07": schCode = "0.688";
                    break;
                case "1I": schCode = "S-30";
                    break;
                case "4X": schCode = "0.688";
                    break;
                case "06": schCode = "0.625";
                    break;
                case "05": schCode = "0.562";
                    break;
                case "M1": schCode = "GRV-10.3";
                    break;
                case "IX": schCode = "0.752";
                    break;
            }
            return schCode;
        }
    }
}