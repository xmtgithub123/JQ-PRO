using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;

namespace Project.Controllers
{
    [Description("Decision")]
    public class DecisionController : CoreController
    {
        private DBSql.Bussiness.BussContract bussContract = new DBSql.Bussiness.BussContract();
        private DBSql.Sys.BaseData baseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表视图

        //主页面
        [Description("列表")]
        public ActionResult ContractDecisionList()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("SynthesizeDecision");
        }

        //合同大小数量月度分析
        [Description("列表")]
        public ActionResult ContractDecision()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ContractDecision");
        }

        //合同收付款分析
        [Description("列表")]
        public ActionResult ContractFeeDecision()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ContractFeeDecision");
        }

        //合同类别分析
        [Description("列表")]
        public ActionResult ContractTypeDecision()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ContractTypeDecision");
        }

        //工程质量视图
        [Description("列表")]
        public ActionResult ProjectQualityReport()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ProjectQualityReport");
        }


              //项目进度视图
        [Description("列表")]
        public ActionResult ProjectProcessDecision()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ProjectProcessDecision");
        }

        [Description("合同统计列表")]
        public ActionResult ContractReport()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ContractReport");
        }

        [Description("项目查询")]
        public ActionResult ProjectReport()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("ProjectReport");
        }

        [Description("绩效统计分析报表")]
        public ActionResult PayAnalyseList()
        {
            //ViewBag.CurrentEmpID = userInfo.EmpID;
            //ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View("PayAnalyseList");
        }

        #endregion

        #region 列表数据json
       
        /// <summary>
        ///  月度分析数据
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractDecisionData(int ContractYear)
        {
            BarModel barModel = new BarModel();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", ContractYear);
            var dataTable = bussContract.ContractDecisionData(PageModel).AsEnumerable().Select(p => new
            {
                ConCount=p["ConCount"],
                ConMonth=p["m"],
                FeeType=p["FeeType"]
            }).OrderBy(p=>p.FeeType).ToList();
            barModel.legendData = new List<object>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };

            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            for (int i = 1; i < 5; i++)
            {
                objList = new List<object>();
                foreach (var data in dataTable)
                {

                    if (i == Convert.ToInt32(data.FeeType))
                    {
                        objList.Add(data.ConCount);
                    }

                }
                barModel.data.Add(objList);
            }


            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  年度分析数据
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractYearDecision(int ContractYear)
        {
            BarModel barModel = new BarModel();
            List<object> lst = new List<object>();
            List<string> lstYear = new List<string>();

            for (int i = ContractYear - 10; i <= ContractYear; i++)
            {
                lst.Add(i + "年");
                lstYear.Add(string.Format("{0}", i));
            }

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", string.Join(",", lstYear));
            var dataTable = bussContract.ContractYearDecision(PageModel).AsEnumerable().Select(p => new
            {
                ConCount = p["ConCount"],
                ConYear = p["ConYear"],
                FeeType = p["FeeType"]
            }).OrderBy(p => p.FeeType).ToList();

           

            barModel.legendData = lst;

            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            for (int i = 1; i < 5; i++)
            {
                 objList = new List<object>();
               
                for (int year = ContractYear - 10; year <= ContractYear; year++)
                {
                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.FeeType) == i &&  Convert.ToInt32(p.ConYear)==year);
                    objList.Add(number != null ? number.ConCount : 0);
                }
                barModel.data.Add(objList);
            }


            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  合同期间数量金额分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractCyclen(int ContractYear)
        {
            BarModel barModel = new BarModel();
            var FromData = new List<object>();

            FromData.Add("一个月以内");
            FromData.Add("三个月以内");
            FromData.Add("半年以内");
          
            FromData.Add("一年以内");
            FromData.Add("一年以上");

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", DateTime.Now.ToShortDateString());
            var dataTable = bussContract.ContractCyclen(PageModel).AsEnumerable().Select(p => new
            {
                ConCount = p["ConCount"],
                MonthType = p["MonthType"],//1,一个月以内2三个月以内,3半年以内,4,5表示
                FeeType = p["FeeType"]//1，合同数量2，表示金额
            }).OrderBy(p => p.FeeType).ToList();

            barModel.legendData = FromData;

            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 1; j < 3; j++)
            {
                objList = new List<object>();
                for (int i = 1; i < 6; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.MonthType) == i && Convert.ToInt32(p.FeeType) == j);
                    objList.Add(number != null ? number.ConCount : 0);

                }
                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }

        /// <summary>
        ///  合同费用月度分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractFeeMonth(int ContractYear)
        {
            BarModel barModel = new BarModel();
           

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", ContractYear);

            var dataTable = bussContract.ContractFeeMonth(PageModel).AsEnumerable().Select(p => new
            {
                ConFee = p["ConFee"],
                monthType = p["monthType"],//1,一个月以内2三个月以内,3半年以内,4,5表示
                FeeType = p["FeeType"]//1，合同数量2，表示金额
            }).OrderBy(p => p.FeeType).ToList();

            barModel.legendData = new List<object>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };


            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 1; j < 5; j++)
            {
                objList = new List<object>();
                for (int i = 1; i < 13; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.monthType) == i && Convert.ToInt32(p.FeeType) == j);
                    objList.Add(number != null ? number.ConFee : 0);

                }
                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }

        /// <summary>
        ///  合同费用年度分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractFeeYear(int ContractYear)
        {
            BarModel barModel = new BarModel();
            List<object> lst = new List<object>();
            List<string> lstYear = new List<string>();

            for (int i = ContractYear - 10; i <= ContractYear; i++)
            {
                lst.Add(i + "年");
                lstYear.Add(string.Format("{0}", i));
            }

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", string.Join(",", lstYear));
            var dataTable = bussContract.ContractFeeYear(PageModel).AsEnumerable().Select(p => new
            {
                ConFee = p["ConFee"],
                monthType = p["monthType"],
                FeeType = p["FeeType"]
            }).OrderBy(p => p.FeeType).ToList();



            barModel.legendData = lst;

            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 1; j < 5; j++)
            {
                objList = new List<object>();
                for (int year = ContractYear - 10; year <= ContractYear; year++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.monthType) == year && Convert.ToInt32(p.FeeType) == j);
                    objList.Add(number != null ? number.ConFee : 0);

                }
                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }



        /// <summary>
        ///  合同类别月度分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractTypeMonthFee(int ContractYear, int type)
        {
            BarModel barModel = new BarModel();
            List<object> lst = new List<object>();
            List<string> lstYear = new List<string>();

           

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", string.Join(",", ContractYear));
            var dataTable = bussContract.ContractTypeMonthFee(PageModel).AsEnumerable().Select(p => new
            {
                confee = p["confee"],
                ConType = p["ConType"],
                TypeMonth = p["TypeMonth"],
                conNumber = p["conNumber"]
            }). ToList();



            barModel.legendData = new List<object>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };


            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("ContractType").AsEnumerable().Select(p => new {
                BaseID = p.BaseID,
                baseOrder = p.BaseOrder
            }).OrderBy(p=>p.baseOrder).ToList(); ;



            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < baseData.Count; j++)
            {
                objList = new List<object>();
                for (int i = 1; i < 13; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.TypeMonth) == i && Convert.ToInt32(p.ConType) == baseData[j].BaseID);
                    if (type == 1)
                    {
                        objList.Add(number != null ? number.confee : 0);
                    }
                    else
                    {
                        objList.Add(number != null ? number.conNumber : 0);
                    }

                }
                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }

        /// <summary>
        ///  合同金额类别年度分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractTypeFeeYear(int ContractYear, int type)
        {
            BarModel barModel = new BarModel();
            List<object> lst = new List<object>();
            List<string> lstYear = new List<string>();

            for (int i = ContractYear - 10; i <= ContractYear; i++)
            {
                lst.Add(i + "年");
                lstYear.Add(string.Format("{0}", i));
            }



            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("ConYear", string.Join(",", lstYear));
            var dataTable = bussContract.ContractTypeFeeYear(PageModel).AsEnumerable().Select(p => new
            {
                confee = p["confee"],
                ConType = p["ConType"],
                TypeYear = p["TypeYear"],
                conNumber = p["conNumber"]
            }).ToList();



            barModel.legendData = lst;


            barModel.data = new List<List<object>>();
            List<object> objList = new List<object>();

            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("ContractType").AsEnumerable().Select(p => new
            {
                BaseID = p.BaseID,
                baseOrder = p.BaseOrder
            }).OrderBy(p => p.baseOrder).ToList(); ;



            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < baseData.Count; j++)
            {
                objList = new List<object>();
                for (int year = ContractYear - 10; year <= ContractYear; year++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.TypeYear) == year && Convert.ToInt32(p.ConType) == baseData[j].BaseID);
                    if (type == 1)
                    {
                        objList.Add(number != null ? number.confee : 0);
                    }
                    else {
                        objList.Add(number != null ? number.conNumber : 0);
                    }

                }
                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  设计差错分析析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ProjectQuality(string StartDate, string EndDate)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();
            //统计错误类别
            var FromDataValue = new List<object>();

            FromDataValue.Add("0");//任务数

            FromDataValue.Add("59");//原则性差错
            FromDataValue.Add("60");//技术性差错

            FromDataValue.Add("61");//一般性差错
            FromDataValue.Add("1"); //出错率



            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            var dataTable = bussContract.ProjectQuality(PageModel).AsEnumerable().Select(p => new
            {
                numbers = p["numbers"],
                EmpDepID = p["EmpDepID"],
                CheckErrTypeID = p["CheckErrTypeID"]
            }).ToList();



            List<object> objList = new List<object>();
            //部门数据
            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("department").AsEnumerable().Select(p => new
            {
                BaseID = p.BaseID,
                BaseName = p.BaseName,
                baseOrder = p.BaseOrder,
                BaseAtt5 = p.BaseAtt5
            }).Where(p => p.BaseAtt5 == "D").OrderBy(p => p.baseOrder).ToList();


            for (int i = 0; i < baseData.Count; i++)
            {
                lstlegend.Add(baseData[i].BaseName);
            }


            //加载类别
            barModel.legendData = lstlegend;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < FromDataValue.Count; j++)
            {
                objList = new List<object>();

                for (int i = 0; i < baseData.Count; i++)
                {
                    //如果是统计出错率则要另外计算 1 表示出错率
                    if (1 != Convert.ToInt32(FromDataValue[j]))
                    {
                        var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.CheckErrTypeID) == Convert.ToInt32(FromDataValue[j]) && Convert.ToInt32(p.EmpDepID) == Convert.ToInt32(baseData[i].BaseID));

                        objList.Add(number != null ? number.numbers : 0);
                    }
                    else
                    {
                        //统计部门总任务数量
                        var depTotal = dataTable.FirstOrDefault(p => Convert.ToInt32(p.CheckErrTypeID) == 0 && Convert.ToInt32(p.EmpDepID) == Convert.ToInt32(baseData[i].BaseID));
                        //统计部门出差数量
                        var ErrorTotal = dataTable.FirstOrDefault(p => Convert.ToInt32(p.CheckErrTypeID) == 1 && Convert.ToInt32(p.EmpDepID) == Convert.ToInt32(baseData[i].BaseID));

                        objList.Add(ErrorTotal == null || depTotal == null ? 0 : ((Convert.ToDecimal(ErrorTotal.numbers) / Convert.ToDecimal(depTotal.numbers))*100)   );

                    }

                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  月度完成情况
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ProjectMonthFinish(string StartDate, string EndDate, string ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();
            //统计错误类别
            var FromDataValue = new List<object>();

            FromDataValue.Add("1");//任务数

            FromDataValue.Add("2");//原则性差错
            FromDataValue.Add("3");//技术性差错


            var FromData = new List<object>();

            FromData.Add("实际任务个数");//任务数

            FromData.Add("按时完成个数");//原则性差错
            FromData.Add("超期完成个数");//技术性差错

            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);
            
            var dataTable = bussContract.ProjectMonthFinish(PageModel).AsEnumerable().Select(p => new
            {
                number = p["number"],
                totalMonth = p["totalMonth"],
                finishType = p["finishType"]
            }).ToList();



            List<object> objList = new List<object>();
      

            //加载类别
           barModel.legendData = new List<object>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" }; ;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < FromDataValue.Count; j++)
            {
                objList = new List<object>();

                for (int i =1; i <= 12; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.finishType) == Convert.ToInt32(FromDataValue[j]) && Convert.ToInt32(p.totalMonth) == i);

                        objList.Add(number != null ? number.number : 0);

                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  年度完成情况
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ProjectYearFinish(string StartDate, string EndDate, int ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lst = new List<object>();
            List<string> lstYear = new List<string>();

            for (int i = ConYear - 10; i <= ConYear; i++)
            {
                lst.Add(i + "年");
                lstYear.Add(string.Format("{0}", i));
            }
            //统计错误类别
            var FromDataValue = new List<object>();

            FromDataValue.Add("1");//任务数

            FromDataValue.Add("2");//原则性差错
            FromDataValue.Add("3");//技术性差错


            var FromData = new List<object>();

            FromData.Add("实际任务个数");//任务数

            FromData.Add("按时完成个数");//原则性差错
            FromData.Add("超期完成个数");//技术性差错

            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);

            var dataTable = bussContract.ProjectYearFinish(PageModel).AsEnumerable().Select(p => new
            {
                number = p["number"],
                totalYEAR = p["totalYEAR"],
                finishType = p["finishType"]
            }).ToList();



            List<object> objList = new List<object>();


            //加载类别
            barModel.legendData = lst;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < FromDataValue.Count; j++)
            {
                objList = new List<object>();
                for (int year = ConYear - 10; year <= ConYear; year++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.finishType) == Convert.ToInt32(FromDataValue[j]) && Convert.ToInt32(p.totalYEAR) == year);

                    objList.Add(number != null ? number.number : 0);

                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  年度完成情况
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ProjectPhaseFinish(string StartDate, string EndDate, int ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();
            List<object> objList = new List<object>();

 
            //统计错误类别
            var FromDataValue = new List<object>();

            FromDataValue.Add("1");//任务数

            FromDataValue.Add("2");//原则性差错
            FromDataValue.Add("3");//技术性差错


            var FromData = new List<object>();

            FromData.Add("实际任务个数");//任务数

            FromData.Add("按时完成个数");//原则性差错
            FromData.Add("超期完成个数");//技术性差错

            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);

            var dataTable = bussContract.ProjectPhaseFinish(PageModel).AsEnumerable().Select(p => new
            {
                number = p["number"],
                PhaseId = p["PhaseId"],
                finishType = p["finishType"]
            }).ToList();

            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase").AsEnumerable().Select(p => new
            {
                BaseID = p.BaseID,
                BaseName = p.BaseName,
                baseOrder = p.BaseOrder
            }).OrderBy(p => p.baseOrder).ToList(); ;


            for (int i = 0; i < baseData.Count; i++)
            {
                lstlegend.Add(baseData[i].BaseName.Replace(",",""));
            }

            //加载类别
            barModel.legendData = lstlegend;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < FromDataValue.Count; j++)
            {
                objList = new List<object>();
                for (int i = 0; i < baseData.Count; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.finishType) == Convert.ToInt32(FromDataValue[j]) && Convert.ToInt32(p.PhaseId) == baseData[i].BaseID);

                    objList.Add(number != null ? number.number : 0);

                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }



        /// <summary>
        ///  年度完成情况
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string DepFinish(string StartDate, string EndDate, int ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();
            List<object> objList = new List<object>();


            //统计错误类别
            var FromDataValue = new List<object>();

            FromDataValue.Add("1");//任务数

            FromDataValue.Add("2");//原则性差错
            FromDataValue.Add("3");//技术性差错
            FromDataValue.Add("4");//任务数

            FromDataValue.Add("5");//原则性差错
            FromDataValue.Add("6");//技术性差错


            var FromData = new List<object>();

            FromData.Add("项目个数");//任务数'项目个数', '任务个数', '子项个数','设计个数', '校核个数', '审核个数'

            FromData.Add("任务个数");//原则性差错
            FromData.Add("子项个数");//技术性差错
            FromData.Add("设计个数");//原则性差错
            FromData.Add("校核个数");//技术性差错
            FromData.Add("审核个数");//原则性差错
            

            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);

            var dataTable = bussContract.DepFinish(PageModel).AsEnumerable().Select(p => new
            {
                number = p["number"],
                ProjDepId = p["ProjDepId"],
                taskType = p["taskType"]
            }).ToList();

            //部门数据
            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("department").AsEnumerable().Select(p => new
            {
                BaseID = p.BaseID,
                BaseName = p.BaseName,
                baseOrder = p.BaseOrder,
                BaseAtt5 = p.BaseAtt5
            }).Where(p => p.BaseAtt5 == "D").OrderBy(p => p.baseOrder).ToList();




            for (int i = 0; i < baseData.Count; i++)
            {
                lstlegend.Add(baseData[i].BaseName);
            }


            //加载类别
            barModel.legendData = lstlegend;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < FromDataValue.Count; j++)
            {
                objList = new List<object>();
                for (int i = 0; i < baseData.Count; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.taskType) == Convert.ToInt32(FromDataValue[j]) && Convert.ToInt32(p.ProjDepId) == baseData[i].BaseID);

                    objList.Add(number != null ? number.number : 0);

                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }

        #endregion


        /// <summary>
        ///  部门月度产值统计分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string PayMonthReport(string StartDate, string EndDate, string ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();
            //统计错误类别
            //var FromDataValue = new List<object>();

            //FromDataValue.Add("1");//任务数

            //FromDataValue.Add("2");//原则性差错
            //FromDataValue.Add("3");//技术性差错


            //var FromData = new List<object>();

            //FromData.Add("实际任务个数");//任务数

            //FromData.Add("按时完成个数");//原则性差错
            //FromData.Add("超期完成个数");//技术性差错


            //部门数据
            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("department").AsEnumerable().Select(p => new
            {
                BaseID = p.BaseID,
                BaseName = p.BaseName,
                baseOrder = p.BaseOrder,
                BaseAtt5 = p.BaseAtt5
            }).Where(p => p.BaseAtt5 == "D").OrderBy(p => p.baseOrder).ToList();



            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);

            var dataTable = bussContract.PayMonthReport(PageModel).AsEnumerable().Select(p => new
            {
                EmpDepID = p["EmpDepID"],
                TotalMoney = p["TotalMoney"],
                totalMonth = p["totalMonth"]
            }).ToList();



            List<object> objList = new List<object>();


            //加载类别
            barModel.legendData = new List<object>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" }; ;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < baseData.Count; j++)
            {
                objList = new List<object>();

                for (int i = 1; i <= 12; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.EmpDepID) == Convert.ToInt32(baseData[j].BaseID) && Convert.ToInt32(p.totalMonth) == i);

                    objList.Add(number != null ? number.TotalMoney : 0);

                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  部门年度产值统计分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string PayYearReport(string StartDate, string EndDate, int ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<object> lst = new List<object>();
            List<string> lstYear = new List<string>();


            for (int i = ConYear - 10; i <= ConYear; i++)
            {
                lst.Add(i + "年");
                lstYear.Add(string.Format("{0}", i));
            }


            //部门数据
            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName("department").AsEnumerable().Select(p => new
            {
                BaseID = p.BaseID,
                BaseName = p.BaseName,
                baseOrder = p.BaseOrder,
                BaseAtt5 = p.BaseAtt5
            }).Where(p => p.BaseAtt5 == "D").OrderBy(p => p.baseOrder).ToList();

            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);

            var dataTable = bussContract.PayYearReport(PageModel).AsEnumerable().Select(p => new
            {
                EmpDepID = p["EmpDepID"],
                TotalMoney = p["TotalMoney"],
                totalYEAR = p["totalYEAR"]
            }).ToList();


            List<object> objList = new List<object>();
            //加载类别
            barModel.legendData =lst ;
            barModel.data = new List<List<object>>();


            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < baseData.Count; j++)
            {
                objList = new List<object>();

                for (int year = ConYear - 10; year <= ConYear; year++)
                {
                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.EmpDepID) == Convert.ToInt32(baseData[j].BaseID) && Convert.ToInt32(p.totalYEAR) ==year);

                    objList.Add(number != null ? number.TotalMoney : 0);
                }

                barModel.data.Add(objList);
            }

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  部门员工月度产值统计分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string PayEmpManMonthReport(string StartDate, string EndDate, string ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();
             
            //部门员工数据
            var baseData = new DBSql.Sys.BaseEmployee().GetList(e=>e.EmpDepID==userInfo.EmpDepID && e.EmpIsDeleted==false).Select(p => new
            {
                EmpID = p.EmpID,
                EmpName = p.EmpName,
              
            }).ToList();

            //for (int i = 0; i < baseData.Count; i++)
            //{
            //    lstlegend.Add(baseData[i].EmpName);
            //}

            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("StartDate", StartDate);
            PageModel.SelectCondtion.Add("endDate", EndDate);
            PageModel.SelectCondtion.Add("ConYear", ConYear);
            PageModel.SelectCondtion.Add("EmpDepID", userInfo.EmpDepID);

            
            var dataTable = bussContract.PayEmpManMonthReport(PageModel).AsEnumerable().Select(p => new
            {
                EmpID = p["EmpID"],
                TotalMoney = p["TotalMoney"],
                totalMonth = p["totalMonth"]
            }).ToList();

            List<object> objList = new List<object>();
            
            //加载类别
            barModel.legendData = new List<object>() { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" }; ;
            barModel.data = new List<List<object>>();

            var ErrorPix = 0;
            //有几种统计就有几个数组，这个有合同个数，合同金额，就有2个数组集合
            for (int j = 0; j < baseData.Count; j++)
            {
                objList = new List<object>();

                for (int i = 1; i <= 12; i++)
                {

                    var number = dataTable.FirstOrDefault(p => Convert.ToInt32(p.EmpID) == Convert.ToInt32(baseData[j].EmpID) && Convert.ToInt32(p.totalMonth) == i);

                    objList.Add(number != null ? number.TotalMoney : 0);
                   
                }
                lstlegend.Add(baseData[j].EmpName);
                barModel.data.Add(objList);
            }
            barModel.dataName = lstlegend;

            return JavaScriptSerializerUtil.getJson(barModel);

        }


        /// <summary>
        ///  部门员工月度产值统计分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ContractReportList(string StartDate, string ConMonth, string ConYear)
        {
            BarModel barModel = new BarModel();
            List<object> lstlegend = new List<object>();
            List<string> lstYear = new List<string>();


            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //PageModel.SelectCondtion.Add("StartDate", StartDate);
            //PageModel.SelectCondtion.Add("ConMonth", ConMonth);
            //PageModel.SelectCondtion.Add("ConYear", ConYear);

            string strMonth = PageModel.Filter[1].list[0].Value;
           
            var dataTable = bussContract.ContractReport(PageModel).AsEnumerable().Select(p => new
            {
                BaseName = p["BaseName"],
                number = p["number"],
                conMonth = p["conMonth"],
                TotalFee = p["TotalFee"],
                ConType = p["ConType"],
            }).ToList();

            //本月合同个数
            var mConTable = dataTable.Select(p => new
            {
                BaseName = p.BaseName,
                number =  p.number ,
                conMonth = p.conMonth ,
                TotalFee =  p.TotalFee  ,
                ConType = p.ConType
            }).Where(p => p.conMonth.ToString() == strMonth);

            var YMcon = (from con in mConTable
                         group con by con.ConType into cn
                         
                            select new
                            {
                                ConType = cn.Key,
                                YMc = cn.Sum(p => Convert.ToInt32(p.number)),
                                YMoney = cn.Sum(p => decimal.Parse( p.TotalFee.ToString())) //p => p.ConFinshedFee
                            }).ToList();

            // 年初至本月累计数
            var Ycon = (from con in dataTable
                        group con by con.ConType into cn
                        select new
                        {
                            ConType = cn.Key,
                            YMc = cn.Sum(p=>Convert.ToInt32( p.number)),
                            YMoney = cn.Sum(p => decimal.Parse(p.TotalFee.ToString())) //p => p.ConFinshedFee
                        }).ToList();


           // 上年同期数
            Common.SqlPageInfo frontPageModel = new Common.SqlPageInfo();
            frontPageModel.SelectCondtion.Add("IsFrontYear", "1");
            //frontPageModel.SelectCondtion.Add("ConMonth", ConMonth);
            //frontPageModel.SelectCondtion.Add("ConYear", Convert.ToInt32(ConYear) - 1);

           // frontPageModel.Filter[0].list[0].Value = (Convert.ToInt32(frontPageModel.Filter[0].list[0]) + 1).ToString();

            var frontTable = bussContract.ContractReport(frontPageModel).AsEnumerable().Select(p => new
            {
                BaseName = p["BaseName"],
                number = p["number"],
                conMonth = p["conMonth"],
                TotalFee = p["TotalFee"],
                ConType = p["ConType"],
            }).ToList();

            var Lcon = (from con in frontTable
                        group con by con.ConType into cn
                        select new
                        {
                            ConType = cn.Key,
                          //  YMc = cn.Count(),
                            YMoney = cn.Sum(p => decimal.Parse(p.TotalFee.ToString())) //p => p.ConFinshedFee
                        }).ToList();

            var handle = new DBSql.Sys.BaseData();
            var TWhere = QueryBuild<DataModel.Models.BaseData>.True();
            TWhere = TWhere.And(p => p.BaseOrder.StartsWith("059_") && p.BaseIsDeleted == false);

            var list = handle.GetQuery(TWhere).ToList();

            var data = (from s in list
                        join Yc in YMcon on s.BaseID equals Yc.ConType into con1
                        join Ya in Ycon on s.BaseID equals Ya.ConType into con2
                        join La in Lcon on s.BaseID equals La.ConType into con3
                        from qyc in con1.DefaultIfEmpty()
                        from qya in con2.DefaultIfEmpty()
                        from qla in con3.DefaultIfEmpty()
                        orderby s.BaseOrder ascending
                        select new
                        {
                            BaseID = s.BaseID,
                            s.BaseName,
                            s.BaseOrder,
                            //Bxu = s.BaseOrder.Substring(s.BaseOrder.Length - 1),
                            Bxu = Convert.ToInt32(s.BaseOrder.Substring(s.BaseOrder.Length - 2)).ToString(),
                            ConMonthAll = qyc == null ? 0 : qyc.YMc,
                            ConAll = qya == null ? 0 : qya.YMc,
                            ConMonthAllM = qyc == null ? 0 : qyc.YMoney,
                            ConAllM = qya == null ? 0 : qya.YMoney,
                            LConAllM = qla == null ? 0 : qla.YMoney
                        }).ToList();

            List<ContractBound> Blist = new List<ContractBound>();

            foreach (var obj in data)
            {
                ContractBound model = new ContractBound();
                model.BaseID = obj.BaseID;
                model.BaseName = obj.BaseName;
                model.BaseOrder = obj.BaseOrder;
                model.Bxu = obj.Bxu;
                model.ConAll = obj.ConAll;
                model.ConAllM = obj.ConAllM;
                model.ConMonthAll = obj.ConMonthAll;
                model.ConMonthAllM = obj.ConMonthAllM;
                model.LConAllM = obj.LConAllM;

                if (model.BaseOrder.Split('_').Length == 2)
                {
                    model.ConAll = data.Where(p => p.BaseOrder.Contains(model.BaseOrder)).Sum(p => p.ConAll);
                    model.ConAllM = data.Where(p => p.BaseOrder.Contains(model.BaseOrder)).Sum(p => p.ConAllM);
                    model.ConMonthAll = data.Where(p => p.BaseOrder.Contains(model.BaseOrder)).Sum(p => p.ConMonthAll);
                    model.ConMonthAllM = data.Where(p => p.BaseOrder.Contains(model.BaseOrder)).Sum(p => p.ConMonthAllM);
                    model.LConAllM = data.Where(p => p.BaseOrder.Contains(model.BaseOrder)).Sum(p => p.LConAllM);
                }
                model.Tqbl = "-%";
                decimal dp = model.ConAllM - model.LConAllM;
                if (dp != 0)
                {
                    if (model.LConAllM != 0)
                    {
                        dp = Math.Round(Math.Round(dp / model.LConAllM, 4) * 100, 2);
                        model.Tqbl = dp.ToString() + "%";
                    }
                }

                Blist.Add(model);
            }

            #region 增加汇总行

            ContractBound cmodel = new ContractBound();
            cmodel.BaseID = -1;
            cmodel.BaseName = "合计";
            cmodel.BaseOrder = "059";
            cmodel.Bxu = "";
            cmodel.ConAll = data.Sum(p => p.ConAll);
            cmodel.ConAllM = data.Sum(p => p.ConAllM);
            cmodel.ConMonthAll = data.Sum(p => p.ConMonthAll);
            cmodel.ConMonthAllM = data.Sum(p => p.ConMonthAllM);
            cmodel.LConAllM = data.Sum(p => p.LConAllM);
            cmodel.Tqbl = "-%";
            decimal cmdp = cmodel.ConAllM - cmodel.LConAllM;
            if (cmdp != 0)
            {
                if (cmodel.LConAllM != 0)
                {
                    cmdp = Math.Round(Math.Round(cmdp / cmodel.LConAllM, 4) * 100, 2);
                    cmodel.Tqbl = cmdp.ToString() + "%";
                }
            }

            #endregion


            Blist.Add(cmodel);

            return JavaScriptSerializerUtil.getJson(Blist);

        }



        /// <summary>
        ///  部门员工月度产值统计分析
        /// </summary>
        /// <returns></returns>
        [Description("列表json")]
        public string ProjectReportList(string StartDate, string EndDate, string ConYear)
        { 
            //统计数据
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //PageModel.SelectCondtion.Add("StartDate", StartDate);
            //PageModel.SelectCondtion.Add("endDate", EndDate);
            //PageModel.SelectCondtion.Add("ConYear", ConYear);
            //PageModel.SelectCondtion.Add("EmpDepID", userInfo.EmpDepID);

            var list = bussContract.ProjectReportList(PageModel);

           // var list = desTaskDB.GetSpecPlanList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });

        }

    }


    #region 自定义处理类
    public class ContractBound
    {
        private int _baseID = 0;
        public int BaseID
        {
            get { return _baseID; }
            set { _baseID = value; }
        }

        private string _baseName = "";
        public string BaseName
        {
            get { return _baseName; }
            set { _baseName = value; }
        }

        private string _baseOrder = "";
        public string BaseOrder
        {
            get { return _baseOrder; }
            set { _baseOrder = value; }
        }

        private string _bxu = "";
        public string Bxu
        {
            get { return _bxu; }
            set { _bxu = value; }
        }

        private int _conMonthAll = 0;
        private int _conAll = 0;
        private decimal _conMonthAllM = 0;
        private decimal _conAllM = 0;
        private decimal _lConAllM = 0;

        public int ConMonthAll
        {
            get { return _conMonthAll; }
            set { _conMonthAll = value; }
        }
        public int ConAll
        {
            get { return _conAll; }
            set { _conAll = value; }
        }
        public decimal ConMonthAllM
        {
            get { return _conMonthAllM; }
            set { _conMonthAllM = value; }
        }
        public decimal ConAllM
        {
            get { return _conAllM; }
            set { _conAllM = value; }
        }
        public decimal LConAllM
        {
            get { return _lConAllM; }
            set { _lConAllM = value; }
        }
        private string _tqbl; //同期比例
        public string Tqbl
        {
            get { return _tqbl; }
            set { _tqbl = value; }
        }

    }
    #endregion

}
