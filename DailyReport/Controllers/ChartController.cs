using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using DailyReportOracleDB.Dal;
using Kendo.Mvc.UI;
using DailyReport.Models;
using System.Data.Objects.SqlClient;

namespace DailyReport.Controllers
{
    public class ChartController : Controller
    {
        private Entities db = new Entities();

        private class tempClass
        {
            public Nullable<DateTime> Date { get; set; }
            public int Count { get; set; }
        }

        public ActionResult ModelProgressLine_Chart([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from lines in db.DR_MODELPROGRESS_LINE

                          group lines by lines.DATECREATED into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new tempClass
                         {
                             Count = dataCount,
                             Date = g.Key
                         }).ToList();
            return Json(result);
        }

        public ActionResult ModelProgressRun_Chart([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from run in db.DR_MODELPROGRESS_RUN
                        group run by run.DATECREATED into g
                        let dataCount = g.Count()
                        orderby g.Key descending
                        select new tempClass
                        {
                            Count = dataCount,
                            Date = g.Key
                        }).ToList();
            return Json(result);
        }

        public ActionResult ModelProgressPart_Chart([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from part in db.DR_MODELPROGRESS_PART
                        group part by part.DATECREATED into g
                        let dataCount = g.Count()
                        orderby g.Key descending
                        select new tempClass
                        {
                            Count = dataCount,
                            Date = g.Key
                        }).ToList();

            return Json(result);
        }

        public ActionResult ModelProgressInstrument_Chart([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from instrument in db.DR_MODELPROGRESS_INSTRUMENT
                        group instrument by instrument.DATECREATED into g
                        let dataCount = g.Count()
                        orderby g.Key descending
                        select new tempClass
                        {
                            Count = dataCount,
                            Date = g.Key
                        }).ToList();

            return Json(result);
        }
    }
}
