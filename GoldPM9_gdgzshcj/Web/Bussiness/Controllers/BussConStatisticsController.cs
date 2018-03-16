using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using System.Collections;

namespace Bussiness.Controllers
{
    [Description("BussConStatistics")]
    public class BussConStatisticsController : CoreController
    {
        private DBSql.Bussiness.BussContract op = new DBSql.Bussiness.BussContract();

        /// <summary>
        /// 将DataTable转化成Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DataTableToJson(DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>(); 
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }
            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }

        /// <summary>
        /// 统计列表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string StatisticsListJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            string isF = Request["isF"];     
            string isQ = Request["isQ"];     
            string startT = Request["startT"];
            string endT = Request["endT"];

            DataTable data = op.ConStati();
            var list = from o in data.AsEnumerable().ToList()
                       select new
                       {
                           ConIsFeeFinished = o.Field<int?>("ConIsFeeFinished"),
                           ConDate = o.Field<DateTime>("ConDate"),
                           公司 = o.Field<string>("公司"),
                           合同类型 = o.Field<string>("合同类型"),
                           合同状态 = o.Field<string>("合同状态"),
                           年份 = o.Field<int?>("年份"),
                           月份 = o.Field<int?>("月份"),
                           合同金额 = o.Field<decimal?>("合同金额"),//.Value.ToString("0.00"),
                           结算金额 = o.Field<decimal?>("结算金额"),//.Value.ToString("0.00"),
                           收款金额 = o.Field<decimal?>("收款金额"),//.Value.ToString("0.00"),
                           开票额 = o.Field<decimal?>("开票额"),//.Value.ToString("0.00"),
                           合同个数 = o.Field<int?>("合同个数"),
                       };


            int type = 0;   //传过来的值
            string company = "";
            switch (type)
            {
                case 1: company = "分公司";break;
                case 2: company = "工程公司";break;
                case 3: company = "创景公司"; break;
                default:
                    break;
            }
            if (company != string.Empty)
                list = list.Where(p => p.公司 == company);
            if (isF == "1")
                list = list.Where(p => p.ConIsFeeFinished == 1);
            else if(isF == "0")
                list = list.Where(p => p.ConIsFeeFinished == 0);
            if (isQ == "1")
                list = list.Where(p => p.合同状态 == "已签");
            else if (isQ == "0")
                list = list.Where(p => p.合同状态 == "未签");
            if (startT != string.Empty)
            {
                DateTime s = DateTime.Parse(startT);
                list = list.Where(p => p.ConDate >= s);
            }
            if (endT != string.Empty)
            {
                DateTime e = DateTime.Parse(endT);
                list = list.Where(p => p.ConDate <= e);
            }
            return DataTableToJson(list.ToDataTable());
        }



        /// <summary>
        /// 统计列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult StatisticsList()
        {
            List<string> list = PermissionList("ContractStatistics");
            if(list.Contains("export"))
            {
                ViewBag.exportPermission = 1;
            }
            else
            {
                ViewBag.exportPermission = 0;
            }
            return View();
        }
    }
}
