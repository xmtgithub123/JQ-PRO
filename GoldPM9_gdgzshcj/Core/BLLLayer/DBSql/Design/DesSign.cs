using Common;
using DAL;
using DataModel;
using JQ.Util.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DBSql.Design
{
    public class DesSign
    {

        /// <summary>
        /// 获取 某设计任务已审批完成的文件，及其对应审批节点信息
        /// </summary>
        /// <param name="TaskId">要归档的任务Id</param>
        /// <param name="Extension">文件类型筛选，为空为全部</param>
        public DataTable GetTaskAttachSignData(long TaskId, string Extension = "")
        {
            string sql = @"
                -- 获取该任务下所以校审完成附件
                SELECT  ba.*, 
						ta.AttachFlowNode,
                        '' AS MutiSignNode
                FROM    dbo.BaseAttach AS ba
                        INNER JOIN dbo.DesTaskAttachEx ta ON ba.AttachID = ta.AttachId AND ba.AttachVer = ta.AttachVer
                WHERE   ba.AttachRefTable = 'DesTaskAttach'
                        AND ba.AttachRefID IN ( @TaskId )
				        -- 节点状态: 0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 停止（红色） 5 回退（橙色）
                        AND NOT ( ta.AttachFlowNode.exist('/root/item[@FlowNodeStatus!=sql:variable(""@FlowNodeStatus"")]') = 1 )
            ";
            SqlParameter[] paras = {
                new SqlParameter("@TaskId",SqlDbType.BigInt),
                new SqlParameter("@FlowNodeStatus", SqlDbType.Int)
            };
            paras[0].Value = TaskId;
            paras[1].Value = (int)FlowNodeStatus.已完成;

            if (!String.IsNullOrWhiteSpace(Extension))
            {
                sql += String.Format(@" AND ba.AttachExt IN ({0}) ", Extension.ToLower());
            }

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        /// <summary>
        /// 获取 一些设计文件拆图转换后的文件，及其对应审批节点信息
        /// </summary>
        /// <param name="AttachID">附件ID</param>
        public DataTable GetAttachsSignData(string AttachIDs)
        {
            string sql = @"
                -- 获取该任务下所以校审完成附件
                SELECT  ba.*, 
						ta.AttachFlowNode,
                        ISNULL((
							SELECT mr.RecSpecName AS '@RecSpecName', mr.RecEmpName AS '@RecEmpName', CAST(DATEPART(yyyy,mr.RecTime) AS VARCHAR) + '.' + CAST(DATEPART(mm,mr.RecTime) AS VARCHAR) AS '@RecTime'
							FROM dbo.DesMutiSignRec mr LEFT JOIN dbo.DesMutiSign ms on mr.MutiSignId=ms.Id
                            INNER JOIN dbo.DesMutiSignAttach ma ON mr.MutiSignId = ma.MutiSignID 
							WHERE mr.RecNeedSign = 1 AND ma.AttachID = ba.AttachID AND ma.AttachVer = ba.AttachVer
                            AND ms.DeleterEmpId=0   AND MutiStatus=1 AND RecStatus=2	FOR XML PATH('item'), ROOT('root')
						), '') AS MutiSignNode
                FROM    dbo.BaseAttach AS ba
                        INNER JOIN dbo.DesTaskAttachEx ta ON ba.AttachID = ta.AttachId AND ba.AttachVer = ta.AttachVer
                WHERE   ba.AttachID IN ( SELECT ID FROM dbo.Split( @AttachIDs,',') )
                        -- 节点状态: 0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 停止（红色） 5 回退（橙色）
                        AND NOT ( ta.AttachFlowNode.exist('/root/item[@FlowNodeStatus!=sql:variable(""@FlowNodeStatus"")]') = 1 )
            ";
            SqlParameter[] paras = {
                new SqlParameter("@AttachIDs",SqlDbType.NVarChar),
                new SqlParameter("@FlowNodeStatus", SqlDbType.Int)
            };
            paras[0].Value = AttachIDs;
            paras[1].Value = (int)FlowNodeStatus.已完成;

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        /// <summary>
        /// 获取 某设计任务拆图转换后的文件，及其对应审批节点信息
        /// </summary>
        /// <param name="TaskId">要归档的任务Id</param>
        /// <param name="Extension">文件类型筛选，为空为全部</param>
        public DataTable GetTaskSplitAttachSignData(long TaskId, string Extension = "")
        {
            string sql = @"
                -- 获取该任务下所以校审完成附件
                SELECT 
                        ts.*, 
						ta.AttachFlowNode,
						ISNULL((
							SELECT mr.RecSpecName AS '@RecSpecName', mr.RecEmpName AS '@RecEmpName', CAST(DATEPART(yyyy,mr.RecTime) AS VARCHAR) + '.' + CAST(DATEPART(mm,mr.RecTime) AS VARCHAR) AS '@RecTime'
							FROM dbo.DesMutiSignRec mr LEFT JOIN dbo.DesMutiSign ms on mr.MutiSignId=ms.Id
                            INNER JOIN dbo.DesMutiSignAttach ma ON mr.MutiSignId = ma.MutiSignID 
							WHERE mr.RecNeedSign = 1 AND ma.AttachID = ba.AttachID AND ma.AttachVer = ba.AttachVer
                            AND ms.DeleterEmpId=0   AND MutiStatus=1 AND RecStatus=2
                            --AND  (select count(1) from DesTaskSplitFile mtsf where mtsf.BaseAttachID=ba.AttachID and mtsf.BaseAttachVer=ba.AttachVer and charindex
                            --(','+CONVERT(varchar(max),mtsf.ID)+',',(','+ma.SplitFileIds+','))>0 and REPLACE(mtsf.Name,'.'+mtsf.Extension,'')=REPLACE(ts.Name,'.'+ts.Extension,''))>0
                            --AND charindex(','+CONVERT(varchar(max),ts.ID)+',',(','+SplitFileIds+',')) >0
							FOR XML PATH('item'), ROOT('root')
						), '') AS MutiSignNode
                FROM    dbo.BaseAttach AS ba
                        INNER JOIN dbo.BaseAttachVer bav ON ba.AttachID = bav.AttachId AND ba.AttachVer = bav.AttachVer
                        INNER JOIN dbo.DesTaskAttachEx ta ON ba.AttachID = ta.AttachId AND ba.AttachVer = ta.AttachVer
                        LEFT JOIN dbo.DesTaskSplitFile ts ON ba.AttachID = ts.BaseAttachID AND bav.AttachVerId = ts.BaseAttachVerID
                WHERE   ba.AttachRefTable = 'DesTaskAttach'
                        AND ba.AttachRefID IN ( @TaskId )
				        -- 节点状态: 0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 停止（红色） 5 回退（橙色）
                        AND NOT ( ta.AttachFlowNode.exist('/root/item[@FlowNodeStatus!=sql:variable(""@FlowNodeStatus"")]') = 1 )
            ";
            SqlParameter[] paras = {
                new SqlParameter("@TaskId",SqlDbType.BigInt),
                new SqlParameter("@FlowNodeStatus", SqlDbType.Int)
            };
            paras[0].Value = TaskId;
            paras[1].Value = (int)FlowNodeStatus.已完成;

            if (!String.IsNullOrWhiteSpace(Extension))
            {
                sql += String.Format(@" AND ts.Extension IN ({0}) ", Extension.ToLower());
            }

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        /// <summary>
        /// 获取 一些设计文件拆图转换后的文件，及其对应审批节点信息
        /// </summary>
        /// <param name="AttachID">附件ID</param>
        public DataTable GetSplitAttachsSignData(string AttachIDs)
        {
            string sql = @"
                -- 获取该任务下所以校审完成附件
                SELECT  
                        ts.*, 
						ta.AttachFlowNode,
						ISNULL((
							SELECT mr.RecSpecName AS '@RecSpecName', mr.RecEmpName AS '@RecEmpName', CAST(DATEPART(yyyy,mr.RecTime) AS VARCHAR) + '.' + CAST(DATEPART(mm,mr.RecTime) AS VARCHAR) AS '@RecTime'
							FROM dbo.DesMutiSignRec mr LEFT JOIN dbo.DesMutiSign ms on mr.MutiSignId=ms.Id
                            INNER JOIN dbo.DesMutiSignAttach ma ON mr.MutiSignId = ma.MutiSignID 
							WHERE mr.RecNeedSign = 1 AND ma.AttachID = ba.AttachID AND ma.AttachVer = ba.AttachVer
                            AND ms.DeleterEmpId=0   AND MutiStatus=1 AND RecStatus=2
                              --AND  (select count(1) from DesTaskSplitFile mtsf where mtsf.BaseAttachID=ba.AttachID and mtsf.BaseAttachVer=ba.AttachVer and charindex
                                --(','+CONVERT(varchar(max),mtsf.ID)+',',(','+ma.SplitFileIds+','))>0 and REPLACE(mtsf.Name,'.'+mtsf.Extension,'')=REPLACE
                                --(ts.Name,'.'+ts.Extension,''))>0
							FOR XML PATH('item'), ROOT('root')
						), '') AS MutiSignNode
                FROM    dbo.BaseAttach AS ba
                        INNER JOIN dbo.BaseAttachVer bav ON ba.AttachID = bav.AttachId AND ba.AttachVer = bav.AttachVer
                        INNER JOIN dbo.DesTaskAttachEx ta ON ba.AttachID = ta.AttachId AND ba.AttachVer = ta.AttachVer
                        LEFT JOIN dbo.DesTaskSplitFile ts ON ba.AttachID = ts.BaseAttachID AND bav.AttachVerId = ts.BaseAttachVerID
                WHERE   ba.AttachID IN ( SELECT ID FROM dbo.Split( @AttachIDs,',') )
                        -- 节点状态: 0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 停止（红色） 5 回退（橙色）
                        AND NOT ( ta.AttachFlowNode.exist('/root/item[@FlowNodeStatus!=sql:variable(""@FlowNodeStatus"")]') = 1 )
            ";
            SqlParameter[] paras = {
                new SqlParameter("@AttachIDs",SqlDbType.NVarChar),
                new SqlParameter("@FlowNodeStatus", SqlDbType.Int)
            };
            paras[0].Value = AttachIDs;
            paras[1].Value = (int)FlowNodeStatus.已完成;

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        /// <summary>
        /// 获取 某设计文件拆图转换后的文件，及其对应审批节点信息
        /// </summary>
        /// <param name="AttachID">附件ID</param>
        /// <param name="AttachVer">附件版本</param>
        public DataTable GetSplitAttachSignData(long AttachID, long AttachVer)
        {
            string sql = @"
                -- 获取该任务下所以校审完成附件
                SELECT   
                        ts.*, 
						ta.AttachFlowNode,
						ISNULL((
							SELECT mr.RecSpecName AS '@RecSpecName', mr.RecEmpName AS '@RecEmpName', CAST(DATEPART(yyyy,mr.RecTime) AS VARCHAR) + '.' + CAST(DATEPART(mm,mr.RecTime) AS VARCHAR) AS '@RecTime'
							FROM dbo.DesMutiSignRec mr LEFT JOIN dbo.DesMutiSign ms on mr.MutiSignId=ms.Id
                            INNER JOIN dbo.DesMutiSignAttach ma ON mr.MutiSignId = ma.MutiSignID 
							WHERE mr.RecNeedSign = 1 AND ma.AttachID = ba.AttachID AND ma.AttachVer = ba.AttachVer
                            AND ms.DeleterEmpId=0   AND MutiStatus=1 AND RecStatus=2
                              --AND  (select count(1) from DesTaskSplitFile mtsf where mtsf.BaseAttachID=ba.AttachID and mtsf.BaseAttachVer=ba.AttachVer and charindex
                                --(','+CONVERT(varchar(max),mtsf.ID)+',',(','+ma.SplitFileIds+','))>0 and REPLACE(mtsf.Name,'.'+mtsf.Extension,'')=REPLACE
                                --(ts.Name,'.'+ts.Extension,''))>0
							FOR XML PATH('item'), ROOT('root')
						), '') AS MutiSignNode
                FROM    dbo.DesTaskAttachEx ta 
                        INNER JOIN dbo.BaseAttachVer bav ON ba.AttachID = bav.AttachId AND ba.AttachVer = bav.AttachVer
                        INNER JOIN dbo.DesTaskAttachEx ta ON ba.AttachID = ta.AttachId AND ba.AttachVer = ta.AttachVer
                        LEFT JOIN dbo.DesTaskSplitFile ts ON ba.AttachID = ts.BaseAttachID AND bav.AttachVerId = ts.BaseAttachVerID
                WHERE   ba.AttachID IN ( @AttachID )
                        AND ba.AttachVer IN ( @AttachVer )
            ";
            SqlParameter[] paras = {
                new SqlParameter("@AttachID",SqlDbType.BigInt),
                new SqlParameter("@AttachVer",SqlDbType.Int),
            };
            paras[0].Value = AttachID;
            paras[1].Value = AttachVer;

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        /// <summary>
        /// 获取 文件签名配置 
        /// </summary>
        /// <param name="rowAttach"></param>
        /// <param name="designDate">制图日期</param>
        /// <returns></returns>
        public Dto.DesTaskAttachSignInfo GetTaskAttachSignInfo(DataRow rowAttach, string designDate)
        {
            // 获取 设校审批 数据
            XDocument flowNodesXML = null;
            try
            {
                flowNodesXML = XDocument.Parse(rowAttach["AttachFlowNode"].ToString());
            }
            catch
            {
                throw new Exception(String.Format("{0} 的设校审批数据获取失败", rowAttach["Name"].ToString()));
            }

            // 获取 会签 数据
            XDocument muiltiNodesXML = null;
            try
            {
                muiltiNodesXML = XDocument.Parse(rowAttach["MutiSignNode"].ToString());
            }
            catch
            {
                // 有可能没有会签数据
            }

            // 获取 设校审批 签名配置
            var roleSigns = new SerializableDictionary<string, string>();
            foreach (XElement el in flowNodesXML.Element("root").Elements("item"))
            {
                if (!roleSigns.ContainsKey(el.Attribute("FlowNodeName").Value))
                {
                    roleSigns.Add(
                        el.Attribute("FlowNodeName").Value,
                        el.Attribute("FlowNodeEmpName").Value
                    );
                    roleSigns.Add(
                        "HQ" + el.Attribute("FlowNodeName").Value,
                        el.Attribute("FlowNodeEmpName").Value
                    );
                }
            }

            // 写入制图日期
            if (!string.IsNullOrEmpty(designDate))
            {
                roleSigns.Add("制图", designDate);
            }

            // 获取 会签 签名配置
            var muilti_n = 0;
            var muiltiSigns = new SerializableDictionary<string, string>();
            if (muiltiNodesXML != null)
            {
                foreach (XElement el in muiltiNodesXML.Element("root").Elements("item"))
                {
                    muilti_n++;
                    // 会签专业
                    muiltiSigns.Add(
                        "HQMajor" + muilti_n.ToString(),
                        el.Attribute("RecSpecName").Value
                    );
                    // 会签人员
                    muiltiSigns.Add(
                        "会签" + muilti_n.ToString(),
                        el.Attribute("RecEmpName").Value
                    );
                    // 会签会签时间
                    muiltiSigns.Add(
                        "HQDate" + muilti_n.ToString(),
                        el.Attribute("RecTime").Value
                    );
                }
            }

            // 构建PDF签名配置对象
            string UpFileRoot = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
            Dto.DesTaskAttachSignInfo signInfo = null;
            //if (rowAttach.Table.Columns.Contains("Extension"))
            //{
            //    // 针对拆分后文件
            //    //signInfo = new Dto.DesTaskAttachSignInfo()
            //    //{
            //    //    ID = (long)rowAttach.Field<int>("ID"),
            //    //    Name = rowAttach["Name"].ToString(),
            //    //    Extension = rowAttach["Extension"].ToString(),
            //    //    Size = rowAttach.Field<long>("Size"),
            //    //    Path = rowAttach["Path"].ToString(),
            //    //    LocalPath = Path.Combine(UpFileRoot, rowAttach["Path"].ToString()),
            //    //    BaseAttachID = rowAttach.Field<long>("BaseAttachID"),
            //    //    BaseAttachVerID = rowAttach.Field<long>("BaseAttachVerID"),
            //    //    BaseAttachVer = rowAttach.Field<int>("BaseAttachVer"),
            //    //    ApproveSigns = roleSigns,
            //    //    MuiltiSigns = muiltiSigns
            //    //};
            //}
            //else if (rowAttach.Table.Columns.Contains("AttachExt"))
            //{
                // 针对原文件
                signInfo = new Dto.DesTaskAttachSignInfo()
                {
                    ID = rowAttach.Field<long>("AttachID"),
                    Name = rowAttach["AttachName"].ToString(),
                    Extension = rowAttach["AttachExt"].ToString().TrimStart('.'),
                    Size = rowAttach.Field<long>("AttachSize"),
                    Path = rowAttach["AttachDir"].ToString(),
                    LocalPath = Path.Combine(UpFileRoot, rowAttach["AttachDir"].ToString()),
                    BaseAttachID = 0,
                    BaseAttachVerID = 0,
                    BaseAttachVer = 0,
                    ApproveSigns = roleSigns,
                    MuiltiSigns = muiltiSigns
                };
            //}
            return signInfo;
        }

        /// <summary>
        /// 任务附件电子签名
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="empSession"></param>
        /// <param name="designDate">制图日期</param>
        public void TaskAttachSign(long TaskId, EmpSession empSession, string designDate)
        {
            string TaskName = new DBSql.Design.DesTask().GetTaskTextPath(TaskId);

            DesSignPDF signPDF = new DesSignPDF();

            // 创建 设计文件统一PDF签名处理程序
            DBSql.Design.DesSignDWG signDWG = new DBSql.Design.DesSignDWG();

            DataTable dtFile = this.GetTaskSplitAttachSignData(TaskId, "'pdf','docx','xlsx'");
            for (int i = 0; i < dtFile.Rows.Count; i++)
            {
                var signAttach = dtFile.Rows[i];
                var ext = signAttach["Extension"].ToString().ToLower();

                // 获取签名信息
                Dto.DesTaskAttachSignInfo signInfo = GetTaskAttachSignInfo(signAttach, designDate);

                // 放到对应的签名处理程序中
                if (ext == "pdf")
                {
                    signPDF.EmpID = empSession.EmpID;
                    signPDF.EmpName = empSession.EmpName;
                    signPDF.TaskId = TaskId;
                    signPDF.TaskName = TaskName;
                    signPDF.SignFiles.Add(signInfo);
                }
                else if (ext == "dwg")
                {
                    signDWG.EmpID = empSession.EmpID;
                    signDWG.EmpName = empSession.EmpName;
                    signDWG.TaskId = TaskId;
                    signDWG.TaskName = TaskName;
                    signDWG.SignFiles.Add(signInfo);
                }
            }

            // 开始签名
            signPDF.BeginSign();
            //signDWG.BeginSign();
        }
    }
}
