using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using DailyReportOracleDB.Dal;
using Kendo.Mvc.UI;
using DailyReport.Models;

namespace DailyReport.Controllers
{
    public class ChartController : Controller
    {
        private Entities db = new Entities();
        [HttpPost]
        public ActionResult ModelProgressLine_Chart([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from lines in db.DR_MODELPROGRESS_LINE
                     where lines.DATECREATED >= new DateTime(2014, 11, 1) && lines.DATECREATED <= new DateTime(2014, 12, 31)
                     group lines by lines.DATECREATED into g
                     let dataCount = g.Count()
                      select new ModelProgressLineDatas
                     {
                         Count = dataCount,
                         Date = g.Key
                     }).ToList();

            //var result = db.DR_MODELPROGRESS_LINE.Where(line => line.DATECREATED.Value >= new DateTime(2014, 11, 1) && line.DATECREATED.Value <= new DateTime(2014, 12, 31)).GroupBy(line => line.DATECREATED).ToList();
            return Json(result.ToDataSourceResult(request));
        }

    }
}
