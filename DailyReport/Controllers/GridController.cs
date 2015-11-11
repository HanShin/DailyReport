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

        #region View
        public ActionResult MtoOff()
        {
            return View();
        }

        public ActionResult MModelTodoList(){
            return View();
        }

        public ActionResult Below2()
        {
            return View();
        }

        public ActionResult TwotoSix()
        {
            return View();
        }

        public ActionResult EighttoTwentyFour()
        {
            return View();
        }

        public ActionResult LargerThanTwentyFour()
        {
            return View();
        }

        public ActionResult LineNoIso()
        {
            return View();
        }

        public ActionResult IsoRev()
        {
            return View();
        }

        public ActionResult IsoDescription()
        {
            return View();
        }
        
        public ActionResult BulkBM()
        {
            return View();
        }

        public ActionResult SupportBm()
        {
            return View();
        }

        public ActionResult InstrumentBm()
        {
            return View();
        }

        public ActionResult SpecialtyBm()
        {
            return View();
        }

        public ActionResult Consistency()
        {
            return View();
        }
    #endregion 

        #region Json
        public ActionResult Mto_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MMODEL_MTOOFF.ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TodoList_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = db.DR_MMODEL_TODOLIST.ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Below2_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<LineList> linelist = new List<LineList>();
            string[] sp;
            
            foreach (LineList line in GetLineListAll())
            {
                if (line.PIPELINE != null)
                {
                    sp = line.PIPELINE.Split('-');
                    if (sp[0] == "1\"" || sp[0].Contains("1/2\"") || sp[0] == "3/4\"")
                    {
                        linelist.Add(line);
                    }
                }
                else
                {
                    continue;
                }
               
            }

            return Json(linelist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult TwotoSix_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<LineList> linelist = new List<LineList>();
            string[] sp;
            int a;
            foreach (LineList line in GetLineListAll())
            {
                if (line.PIPELINE != null)
                {
                    sp = line.PIPELINE.Split('-');
                    if (sp[0] == "2\"" || sp[0] == "3\"" || sp[0] == "4\"" || sp[0] == "6\"")
                    {
                        linelist.Add(line);
                    }
                }
                else
                {
                    continue;
                }

            }
            return Json(linelist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult EighttoTwentyFour_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<LineList> linelist = new List<LineList>();
            string[] sp;

            foreach (LineList line in GetLineListAll())
            {
                if (line.PIPELINE != null)
                {
                    sp = line.PIPELINE.Split('-');
                                        
                    if (sp[0] == "8\"" || sp[0] == "10\"" || sp[0] == "12\"" || sp[0] == "14\"" ||
                        sp[0] == "16\"" ||sp[0] == "18\"" ||sp[0] == "20\"" ||sp[0] == "22\"" ||
                        sp[0] == "24\"")
                    {
                        linelist.Add(line);
                    }
                }
                else
                {
                    continue;
                }

            }

            return Json(linelist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult LargerThanTwentyFour_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<LineList> linelist = new List<LineList>();
            string[] sp;

            foreach (LineList line in GetLineListAll())
            {
                if (line.PIPELINE != null)
                {
                    sp = line.PIPELINE.Split('-');
                    if (sp[0] == "26\"" || sp[0] == "28\"" || sp[0] == "30\"" || sp[0] == "32\"" ||
                        sp[0] == "34\"" || sp[0] == "36\"" || sp[0] == "38\"" || sp[0] == "40\"" ||
                        sp[0] == "42\"" || sp[0] == "46\"" || sp[0] == "48\"" || sp[0]==("50") ||
                        sp[0] == "54\"" || sp[0] == "56\"" ||sp[0] == "58\""||   sp[0] == "60\""||
                        sp[0] == "64\""||sp[0] == "66\""|| sp[0] == "68\"" || sp[0] == "78\"")
                    {
                        linelist.Add(line);
                    }
                }
                else
                {
                    continue;
                }

            }

            return Json(linelist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Consistency_Read([DataSourceRequest]DataSourceRequest request)
        {
            var tempItem = db.DR_CONSISTENCY.Take(500).ToList();
            List<ConsistencyItem> result = new List<ConsistencyItem>();
            foreach (var item in tempItem)
            {
                ConsistencyItem consistency = new ConsistencyItem();
                consistency.PIPE_LINE = item.LINENO;
                Dictionary<string, int> itemList = new Dictionary<string, int>();

                foreach (var subitem in item.DR_CONSISTENCY_VALVE)
                {
                    if (string.IsNullOrEmpty(subitem.LONGMATERIALDESCRIPTION))
                    {
                        continue;
                    }
                    string valveItem =  subitem.LONGMATERIALDESCRIPTION.Split(' ').First();
                    addOrUpdate(itemList, valveItem, 1);
                    subitem.DR_CONSISTENCY = null;
                }

                foreach(string valveItem in itemList.Keys)
                {
                    consistency.ITEMS += ",";
                    consistency.ITEMS += valveItem + itemList[valveItem].ToString("00");
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

        public ActionResult BulkBM_Read([DataSourceRequest]DataSourceRequest request)
        {
            // Item을 그룹화하여 Select
            var result = (from bulkbm in db.DR_BULKBM
                          group bulkbm by new { bulkbm.DWGNO, bulkbm.SPECNAME, bulkbm.INDUSTRYCOMMODITYCODE, bulkbm.PART, bulkbm.LONGMATERIALDESCRIPTION, bulkbm.COMMENTS, bulkbm.PARTNUMBER, bulkbm.FABRICATIONTYPE} into g
                          let dataCount = g.Count()
                          select new BMItem
                          {
                              // SEP용 JOB_NO = 110670 (다른 프로젝트들이 추가될때 변경)
                              JOB_NO = "110670",
                              DWG_NO = g.Key.DWGNO,
                              CLASS_CD = g.Key.SPECNAME,
                              SYMBOL_CD = g.Key.COMMENTS,
                              BM_CD = g.Key.INDUSTRYCOMMODITYCODE.Substring(0,8),
                              MAIN_THK = g.Key.INDUSTRYCOMMODITYCODE.Substring(8,2),
                              SUB_THK = g.Key.INDUSTRYCOMMODITYCODE.Substring(10,2),
                              SIZE_UNIT = "in",
                              DWG_BM_QTY = dataCount,
                              FS_GUBN = g.Key.FABRICATIONTYPE,
                              INDUSTRYCOMMODITYCODE = g.Key.INDUSTRYCOMMODITYCODE,
                              PARTNUMBER = g.Key.PARTNUMBER,
                              LONGMATERIALDESCRIPTON = g.Key.LONGMATERIALDESCRIPTION
                          }).ToList();

            foreach (var item in result)
            {
                // FS_GUBN은 고정값 왜 이렇게 들어가는지는 모름
                switch (item.FS_GUBN)
                {
                    case "7": item.FS_GUBN = "F";
                        break;
                    case "15": item.FS_GUBN = "S";
                        break;

                }
                // null 체크
                if (item.PARTNUMBER == null)
                {
                    continue;
                }
                if (item.PARTNUMBER.Length < 10 || item.PARTNUMBER.Contains("ATM"))
                {
                    continue;
                }
                string ItemType = item.PARTNUMBER.Substring(8, 2);
                string schCode = getSchCode(ItemType);
                item.PARTNUMBER = item.PARTNUMBER.Replace(item.INDUSTRYCOMMODITYCODE, "");
                if (schCode.Length != 0)
                {
                    item.PARTNUMBER = item.PARTNUMBER.Replace(schCode, "").Replace("BS3", "").Replace("BS4","");

                }
                int portCount = 2;
                if (item.LONGMATERIALDESCRIPTON != null)
                {
                    portCount = getPortCount(item.LONGMATERIALDESCRIPTON);
                }

                string partNumber = item.PARTNUMBER;
                partNumber = partNumber.Substring(0, partNumber.Length / portCount);
                item.MAIN_SIZE = partNumber;

            }
            return Json(result.ToDataSourceResult(request)) ;
        }

        public ActionResult SupportBm_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from sup in db.DR_SUPPORTBM
                          select new SupportBM
                          {
                              OID = sup.OID,
                              DWGNO = sup.DWGNO,
                              HANGERNAME = sup.HANGERNAME,
                              DESCRIPTION = sup.DESCRIPTION,
                          }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult InstrumentBm_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from sup in db.DR_INSTRUMENTBM
                        select new InstrumentBM
                        {
                            OID = sup.OID,
                            DWGNO = sup.DWGNO,
                            INSTRUMENT = sup.INSTRUMENT,
                            SPECNAME = sup.SPECNAME,
                            NPD = sup.NPD,
                            PARTNUMBER = sup.PARTNUMBER,
                        }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SpeceltyBm_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from sup in db.DR_SPECIALTYBM
                          select new SpecialtyBM
                          {
                              OID = sup.OID,
                              DWGNO = sup.DWGNO,
                              TAG = sup.TAG,
                              SPECNAME = sup.SPECNAME,
                              NPD1 = sup.NPD1,
                              NPDUNITTYPE1 = sup.NPDUNITTYPE1,
                              NPD2 = sup.NPD2,
                              NPDUNITTYPE2 = sup.NPDUNITTYPE2,
                              NPD3 = sup.NPD3,
                              NPDUNITTYPE3 = sup.NPDUNITTYPE3,
                          }).ToList();
            
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LineNoIso_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from sup in db.DR_LINENOISO
                          select new LineNoIso
                          {
                              OID = sup.OID,
                              PIPELINE = sup.PIPELINE,
                              DWGNO = sup.DWGNO,
                          }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsoRev_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from sup in db.DR_ISOREV
                          select new IsoRev
                          {
                              DWGNO = sup.DWGNO,
                              REV_NO = sup.REV_NO,
                              REV_DATE = sup.REV_DATE,
                              DWN = sup.DWN,
                              CHKD = sup.CHKD,
                              AAPD = sup.AAPD,
                              PLANT = sup.PLANT,
                              AREA = sup.AREA,
                              UNIT = sup.UNIT,
                          }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsoDescription_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = (from sup in db.DR_ISODESCRIPTION
                          select new IsoDescription
                          {
                              DWGNO = sup.DWGNO,
                              REV_NO = sup.REV_NO,
                              REV_DATE = sup.REV_DATE,
                              DESCRIPTION = sup.DESCRIPTION,
                              DWN = sup.DWN,
                              CHKD = sup.CHKD,
                              AAPD = sup.AAPD,
                              PLANT = sup.PLANT,
                              AREA = sup.AREA,
                              UNIT = sup.UNIT,
                          }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        void addOrUpdate(Dictionary<string, int> dic, string key, int newValue)
        {
            int val;
            if (dic.TryGetValue(key, out val))
            {
                // yay, value exists!
                dic[key] = val + newValue;
            }
            else
            {
                // darn, lets add the value
                dic.Add(key, newValue);
            }
        }

        /// <summary>
        /// Type에 따라 Port가 몇개인지 체크 대부분의 타입은 2개
        /// </summary>
        /// <param name="descrip"></param>
        /// <returns></returns>
        public static int getPortCount(string descrip)
        {
            int portCount = 2;
            descrip = descrip.Split(';')[0];
            if (descrip.Contains("CAP") || descrip.Contains("PLUG") || descrip.Contains("BLIND FLANGE"))
            {
                portCount = 1;
            }
            else if (descrip.Contains("TEE")  || descrip.Contains("LATERAL"))
            {
                portCount = 3;
            }
            return portCount;
        }


        /// <summary>
        /// Type에 따라 schCode를 가져온다.
        /// </summary>
        /// <param name="ItemType"></param>
        /// <returns></returns>
        public static string getSchCode(string ItemType)
        {
            string schCode = string.Empty;
            switch (ItemType)
            {
                case "1M": schCode = "S-40";
                    break;
                case "1S": schCode = "S-80";
                    break;
                case "1E": schCode = "S-160";
                    break;
                case "MM": schCode = "GRV-10.3";
                    break;
                case "1J": schCode = "S-30";
                    break;
                case "M2": schCode = "GRE";
                    break;
                case "1W": schCode = "S-40S";
                    break;
                case "1U": schCode = "S-XXS";
                    break;
                case "17": schCode = "S-STD";
                    break;
                case "1G": schCode = "S-20";
                    break;
                case "1V": schCode = "S-10S";
                    break;
                case "18": schCode = "S-10";
                    break;
                case "1Y": schCode = "S-80S";
                    break;
                case "1R": schCode = "S-80";
                    break;
                case "1T": schCode = "S-XS";
                    break;
                case "1H": schCode = "S-20";
                    break;
                case "1L": schCode = "S-40";
                    break;
                case "1Q": schCode = "S-60";
                    break;
                case "LV": schCode = "GRV-10";
                    break;
                case "IW": schCode = "S-40S";
                    break;
                case "6B": schCode = string.Empty;
                    break;
                case "1N": schCode = "S-40";
                    break;
                case "TA": schCode = "GRE";
                    break;
                case "07": schCode = "0.688";
                    break;
                case "1I": schCode = "S-30";
                    break;
                case "4X": schCode = "0.688";
                    break;
                case "06": schCode = "0.625";
                    break;
                case "05": schCode = "0.562";
                    break;
                case "M1": schCode = "GRV-10.3";
                    break;
                case "IX": schCode = "0.752";
                    break;
            }
            return schCode;
        }

        public List<LineList> GetLineListAll()
        {
            var result = (from sup in db.DR_LINELIST
                          select new LineList
                          {
                              OID = sup.OID,
                              PIPELINE = sup.PIPELINE,
                              DWGNO = sup.DWGNO,
                              DESIGN_PRESS = sup.DESIGN_PRESS,
                              OPER_PRESS = sup.OPER_PRESS,
                              TEST_PRESS = sup.TEST_PRESS,
                              DESIGN_TEMP = sup.DESIGN_TEMP,
                              OPER_TEMP = sup.OPER_TEMP,
                              INSUL_CODE = sup.INSUL_CODE,
                              INSUL_THK = sup.INSUL_THK,
                              PAINT_CODE = sup.PAINT_CODE,
                              RTPT = sup.RTPT,
                              PWHT = sup.PWHT,
                              TEST_MEDIUM = sup.TEST_MEDIUM,
                              STEAM_TRACE = sup.STEAM_TRACE,
                              STEAM_JACKETING = sup.STEAM_JACKETING,
                              ELECTRIC_TRACE = sup.ELECTRIC_TRACE,
                              STRESS_ANALYSIS = sup.STRESS_ANALYSIS,
                              CLEANING = sup.CLEANING,
                              PIPING_PID = sup.PIPING_PID,
                              PIPING_PLAN = sup.PIPING_PLAN,
                              PIPING_STRESS = sup.PIPING_STRESS,
                          }).ToList();
            return result;
        }
    }
}