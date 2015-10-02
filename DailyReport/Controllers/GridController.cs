using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DailyReport.Models;

namespace DailyReport.Controllers
{
	public partial class GridController : Controller
    {
		public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
		{
			var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
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
            var result = Enumerable.Range(0, 50).Select(i => new DR_MODELPROGRESS_LINE
            {
                OID = "ABCDEFG"+ i,
                LINE_NO = "HHSIRJADU" + i * 10,
                CREATED_BY = "HanShin",
                CREATED_DATE = DateTime.Now.AddDays(-(2 * i)),
                MODIFIED_BY = "Modeler",
                MODIFIED_DATE = DateTime.Now.AddDays(-(i))
            });

            return Json(result.ToDataSourceResult(request));
        }
        
        // TODO
        public ActionResult ModelProgressRun_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new DR_MODELPROGRESS_RUN
            {
                OID = "ABCDEFG" + i,
                RUN_NAME = "RUNAAA" + i,
                FLUID_CODE = "fsdfsgJADU" + i * 10,
                SPEC = "SPECSHFFDF" + i * 10,
                CREATED_BY = "HanShin",
                CREATED_DATE = DateTime.Now.AddDays(-(2 * i)),
                MODIFIED_BY = "Modeler",
                MODIFIED_DATE = DateTime.Now.AddDays(-(i))
            });

            return Json(result.ToDataSourceResult(request));
        }

        // TODO
        public ActionResult ModelProgressPart_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new DR_MODELPROGRESS_PART
            {
                OID = "ABCDEFG" + i,
                PART_NAME = "PART_HHSIRJADU" + i * 10,
                CREATED_BY = "HanShin",
                CREATED_DATE = DateTime.Now.AddDays(-(2 * i)),
                MODIFIED_BY = "Modeler",
                MODIFIED_DATE = DateTime.Now.AddDays(-(i))
            });

            return Json(result.ToDataSourceResult(request));
        }

        // TODO
        public ActionResult ModelProgressInstrument_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new DR_MODELPROGRESS_INSTRUMENT
            {
                OID = "ABCDEFG" + i,
                PART_NAME = "HHSIRJADU" + i * 10,
                COORDINATE = "COORDNATE_" + (i * 10),
                CREATED_BY = "HanShin",
                CREATED_DATE = DateTime.Now.AddDays(-(2 * i)),
                MODIFIED_BY = "Modeler",
                MODIFIED_DATE = DateTime.Now.AddDays(-(i))
            });

            return Json(result.ToDataSourceResult(request));
        }
	}
}