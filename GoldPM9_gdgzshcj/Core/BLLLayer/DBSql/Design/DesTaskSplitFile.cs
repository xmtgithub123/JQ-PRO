using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design
{

    public class SpllitFileDisplay
    {
        public int ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 其他格式
        /// </summary>
        public List<SpllitFile> Extensions
        {
            get; set;
        }

        public long Size
        {
            get; set;
        }

    }

    public class SpllitFile
    {
        public int ID
        {
            get; set;
        }

        public string Extension
        {
            get; set;
        }

        public long Size
        {
            get; set;
        }

        public bool IsSigned
        {
            get; set;
        }
    }

    public class DesTaskSplitFile : EFRepository<DataModel.Models.DesTaskSplitFile>, IDisposable
    {
        /// <summary>
        /// 显示该附件最新版本的
        /// </summary>
        /// <returns></returns>
        public List<SpllitFileDisplay> GetSplitFiles(long attachID)
        {
            var attach = this.DbContext.Set<DataModel.Models.BaseAttach>().FirstOrDefault(m => m.AttachID == attachID);
            if (attach == null || attach.AttachVer == 0)
            {
                return new List<SpllitFileDisplay>();
            }
            return GetSplitFiles(attachID, attach.AttachVer);
        }

        public List<SpllitFileDisplay> GetSplitFiles(long attachID, long attachVer)
        {
            var datas = this.DbContext.Set<DataModel.Models.DesTaskSplitFile>().Where(m => m.BaseAttachID == attachID && m.BaseAttachVer == attachVer).ToList().GroupBy(m => System.IO.Path.GetFileNameWithoutExtension(m.Name)).OrderBy(m => m.Key);
            var result = new List<SpllitFileDisplay>();
            var local = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
            foreach (var data in datas)
            {
                //获取出dwg文件
                var splitFile = new SpllitFileDisplay();
                result.Add(splitFile);
                splitFile.Name = data.Key + ".dwg";
                var dwgFile = data.FirstOrDefault(m => Path.GetExtension(m.Name).ToLower() == ".dwg");
                if (dwgFile != null)
                {
                    splitFile.Size = dwgFile.Size;
                    splitFile.ID = dwgFile.ID;
                }
                splitFile.Extensions = new List<SpllitFile>();
                foreach (var file in data)
                {
                    var extension = Path.GetExtension(file.Name).TrimStart('.');
                    if (extension == "dwg")
                    {
                        continue;
                    }
                    var sf = new SpllitFile();
                    splitFile.Extensions.Add(sf);
                    sf.Extension = extension.ToUpper();
                    sf.Size = file.Size;
                    sf.ID = file.ID;
                    if (sf.Extension == "PDF")
                    {
                        //验证是否存在签名文件
                        var path = Path.Combine(local, file.Path);
                        path = Path.Combine(Path.GetDirectoryName(path), "Sign", Path.GetFileName(path));
                        if (File.Exists(path))
                        {
                            var sfSign = new SpllitFile();
                            splitFile.Extensions.Add(sfSign);
                            sfSign.Extension = sf.Extension;
                            sfSign.ID = sf.ID;
                            sfSign.IsSigned = true;
                            sfSign.Size = new FileInfo(path).Length;
                        }
                    }
                }
            }
            return result;
        }

        public DataTable GetPrintFileList(Common.SqlPageInfo queryContext)
        {
            var sbSQL = new StringBuilder();
            sbSQL.Append(" FROM BaseAttach ba");
            sbSQL.Append(" INNER JOIN DesTaskAttachEx ta ON ba.AttachID=ta.AttachId AND ba.AttachVer=ta.AttachVer");
            sbSQL.Append(" INNER JOIN DesTask s ON ba.AttachRefID=s.Id");
            sbSQL.Append(" INNER JOIN DesTaskGroup g ON s.TaskGroupId=g.Id");
            var sbCondition = new StringBuilder(" WHERE 1=1 AND ba.AttachRefTable = 'DesTaskAttach' AND NOT (ta.AttachFlowNode.exist('/root/item[@FlowNodeStatus!=sql:variable(\"@FlowNodeStatus\")]') = 1)");
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@FlowNodeStatus", 3));
            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                sbCondition.Append(" AND (ba.AttachName LIKE @text OR s.TaskName LIKE @text OR g.TaskGroupName LIKE @text OR dbo.F_GetDesTaskGroupPathJson(g.Id) LIKE @text OR dbo.F_GetDesTaskPathJson(s.Id) LIKE @text)");
                sqlParameters.Add(new SqlParameter("@text", "%" + queryContext.TextCondtion + "%"));
            }
            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "EmpID":
                            sbCondition.Append(" AND ba.AttachEmpID=" + de.Value.ToString());
                            break;
                    }
                }
            }
            //分页
            queryContext.PageTotleRowCount = JQ.Util.TypeParse.parse<int>(DAL.DBExecute.ExecuteScalar("SELECT COUNT(1)" + sbSQL.ToString() + sbCondition.ToString(), sqlParameters.ToArray()));
            //queryContext.SelectOrder = "g.ProjId DESC,g.TaskGroupLevel,g.TaskGroupOrderNum,ba.AttachDateUpload";
            queryContext.SelectOrder = "ba.AttachDateUpload DESC";
            return DAL.DBExecute.ExecuteDataTable("SELECT * FROM (SELECT dbo.F_GetDesTaskGroupPathJson(g.Id) AS 'ItemPath1',dbo.F_GetDesTaskPathJson(s.Id) AS 'ItemPath2',ba.AttachID,ba.AttachParentID AS _parentId ,ba.AttachName,ba.AttachExt,ba.AttachDir,ba.AttachOrderPath,ba.AttachPathIDs,ba.AttachSize,ba.AttachDateUpload,ba.AttachDateChange,ba.AttachEmpID,ba.AttachEmpName,ba.AttachVer,ba.AttachTag,ba.AttachGrade,ta.AttachFlowNode,row_number() OVER (ORDER BY " + queryContext.SelectOrder + ") AS row_number" + sbSQL.ToString() + sbCondition.ToString() + ") as tb WHERE tb.row_number BETWEEN " + (((queryContext.PageIndex - 1) * queryContext.PageSize) + 1) + " AND " + (queryContext.PageIndex * queryContext.PageSize), sqlParameters.ToArray());
        }

        /// <summary>
        /// 删除拆分文件
        /// </summary>
        /// <param name="baseAttachId"></param>
        /// <param name="baseAttachVerId"></param>
        /// <returns></returns>
        public int DeleteSplitFile(long baseAttachId, long baseAttachVerId, long baseAttachVer)
        {
            string sql = string.Format("delete from DesTaskSplitFile where BaseAttachID={0} and BaseAttachVerID={1} and BaseAttachVer={2}", baseAttachId, baseAttachVerId, baseAttachVer);
            return DAL.DBExecute.ExecuteNonQuery(sql);
        } 

        private bool _isDisposed = false;
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }
    }
}
