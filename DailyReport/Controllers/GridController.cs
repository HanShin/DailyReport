using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DailyReportOracleDB.Dal;

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

        public ActionResult ModelProgressLine()
        {
            return View();
        }

        public ActionResult ModelProgressRun()
        {
            return View();
        }

        public ActionResult ModelProgressPart()
        {
            return View();
        }

        public ActionResult ModelProgressInstrument()
        {
            return View();
        }

        public ActionResult ModelProgressLine_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_LINE.ToList();

            return Json(result.ToDataSourceResult(request));
        }

  
        public ActionResult ModelProgressRun_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_RUN.ToList();

            return Json(result.ToDataSourceResult(request));
        }

        // TODO
        public ActionResult ModelProgressPart_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_PART.ToList();

            return Json(result.ToDataSourceResult(request));
        }

        // TODO
        public ActionResult ModelProgressInstrument_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_INSTRUMENT.ToList();

            return Json(result.ToDataSourceResult(request));
        }
    }
}