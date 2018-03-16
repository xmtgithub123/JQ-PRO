using Common.Data.Extenstions;
using DBSql.Bussiness;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bussiness.Controllers
{
    [Description("HZShow")]
    public class HZShowController : CoreController
    {
        [Description("项目汇总查看表")]
        public ActionResult list()
        {
            return View();
        }

        [Description("列表json")]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "a.Id desc";
            }

            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Contract == "like")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else
                    {
                        PageModel.SelectCondtion.Add(_child.Key, _child.Value);
                    }
                }
            }

            var dt = new DBSql.Bussiness.HZShow().GetHZShow(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                rows = dt
            });
        }

        #region 开票汇总

        [Description("合同汇总列表")]
        public ActionResult hthzlist()
        {
            return View();
        }

        [Description("合同开票")]
        [HttpPost]
        public string invoicejson(FormCollection condition)
        {
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "InvoiceDate DESC";
            }

            string whereStr = string.Empty;
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "c1.ConName,c1.ConNumber,co1.ConName,co1.ConNumber")
                    {
                        whereStr += string.Format(" and ((CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConName ELSE co.ConrName END) {0} '%{1}%' OR (CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConNumber ELSE co.ConNumber END) {0} '%{1}%')", _child.Contract, _child.Value);
                    }
                    else if (_child.Key == "InvoiceDate")
                    {
                        whereStr += string.Format(" and {0} {1} '{2}'", _child.Key, _child.Contract, _child.Value);
                    }
                }
            }

            DataTable list = new HZShow().GetInvoiceList(PageModel.PageSize, PageModel.PageIndex, whereStr, PageModel.SelectOrder, out recordcount);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }

        [Description("合同收款")]
        [HttpPost]
        public string factjson(FormCollection condition)
        {
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FeeDate DESC";
            }

            string whereStr = string.Empty;
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "c2.ConName,c2.ConNumber,co2.ConName,co2.ConNumber")
                    {
                        whereStr += string.Format(" and ((CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConName ELSE co.ConrName END) {0} '%{1}%' OR (CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConNumber ELSE co.ConNumber END) {0} '%{1}%')", _child.Contract, _child.Value);
                    }
                    else if (_child.Key == "FeeDate")
                    {
                        whereStr += string.Format(" and {0} {1} '{2}'", _child.Key, _child.Contract, _child.Value);
                    }
                }
            }

            DataTable list = new HZShow().GetFactList(PageModel.PageSize, PageModel.PageIndex, whereStr, PageModel.SelectOrder, out recordcount);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }

        [Description("合同付款2")]
        [HttpPost]
        public string subfactjson()
        {
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "SubFeeFactDate DESC";
            }

            string whereStr = string.Empty;
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "c3.ConName,c3.ConNumber,co3.ConName,co3.ConNumber")
                    {
                        whereStr += string.Format(" and ((CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConSubName ELSE co.ConrName END) {0} '%{1}%' OR (CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConSubNumber ELSE co.ConNumber END) {0} '%{1}%')", _child.Contract, _child.Value);
                    }
                    else if (_child.Key == "SubFeeFactDate")
                    {
                        whereStr += string.Format(" and {0} {1} '{2}'", _child.Key, _child.Contract, _child.Value);
                    }
                }
            }

            DataTable list = new HZShow().GetSubFactList(PageModel.PageSize, PageModel.PageIndex, whereStr, PageModel.SelectOrder, out recordcount);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }

        #endregion
    }
}
