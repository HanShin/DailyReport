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

        private class Progress
        {
            public string Line_no { get; set; }
            public string Operator { get; set; }
        }


        public ActionResult ModelProgressLine()
        {
            var modelerDist = db.DR_MODELPROGRESS_LINE.Select(line => line.CREATEDBY).Distinct();
            List<string> modelerList = new List<string>();
            List<ModelCountData> result = new List<ModelCountData>();
            foreach (var item in modelerDist)
            {
                ModelCountData modeler = new ModelCountData(){ Name = item };
            }
            var lineData = db.DR_MODELPROGRESS_LINE
                .GroupBy(line => new { line.DATECREATED, line.CREATEDBY })
                .GroupBy(line => line.Key.DATECREATED);



            foreach (var date in lineData)
            {
                ModelCountData modelCountData = new ModelCountData();
                modelCountData.Date = date.Key;
                foreach (var modeler in modelerList)
                {
                    // TODO

                }
                result.Add(modelCountData);
            }
            return View(result);
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

        public ActionResult ProgressPipingLineList()
        {
            return View();
        }

        //public ActionResult coProgressPipingLineList()
        //{
        //    return View();
        //}

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


        //public ActionResult ModelProgressLine_Chart([DataSourceRequest]DataSourceRequest request)
        //{
        //    var lineData = db.DR_MODELPROGRESS_LINE
        //        .GroupBy(line => new { line.DATECREATED, line.CREATEDBY })
        //        .GroupBy(line => line.Key.DATECREATED);
        //    List<ModelCountData> result = new List<ModelCountData>();
        //    foreach (var date in lineData)
        //    {
        //        ModelCountData modelCountData = new ModelCountData();
        //        modelCountData.Date = date.Key;
        //        foreach (var model in date)
        //        {
        //            ModelerCount modeler = new ModelerCount();
        //            modeler.Modeler = model.Key.CREATEDBY;
        //            modeler.Count = model.Count();
        //            modelCountData.ModelerList.Add(modeler);
        //        }
        //        result.Add(modelCountData);
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult ModelProgressRun_Chart([DataSourceRequest]DataSourceRequest request)
        //{
        //    var result = (from run in db.DR_MODELPROGRESS_RUN
        //                group run by run.DATECREATED into g
        //                let dataCount = g.Count()
        //                orderby g.Key descending
        //                  select new ModelCountData
        //                {
        //                    Count = dataCount,
        //                    Date = g.Key
        //                }).ToList();
        //    return Json(result);
        //}

        //public ActionResult ModelProgressPart_Chart([DataSourceRequest]DataSourceRequest request)
        //{
        //    var result = (from part in db.DR_MODELPROGRESS_PART
        //                group part by part.DATECREATED into g
        //                let dataCount = g.Count()
        //                orderby g.Key descending
        //                select new ModelCountData
        //                {
        //                    Count = dataCount,
        //                    Date = g.Key
        //                }).ToList();

        //    return Json(result);
        //}

        //public ActionResult ModelProgressInstrument_Chart([DataSourceRequest]DataSourceRequest request)
        //{
        //    var result = (from instrument in db.DR_MODELPROGRESS_INSTRUMENT
        //                group instrument by instrument.DATECREATED into g
        //                let dataCount = g.Count()
        //                orderby g.Key descending
        //                  select new ModelCountData
        //                {
        //                    Count = dataCount,
        //                    Date = g.Key
        //                }).ToList();

        //    return Json(result);
        //}

        public ActionResult ProgressPipingLineList_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_PROGRESS_PIPINGLINELIST.ToList();

            return Json(result.ToDataSourceResult(request));
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
            int sQuantity = sOperator("S");
            int wQuantity = sOperator("W");
            int kQuantity = sOperator("K");

            // /// 협력사별 수량 합과 전체 수량
            int mTotal = sQuantity + wQuantity + kQuantity;
            int Total = temp.Count();

            // //// 전체 수량에 대한 백분율
            int mPercentage = mTotal * 100 / Total;
            int all = 100;

            // ////협력사별 백분율
            int sPercentage = (int)(sQuantity * 100 / Total);
            int wPercentage = (int)(wQuantity * 100 / Total);
            int kPercentage = (int)(kQuantity * 100 / Total);

            ModelLineNumData mlnd = new ModelLineNumData(sQuantity,wQuantity, kQuantity, Total,
                wPercentage, sPercentage, kPercentage, all, mPercentage, mTotal);

            List<ModelLineNumData> ml = new List<ModelLineNumData>();
            ml.Add(mlnd);

            return Json(ml);
        }

        //public ActionResult ProgressPipingLineList_Chart([DataSourceRequest]DataSourceRequest request)
        //{
        //    var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
        //                select new Progress
        //                {
        //                    Line_no = aLineList.LINE_NO,
        //                    Operator = aLineList.COOPERATOR
        //                }).ToList();
        //    /// 협력사별 수량
        //    int sQuantity = sOperator("S");
        //    int wQuantity = sOperator("W");
        //    int kQuantity = sOperator("K");

        //    /// 협력사별 수량 합과 전체 수량
        //    int mTotal = sQuantity + wQuantity + kQuantity;
        //    int Total = temp.Count();

        //    //// 전체 수량에 대한 백분율
        //    int mPercentage = mTotal * 100 / Total;
        //    int tPercentage = 100;
        //    string Col1 = "모델"; string Col2 = "전체";

        //    ModelLineNumData mlnd = new ModelLineNumData(Col1, mPercentage, mTotal, Total);
        //    ModelLineNumData mlnd1 = new ModelLineNumData(Col2, tPercentage, Total, Total);

        //    List<ModelLineNumData> ml = new List<ModelLineNumData>();
        //    ml.Add(mlnd); ml.Add(mlnd1);

        //    return Json(ml);
        //}

        //public ActionResult coProgressPipingLineList_Chart([DataSourceRequest]DataSourceRequest request)
        //{
        //    var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
        //                select new Progress
        //                {
        //                    Line_no = aLineList.LINE_NO,
        //                    Operator = aLineList.COOPERATOR
        //                }).ToList();
        //    // /// 협력사별 수량
        //    int sQuantity = sOperator("S");
        //    int wQuantity = sOperator("W");
        //    int kQuantity = sOperator("K");

        //    // /// 협력사별 수량 합과 전체 수량
        //    int mTotal = sQuantity + wQuantity + kQuantity;
        //    int Total = temp.Count();

        //    // //// 전체 수량에 대한 백분율
        //    int mPercentage = mTotal * 100 / Total;

        //    // ///// 협력사별 백분율
        //    int sPercentage = (int)(sQuantity * 100 / Total);
        //    int wPercentage = (int)(wQuantity * 100 / Total);
        //    int kPercentage = (int)(kQuantity * 100 / Total);
        //    string Col1 = "경신"; string Col2 = "우림"; string Col3 = "스페이스";

        //    CoModelLineNumData cmlnd = new CoModelLineNumData(Col1, kPercentage, kQuantity, Total);
        //    CoModelLineNumData cmlnd1 = new CoModelLineNumData(Col2, wPercentage, wQuantity, Total);
        //    CoModelLineNumData cmlnd2 = new CoModelLineNumData(Col3, sPercentage, sQuantity, Total);

        //    List<CoModelLineNumData> ml = new List<CoModelLineNumData>();
        //    ml.Add(cmlnd); ml.Add(cmlnd1); ml.Add(cmlnd2);

        //    return Json(ml);
        //}

        private int sOperator(string coOperater)
        {
            ///Cooperator별/////////
            var temp = (from aLineList in db.DR_PROGRESS_PIPINGLINELIST
                        from pLinslist in db.DR_PROGRESS_CPIPINGLINELIST
                        //join cLineList in db.DR_PROGRESS_CPIPINGLINELIST on aLineList.COOPERATOR equals cLineList.COOPERATOR into temp
                        where aLineList.COOPERATOR == coOperater && aLineList.LINE_NO == pLinslist.LINE_NO
                        select new Progress
                        {
                            Line_no = aLineList.LINE_NO,
                            Operator = aLineList.COOPERATOR
                        }).ToList();
            return temp.Count();
        }
    }
}
