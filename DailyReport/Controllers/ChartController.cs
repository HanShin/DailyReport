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

        // TODO : Linq 에서 DateTime 데이터가 string으로 변환이 안되서 Chart에 표현하기 어려워서 데이터 변형용으로 임시로 만든 클래스
        // 나중에 해결방법 찾으면 수정한다.
        // (Datetime형 데이터는 자료가 없는 날짜에도 Chart에 표시되어서 string으로 변형하려 했는데 실패)
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
            var a = db.DR_PROGRESS_PIPINGLINELIST.ToList();
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
            int model = pModel / total * 100;
            int all = 100;

            // ////협력사별 백분율
            int sPer = sCo / total * 100;
            int wPer = wCo / total * 100;
            int kPer = kCo / total * 100;

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
                        where aLineList.COOPERATOR == "s" && aLineList.LINE_NO == pLinslist.LINE_NO
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
                        where aLineList.COOPERATOR == "w" && aLineList.LINE_NO == pLinslist.LINE_NO
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
                        where aLineList.COOPERATOR == "k" && aLineList.LINE_NO == pLinslist.LINE_NO
                        select new Progress
                        {
                            Line_no = aLineList.LINE_NO,
                            Operator = aLineList.COOPERATOR
                        }).ToList();
            return temp.Count();
        }

    }
}
