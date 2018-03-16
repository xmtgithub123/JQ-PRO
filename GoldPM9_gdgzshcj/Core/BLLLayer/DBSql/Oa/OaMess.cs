using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace DBSql.OA
{
    public class OaMess : EFRepository<DataModel.Models.OaMess>
    {
        public List<dynamic> GetList(Common.SqlPageInfo queryContext)
        {
            var query = this.DbContext.Set<DataModel.Models.OaMessRead>().Join(this.DbContext.Set<DataModel.Models.OaMess>(), t => t.Id, t1 => t1.Id, (t, t1) => new { t.Id, t.MessReadEmpId, t.MessReadEmpName, t.MessReadDate, t.MessReadIsDeleted, t1.MessTitle, t1.MessLinkTitle, t1.MessLinkUrl, t1.MessIsDeleted, t1.MessEmpId, t1.MessDate, t1.MessEmpName, t1.MessIsSystem, t1.MessDialogWidth, t1.MessDialogHeight });
            query = query.Where(m => m.MessIsDeleted == false && m.MessReadIsDeleted == false);
            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                query = query.Where(m => m.MessTitle.Contains(queryContext.TextCondtion) || m.MessLinkTitle.Contains(queryContext.TextCondtion));
            }
            if (queryContext.SelectCondtion != null)
            {
                foreach (DictionaryEntry item in queryContext.SelectCondtion)
                {
                    if (item.Value == null || item.Value.ToString() == "")
                    {
                        continue;
                    }
                    switch (item.Key.ToString())
                    {
                        case "MessReadEmpId":
                            {
                                var temp = 0;
                                if (int.TryParse(item.Value.ToString(), out temp))
                                {
                                    query = query.Where(m => m.MessReadEmpId == temp);
                                }
                            }
                            break;
                        case "MessEmpID":
                            {
                                var temp = 0;
                                if (int.TryParse(item.Value.ToString(), out temp))
                                {
                                    query = query.Where(m => m.MessEmpId == Common.ModelConvert.ConvertToDefault<int>(item.Value));
                                }
                            }
                            break;
                        case "Status":
                            if (item.Value.ToString() == "0")
                            {
                                //未读
                                var datetime = DateTime.Parse("1900-01-01");
                                query = query.Where(m => m.MessReadDate == datetime);
                            }
                            else if (item.Value.ToString() == "1")
                            {
                                //已读
                                var datetime = DateTime.Parse("1900-01-01");
                                query = query.Where(m => m.MessReadDate != datetime);
                            }
                            break;
                        case "StartTime":
                            query = query.Where(m => m.MessDate >= (DateTime)item.Value);
                            break;
                        case "EndTime":
                            query = query.Where(m => m.MessDate <= (DateTime)item.Value);
                            break;
                    }
                }
            }
            query = query.OrderByDescending(m => m.MessDate);
            if (!queryContext.ToPageData)
            {
                if (queryContext.PageIndex < 1)
                {
                    queryContext.PageIndex = 1;
                }
                if (queryContext.PageSize == 0)
                {
                    queryContext.PageSize = 10;
                }
                var itemIndex = (queryContext.PageIndex - 1) * queryContext.PageSize;
                queryContext.PageTotleRowCount = query.FutureCount();
                query = query.Skip(itemIndex).Take(queryContext.PageSize);
            }
            else
            {
                queryContext.PageTotleRowCount = 0;
            }
            return query.ToList<dynamic>();
        }

        public bool UpdateReaded(DataModel.Models.OaMessRead oaMessRead)
        {
            if (oaMessRead == null)
            {
                return false;
            }
            if (oaMessRead.MessReadDate == DateTime.Parse("1900-01-01"))
            {
                oaMessRead.MessReadDate = DateTime.Now;
                this.DbContext.SaveChanges();
                Oa.OaMessRead.CacheRemove();
                return true;
            }
            return false;
        }


        public void SetMessageReaded(string idSet, int empID)
        {
            if (string.IsNullOrEmpty(idSet))
            {
                return;
            }
            DAL.DBExecute.ExecuteNonQuery("UPDATE OaMessRead SET MessReadDate=GETDATE() WHERE Id IN (" + idSet + ") AND MessReadEmpId=" + empID);
            Oa.OaMessRead.CacheRemove();
        }

        public void SetMessageUnReaded(string idSet, int empID)
        {
            if (string.IsNullOrEmpty(idSet))
            {
                return;
            }
            DAL.DBExecute.ExecuteNonQuery("UPDATE OaMessRead SET MessReadDate='1900-01-01' WHERE Id IN (" + idSet + ") AND MessReadEmpId=" + empID);
            Oa.OaMessRead.CacheRemove();
        }

        public void DeleteRead(string idSet, int empID)
        {
            if (string.IsNullOrEmpty(idSet))
            {
                return;
            }
            DAL.DBExecute.ExecuteNonQuery("UPDATE OaMessRead SET MessReadIsDeleted=1 WHERE Id IN (" + idSet + ") AND MessReadEmpId=" + empID);
            Oa.OaMessRead.CacheRemove();
        }
    }

    /// <summary>
    /// 发送消息类
    /// </summary>
    public class OaSendMess
    {
        /// <summary>
        /// 消息接收人
        /// </summary>
        public List<int> RecEmpID = new List<int>();

        /// <summary>
        /// 消息标题
        /// </summary>
        public string MessTitle { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessNote { get; set; }

        /// <summary>
        /// 弹出页面 标题
        /// </summary>
        public string MessLinkTitle { get; set; }

        /// <summary>
        /// 消息中 超链接
        /// </summary>
        public string MessLinkUrl { get; set; }

        /// <summary>
        /// 超链接的 关联字段
        /// </summary>
        public string MessRefTable { get; set; }

        /// <summary>
        /// 超连接 关联ID
        /// </summary>
        public int MessRefID { get; set; }

        /// <summary>
        /// 超链接 宽度
        /// </summary>
        public int DialogWidth { get; set; }

        /// <summary>
        /// 超链接 高度
        /// </summary>
        public int DialogHeight { get; set; }

        /// <summary>
        /// 是否系统消息
        /// </summary>
        public bool MessIsSystem { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int EmpID { get; set; }

        #region 私有属性

        private OaMess _opMess = new OaMess();

        //private DBSql.Oa.OaMessRead _opRead = new Oa.OaMessRead();
        #endregion

        public void SendMess()
        {
            if (RecEmpID.Count == 0)
            {
                return;
            }
            DataModel.Models.OaMess _mess = new DataModel.Models.OaMess();
            _mess.MessDate = System.DateTime.Now;
            _mess.MessTitle = MessTitle;
            _mess.MessLinkTitle = MessLinkTitle == null ? "" : MessLinkTitle;
            _mess.MessLinkUrl = MessLinkUrl == null ? "" : MessLinkUrl;
            _mess.MessNote = MessNote == null ? "" : MessNote;
            _mess.MessRefTable = MessRefTable == null ? "" : MessRefTable;
            _mess.MessRefID = MessRefID == 0 ? 0 : MessRefID;
            _mess.MessDialogWidth = DialogWidth == 0 ? 800 : DialogWidth;
            _mess.MessDialogHeight = DialogHeight == 0 ? 600 : DialogHeight;

            DataModel.Models.BaseEmployee empModel = new DBSql.Sys.BaseEmployee().Get(EmpID);
            _mess.MessEmpId = empModel.EmpID;
            _mess.MessEmpName = empModel.EmpName;
            _mess.MessIsSystem = MessIsSystem == false ? true : MessIsSystem;

            using (var tran = _opMess.DbContext.Database.BeginTransaction())
            {
                try
                {
                    _opMess.Add(_mess);
                    _opMess.DbContext.SaveChanges();

                    foreach (int empId in RecEmpID)
                    {
                        DataModel.Models.OaMessRead readItem = new DataModel.Models.OaMessRead();
                        readItem.Id = _mess.Id;
                        DataModel.Models.BaseEmployee empModelChild = new DBSql.Sys.BaseEmployee().Get(empId);
                        if (empModelChild == null) continue;
                        readItem.MessReadEmpId = empId;
                        readItem.MessReadEmpName = empModelChild.EmpName;
                        readItem.MessReadDate = new DateTime(1900, 1, 1);
                        _opMess.DbContext.Set<DataModel.Models.OaMessRead>().Add(readItem);
                    }
                    _opMess.DbContext.SaveChanges();
                    tran.Commit();
                    DBSql.Oa.OaMessRead.CacheRemove();
                    var t = JQ.Util.IO.MessageMonitor.NotifyAsync(RecEmpID, delegate (int empID)
                   {
                       return new DBSql.Oa.OaMessRead().GetNotifyDatas(empID);
                   });
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
