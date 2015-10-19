﻿using System;
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

        // TODO : Linq 에서 DateTime 데이터가 string으로 변환이 안되서 Chart에 표현하기 어려워서 데이터 변형용으로 임시로 만든 클래스
        // 나중에 해결방법 찾으면 수정한다.
        // (Datetime형 데이터는 자료가 없는 날짜에도 Chart에 표시되어서 string으로 변형하려 했는데 실패)
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
