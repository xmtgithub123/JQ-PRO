using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using JQ.Util;
using System.Linq;

namespace Common
{
    /// <summary>
    /// SqlPageInfo 的摘要说明。
    /// </summary>
    [Serializable]
    public class SqlPageInfo
    {
        public SqlPageInfo()
        {
            this.PageSize = TypeHelper.parseInt(HttpContext.Current.Request["rows"], 20);
            this.PageIndex = TypeHelper.parseInt(HttpContext.Current.Request["page"], 1);


            string sort = HttpContext.Current.Request["sort"];
            string order = HttpContext.Current.Request["order"];
            if (sort.isNotEmpty())
            {
                this.SelectOrder = sort + (order.isNotEmpty() ? " " + order : "asc");
            }
            string arrs = HttpContext.Current.Request["fields[]"];
            if (arrs.isNotEmpty())
            {
                var field = string.Format("new({0})", arrs);
                this.SelectField = field;
            }
            string queryInfo = HttpContext.Current.Request["queryInfo"];
            if (queryInfo.isNotEmpty())
            {
                try
                {
                    this.Filter = JavaScriptSerializerUtil.objectToEntity<List<Data.Extenstions.Filter>>(queryInfo);

                    SetFilter();
                }
                catch
                {
                    this.Filter = null;
                }
            }
            SelectCondtion = new Hashtable();
        }
        public List<Data.Extenstions.Filter> Filter { get; set; }

        /// <summary>
        /// 是否启用分页
        /// </summary>
        public bool ToPageData { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int PageTotleRowCount { get; set; }

        /// <summary>
        /// 查询的条件
        /// </summary>
        public Hashtable SelectCondtion { get; set; }

        /// <summary>
        /// 查询文本
        /// </summary>
        public string TextCondtion { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 排序的ORDER
        /// </summary>
        public string SelectOrder { get; set; }


        /// <summary>
        /// 查询字段
        /// </summary>
        public string SelectField { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string Predicate { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public object[] PredicateValue { get { return this.predicateValue; } }
        private object[] predicateValue;

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<PredicateInfo> QueryTerm
        {
            set
            {
                List<object> Queryval = new List<object>();
                var QueryCount = value.Count;
                for (int i = 0; i < QueryCount; i++)
                {
                    var model = value[i];
                    object o;
                    switch (model.QueryType)
                    {
                        case "Int":
                            o = Convert.ToInt32(model.QueryValue);
                            break;
                        case "Date":
                            if (model.QueryContract == "<=")
                            {
                                o = Convert.ToDateTime(model.QueryValue).AddDays(1).AddSeconds(-1);
                            }
                            else
                            {
                                o = Convert.ToDateTime(model.QueryValue);
                            }
                            break;
                        case "Decimal":
                            o = Convert.ToDecimal(model.QueryValue);
                            break;
                        case "Bool":
                            bool bValue = false;
                            if (model.QueryValue == "1" || model.QueryValue == "true") bValue = true;
                            o = bValue;
                            break;
                        default:
                            o = model.QueryValue;
                            break;
                    }
                    Queryval.Add(o);
                }
                predicateValue = Queryval.ToArray();
            }
        }


        private void SetFilter()
        {
            Predicate = "1=1";
            int index = 0;
            var Term = new List<PredicateInfo>();
            for (int i = 0; i < Filter.Count; i++)
            {
                var model = Filter[i];
                if (model.isGroup)
                {
                    Predicate += string.Format(" {0} (1=1", model.ljf);
                    ContactField(model.list, ref index, Term);
                    Predicate += ")";
                }
                else
                {
                    ContactField(model.list, ref index, Term);
                }
            }
            if (Term.Count > 0)
            {
                QueryTerm = Term;
            }
        }

        private void ContactField(List<Data.Extenstions.FilterChilde> childe, ref int index, List<PredicateInfo> Term)
        {
            for (int i = 0; i < childe.Count; i++)
            {
                var model = childe[i];

                var Info = new PredicateInfo()
                {
                    QueryField = model.Key,
                    QueryType = model.filedType,
                    QueryValue = model.Value,
                    QueryContract = model.Contract
                };

                //多字段关键字筛选用“,”分割开来
                if (model.Contract == "like")
                {
                    int LikeIndex = index++;
                    var Arr = model.Key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (Arr.Count > 0)
                    {
                        string ArrValue = "";
                        for (int j = 0; j < Arr.Count; j++)
                        {
                            if (ArrValue == "")
                            {
                                ArrValue += string.Format("{0}.Contains(@{1})", Arr[j], LikeIndex);
                            }
                            else
                            {
                                ArrValue += string.Format(" or {0}.Contains(@{1})", Arr[j], LikeIndex);
                            }
                        }
                        Predicate += string.Format(" and ({0})", ArrValue);
                    }
                    Term.Add(Info);
                }
                else if (model.Contract == "in")
                {
                    var Arr = model.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (Arr.Count > 0)
                    {
                        string ArrValue = "";
                        for (int j = 0; j < Arr.Count; j++)
                        {
                            Info = new PredicateInfo()
                            {
                                QueryField = model.Key,
                                QueryType = model.filedType,
                                QueryValue = Arr[j],
                                QueryContract = "="
                            };
                            if (ArrValue == "")
                            {
                                ArrValue += string.Format("{0}=@{1}", model.Key, index++);
                            }
                            else
                            {
                                ArrValue += string.Format(" or {0}=@{1}", model.Key, index++);
                            }
                            Term.Add(Info);
                        }
                        Predicate += string.Format(" and ({0})", ArrValue);
                    }
                }
                else
                {
                    if (model.Key == "DeleterEmpId")
                    {
                        if (model.Value == "0")
                        {
                            Predicate += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, index++);
                        }
                        else
                        {
                            Predicate += string.Format(" {0} {1} {2} ", model.ljf, model.Key, "<>0");
                        }
                    }
                    else
                    {
                        Predicate += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, index++);
                    }
                    Term.Add(Info);
                }
            }
        }

        /// <summary>
        /// 设置sql 查询条件
        /// </summary>
        /// <returns></returns>
        public SqlPageInfo SetSqlPrams(List<System.Data.SqlClient.SqlParameter> param, ref string SqlWhere)
        {
            SetFilterSql(ref param, ref SqlWhere);
            return this;
        }


        private void SetFilterSql(ref List<System.Data.SqlClient.SqlParameter> _param, ref string SqlWhere)
        {
            Predicate = "1=1";
            int index = 0;
            for (int i = 0; i < Filter.Count; i++)
            {
                var model = Filter[i];
                if (model.isGroup)
                {
                    Predicate += string.Format(" {0} (1=1", model.ljf);
                    SqlWhere += ContactFieldSql(model.list, ref index, ref _param);
                    Predicate += ")";
                }
                else
                {
                    SqlWhere += ContactFieldSql(model.list, ref index, ref _param);
                }
            }
        }

        private string ContactFieldSql(List<Data.Extenstions.FilterChilde> childe, ref int index, ref List<System.Data.SqlClient.SqlParameter> Term)
        {
            for (int i = 0; i < childe.Count; i++)
            {
                var model = childe[i];

                //多字段关键字筛选用“,”分割开来
                if (model.Contract == "like")
                {
                    //int LikeIndex = index++; 为LikeIndex=index，然后index再++导致出现两个同名参数
                    int LikeIndex = ++index;
                    var Arr = model.Key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (Arr.Count > 0)
                    {
                        string ArrValue = "";
                        for (int j = 0; j < Arr.Count; j++)
                        {
                            if (ArrValue == "")
                            {
                                ArrValue += string.Format("{0} like ('%'+@{1}+'%')", Arr[j], LikeIndex);
                            }
                            else
                            {
                                ArrValue += string.Format(" or {0} like ('%'+@{1}+'%')", Arr[j], LikeIndex);
                            }
                        }
                        System.Data.SqlClient.SqlParameter _para = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", LikeIndex), System.Data.SqlDbType.VarChar);
                        _para.Value = model.Value.ToString();
                        Term.Add(_para);
                        return string.Format(" and ({0})", ArrValue);
                    }
                }
                else if (model.Contract == "in")
                {
                    var Arr = model.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (Arr.Count > 0)
                    {
                        string ArrValue = "";
                        for (int j = 0; j < Arr.Count; j++)
                        {
                            if (ArrValue == "")
                            {
                                ArrValue += string.Format("{0}=@{1}", model.Key, ++index);
                            }
                            else
                            {
                                ArrValue += string.Format(" or {0}=@{1}", model.Key, ++index);
                            }
                            System.Data.SqlClient.SqlParameter _para = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.Int);
                            _para.Value = Arr[j].Value<int>();
                            Term.Add(_para);
                        }
                        return string.Format(" and ({0})", ArrValue);
                    }
                }
                else
                {
                    string sql = "";
                    switch (model.filedType)
                    {
                        case "Int":
                            sql += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, ++index);
                            System.Data.SqlClient.SqlParameter _para = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.Int);
                            _para.Value = model.Value.Value<int>();
                            Term.Add(_para);
                            Term.Add(new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.Int));
                            break;
                        case "Date":
                            sql += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, ++index);
                            System.Data.SqlClient.SqlParameter _paraDate = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.DateTime);
                            _paraDate.Value = model.Value.Value<DateTime>();
                            Term.Add(_paraDate);
                            break;
                        case "Decimal":
                            sql += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, ++index);
                            System.Data.SqlClient.SqlParameter _paraDecimal = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.Decimal);
                            _paraDecimal.Value = model.Value.Value<decimal>();
                            Term.Add(_paraDecimal);
                            break;
                        case "Bool":
                            sql += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, ++index);
                            System.Data.SqlClient.SqlParameter _paraBit = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.Bit);
                            if (model.Value == "1" || model.Value == "true")
                            {
                                _paraBit.Value = true;
                            }
                            else
                            {
                                _paraBit.Value = false;
                            }
                            Term.Add(_paraBit);
                            break;
                        default:
                            sql += string.Format(" {0} {1} {2} @{3}", model.ljf, model.Key, model.Contract, ++index);
                            System.Data.SqlClient.SqlParameter _paraStr = new System.Data.SqlClient.SqlParameter(string.Format("@{0}", index), System.Data.SqlDbType.VarChar);
                            _paraStr.Value = model.Value.Value<string>();
                            Term.Add(_paraStr);
                            break;
                    }
                    return sql;
                }
            }
            return "";
        }
    }




    public class PredicateInfo
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryField { get; set; }


        /// <summary>
        /// 条件类别
        /// </summary>
        public string QueryType { get; set; }


        /// <summary>
        /// 条件值
        /// </summary>
        public string QueryValue { get; set; }


        /// <summary>
        /// 条件运算
        /// </summary>
        public string QueryContract { get; set; }
    }
}
