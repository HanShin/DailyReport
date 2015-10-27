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

        // TODO
        public ActionResult BulkBM_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from bulkbm in db.DR_BULKBM
                          group bulkbm by new { bulkbm.DWGNO, bulkbm.SPECNAME, bulkbm.INDUSTRYCOMMODITYCODE, bulkbm.PART, bulkbm.LONGMATERIALDESCRIPTION, bulkbm.COMMENTS, bulkbm.PARTNUMBER, bulkbm.FABRICATIONTYPE} into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new BMItem
                          {
                              JOB_NO = "110670",
                              DWG_NO = g.Key.DWGNO,
                              CLASS_CD = g.Key.SPECNAME,
                              SYMBOL_CD = g.Key.COMMENTS,
                              BM_CD = g.Key.INDUSTRYCOMMODITYCODE.Substring(0,8),
                              MAIN_THK = g.Key.INDUSTRYCOMMODITYCODE.Substring(8,2),
                              SUB_THK = g.Key.INDUSTRYCOMMODITYCODE.Substring(10,2),
                              DWG_BM_QTY = dataCount,
                              FS_GUBN = g.Key.FABRICATIONTYPE,
                              INDUSTRYCOMMODITYCODE = g.Key.INDUSTRYCOMMODITYCODE,
                              PARTNUMBER = g.Key.PARTNUMBER
                          }).ToList();
            return Json(result.ToDataSourceResult(request)) ;
        }
    }
}