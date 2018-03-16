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
    [Description("ProjectStatistics")]
    public class ProjectStatisticsController : CoreController
    {
        private DBSql.Project.Project op = new DBSql.Project.Project();

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
            DataTable data = op.ProjectStati();
            return DataTableToJson(data);
        }

        public string Statistics(int type)
        {
            //0是分公司
            //1是工程公司
            //2是创景公司
            DataTable data = op.ProjectStati();
            var list = from i in data.AsEnumerable()
                       where i.Field<string>("公司") == "工程公司"
                       select
                        new
                        {
                            公司 = i.Field<string>("公司"),
                            项目类型 = i.Field<string>("项目类型"),
                            项目个数 = i.Field<int?>("项目个数"),
                            年份 = i.Field<int?>("年份"),
                            月份 = i.Field<int?>("月份"),
                            合同金额 = i.Field<decimal?>("合同金额")
                        };
            string str = "";
            switch (type)
            {
                case 1:str = "分公司";break;
                case 2:str = "工程公司";break;
                case 3:str = "创景公司";break;
            }
            if (str.Equals(string.Empty))
            {
                return DataTableToJson(list.Select(p => p.公司 == str).ToDataTable());
            }
            return DataTableToJson(list.ToDataTable());
        }

        /// <summary>
        /// 统计列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult StatisticsList()
        {
            return View();
        }
    }
}
