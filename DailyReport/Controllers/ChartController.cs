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

        #region View
        public ActionResult ModelProgressLine()
        {
            return View();
        }

        public ActionResult ModelProgressLineModeler(string start, string end)
        {
            DateTime startDate = Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);
            var lineData = (from lines in db.DR_MODELPROGRESS_LINE
                            where lines.DATECREATED >= startDate && lines.DATECREATED <= endDate
                          group lines by new { lines.DATECREATED, lines.CREATEDBY } into g
                          orderby g.Key descending
                          select new ProgressLineListItem
                          {
                              Date = g.Key.DATECREATED,
                              Modeler = g.Key.CREATEDBY,
                              Count = g.Count()

                          }).ToList();
            var lineDataGroup = lineData.GroupBy(t => t.Date);
            var modelerDist = db.DR_MODELPROGRESS_LINE.Select(line => line.CREATEDBY).Distinct();
            ModelCountData modelCountData = new ModelCountData();
            foreach (var item in modelerDist)
            {
                ModelerData modeler = new ModelerData() { Name = item};
                modelCountData.Modeler.Add(modeler);
            }

            foreach (var item in lineDataGroup)
            {
                modelCountData.Date.Add(item.Key.Value.ToShortDateString());
                foreach(var modeler in modelCountData.Modeler)
                {
                    ProgressLineListItem listItem = item.Where(t => t.Modeler == modeler.Name).FirstOrDefault();
                    if (listItem == null)
                    {
                        modeler.ModelCount.Add(0);
                    }
                    else
                    {
                        modeler.ModelCount.Add(listItem.Count);
                    }
                }
            }
            return View(modelCountData);
        }

        public ActionResult ModelProgressRun()
        {
            return View();
        }

        public ActionResult ModelProgressRunModeler(string start, string end)
        {
            DateTime startDate = Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);
            var lineData = (from lines in db.DR_MODELPROGRESS_RUN
                            where lines.DATECREATED >= startDate && lines.DATECREATED <= endDate
                            group lines by new { lines.DATECREATED, lines.CREATEDBY } into g
                            orderby g.Key descending
                            select new ProgressLineListItem
                            {
                                Date = g.Key.DATECREATED,
                                Modeler = g.Key.CREATEDBY,
                                Count = g.Count()

                            }).ToList();
            var lineDataGroup = lineData.GroupBy(t => t.Date);
            var modelerDist = db.DR_MODELPROGRESS_LINE.Select(line => line.CREATEDBY).Distinct();
            ModelCountData modelCountData = new ModelCountData();
            foreach (var item in modelerDist)
            {
                ModelerData modeler = new ModelerData() { Name = item };
                modelCountData.Modeler.Add(modeler);
            }

            foreach (var item in lineDataGroup)
            {
                modelCountData.Date.Add(item.Key.Value.ToShortDateString());
                foreach (var modeler in modelCountData.Modeler)
                {
                    ProgressLineListItem listItem = item.Where(t => t.Modeler == modeler.Name).FirstOrDefault();
                    if (listItem == null)
                    {
                        modeler.ModelCount.Add(0);
                    }
                    else
                    {
                        modeler.ModelCount.Add(listItem.Count);
                    }
                }
            }
            return View(modelCountData);
        }

        public ActionResult ModelProgressPart()
        {
            return View();
        }

        public ActionResult ModelProgressPartModeler(string start, string end)
        {
            DateTime startDate = Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);
            var lineData = (from lines in db.DR_MODELPROGRESS_PART
                            where lines.DATECREATED >= startDate && lines.DATECREATED <= endDate
                            group lines by new { lines.DATECREATED, lines.CREATEDBY } into g
                            orderby g.Key descending
                            select new ProgressLineListItem
                            {
                                Date = g.Key.DATECREATED,
                                Modeler = g.Key.CREATEDBY,
                                Count = g.Count()

                            }).ToList();
            var lineDataGroup = lineData.GroupBy(t => t.Date);
            var modelerDist = db.DR_MODELPROGRESS_LINE.Select(line => line.CREATEDBY).Distinct();
            ModelCountData modelCountData = new ModelCountData();
            foreach (var item in modelerDist)
            {
                ModelerData modeler = new ModelerData() { Name = item };
                modelCountData.Modeler.Add(modeler);
            }

            foreach (var item in lineDataGroup)
            {
                modelCountData.Date.Add(item.Key.Value.ToShortDateString());
                foreach (var modeler in modelCountData.Modeler)
                {
                    ProgressLineListItem listItem = item.Where(t => t.Modeler == modeler.Name).FirstOrDefault();
                    if (listItem == null)
                    {
                        modeler.ModelCount.Add(0);
                    }
                    else
                    {
                        modeler.ModelCount.Add(listItem.Count);
                    }
                }
            }
            return View(modelCountData);
        }

        public ActionResult ModelProgressInstrument()
        {
            return View();
        }

        public ActionResult ModelProgressInstrumentModeler(string start, string end)
        {
            DateTime startDate = Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);
            var lineData = (from lines in db.DR_MODELPROGRESS_INSTRUMENT
                            where lines.DATECREATED >= startDate && lines.DATECREATED <= endDate
                            group lines by new { lines.DATECREATED, lines.CREATEDBY } into g
                            orderby g.Key descending
                            select new ProgressLineListItem
                            {
                                Date = g.Key.DATECREATED,
                                Modeler = g.Key.CREATEDBY,
                                Count = g.Count()

                            }).ToList();
            var lineDataGroup = lineData.GroupBy(t => t.Date);
            var modelerDist = db.DR_MODELPROGRESS_LINE.Select(line => line.CREATEDBY).Distinct();
            ModelCountData modelCountData = new ModelCountData();
            foreach (var item in modelerDist)
            {
                ModelerData modeler = new ModelerData() { Name = item };
                modelCountData.Modeler.Add(modeler);
            }

            foreach (var item in lineDataGroup)
            {
                modelCountData.Date.Add(item.Key.Value.ToShortDateString());
                foreach (var modeler in modelCountData.Modeler)
                {
                    ProgressLineListItem listItem = item.Where(t => t.Modeler == modeler.Name).FirstOrDefault();
                    if (listItem == null)
                    {
                        modeler.ModelCount.Add(0);
                    }
                    else
                    {
                        modeler.ModelCount.Add(listItem.Count);
                    }
                }
            }
            return View(modelCountData);
        }
        #endregion

        #region Json
        public ActionResult ProgressPipingLineList()
        {
            return View();
        }

        public ActionResult ModelProgressLine_Read()
        {
            var result = (from lines in db.DR_MODELPROGRESS_LINE
                          group lines by lines.DATECREATED into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new ProgressLineListItem
                          {
                              Count = dataCount,
                              Date = g.Key
                          }).ToList();
            return Json(result);
        }

        public ActionResult ModelProgressRun_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from run in db.DR_MODELPROGRESS_RUN
                          group run by run.DATECREATED into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new ProgressLineListItem
                        {
                            Count = dataCount,
                            Date = g.Key
                        }).ToList();
            return Json(result);
        }

        public ActionResult ModelProgressPart_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from part in db.DR_MODELPROGRESS_PART
                          group part by part.DATECREATED into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new ProgressLineListItem
                          {
                              Count = dataCount,
                              Date = g.Key
                          }).ToList();

            return Json(result);
        }

        public ActionResult ModelProgressInstrument_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from instrument in db.DR_MODELPROGRESS_INSTRUMENT
                          group instrument by instrument.DATECREATED into g
                          let dataCount = g.Count()
                          orderby g.Key descending
                          select new ProgressLineListItem
                        {
                            Count = dataCount,
                            Date = g.Key
                        }).ToList();

            return Json(result);
        }

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
        #endregion

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
