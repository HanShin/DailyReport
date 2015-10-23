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

        private class Progress
        {
            public string Line_no { get; set; }
            public string Operator { get; set; }
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

        public ActionResult ModelProgressRun_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_RUN.ToList();

            return Json(result.ToDataSourceResult(request));
        }


        public ActionResult ModelProgressPart_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_PART.ToList();

            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult ModelProgressInstrument_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MODELPROGRESS_INSTRUMENT.ToList();

            return Json(result.ToDataSourceResult(request));
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
        public ActionResult ProgressPipingLineList_Chart([DataSourceRequest]DataSourceRequest request)
        {
            var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
                        select new Progress
                        {
                            Line_no = aLineList.LINE_NO,
                            Operator = aLineList.COOPERATOR
                        }).ToList();
            // /// 협력사별 수량
            int sCo = sOperator();
            int wCo = wOperator();
            int kCo = kOperator();

            // /// 협력사별 수량 합과 전체 수량
            int pModel = sCo + wCo + kCo;
            int total = temp.Count();

            // //// 전체 수량에 대한 백분율
            int model = pModel * 100 / total;
            int all = 100;

            // ////협력사별 백분율
            int sPer = (int)(sCo * 100 / total);
            int wPer = (int)(wCo * 100 / total);
            int kPer = (int)(kCo * 100 / total);

            ModelLineNumData mlnd = new ModelLineNumData(wCo, sCo, kCo, total,
                wPer, sPer, kPer, all, model, pModel);

            List<ModelLineNumData> ml = new List<ModelLineNumData>();
            ml.Add(mlnd);

            return Json(ml);
        }

        private int sOperator()
        {
            ///Cooperator별/////////
            var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
                        from pLinslist in db.DR_PROGRESS_CPIPINGLINELIST
                        //join cLineList in db.DR_PROGRESS_CPIPINGLINELIST on aLineList.COOPERATOR equals cLineList.COOPERATOR into temp
                        where aLineList.COOPERATOR == "S" && aLineList.LINE_NO == pLinslist.LINE_NO
                        select new Progress
                        {
                            Line_no = aLineList.LINE_NO,
                            Operator = aLineList.COOPERATOR
                        }).ToList();
            return temp.Count();
        }

        private int wOperator()
        {
            ///Cooperator별/////////
            var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
                        from pLinslist in db.DR_PROGRESS_CPIPINGLINELIST
                        //join cLineList in db.DR_PROGRESS_CPIPINGLINELIST on aLineList.COOPERATOR equals cLineList.COOPERATOR into temp
                        where aLineList.COOPERATOR == "W" && aLineList.LINE_NO == pLinslist.LINE_NO
                        select new Progress
                        {
                            Line_no = aLineList.LINE_NO,
                            Operator = aLineList.COOPERATOR
                        }).ToList();
            return temp.Count();
        }

        private int kOperator()
        {
            ///Cooperator별/////////
            var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
                        from pLinslist in db.DR_PROGRESS_CPIPINGLINELIST
                        //join cLineList in db.DR_PROGRESS_CPIPINGLINELIST on aLineList.COOPERATOR equals cLineList.COOPERATOR into temp
                        where aLineList.COOPERATOR == "K" && aLineList.LINE_NO == pLinslist.LINE_NO
                        select new Progress
                        {
                            Line_no = aLineList.LINE_NO,
                            Operator = aLineList.COOPERATOR
                        }).ToList();
            return temp.Count();
        }

    }
}
