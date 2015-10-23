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

        public ActionResult ProgressPipingLineList()
        {
            return View();
        }

        public ActionResult Consistency()
        {
            return View();
        }

        public ActionResult ProgressPipingLineList_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_PROGRESS_PIPINGLINELIST.ToList();

            return Json(result.ToDataSourceResult(request));
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
            var result = db.DR_BULKBM.ToList();
            return Json(result.ToDataSourceResult(request)) ;
        }
    }
}