﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-04-20 11:28:46
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DBSql.Archive
{
    public class ArchPaperFolderType : EFRepository<DataModel.Models.ArchPaperFolderType>
    {
        public DataTable GetArchPaperFolderFile(Common.SqlPageInfo condition)
        {
            string RowColumn = @"CASE a.AttachRefTable
                                    WHEN 'ArchPaperFolder' THEN '档案文件'
                                    WHEN 'ArchShareFolderML'
                                    THEN ( SELECT CASE Type
                                                    WHEN 0 THEN '档案：[' + Number + ']' + Name
                                                    WHEN 1 THEN '设计文件：[' + Number + ']' + Name
                                                END
                                            FROM   dbo.ArchPaperFolderType
                                            WHERE  Id IN (
                                                SELECT  CASE apf.ParentId
                                                            WHEN 0 THEN apf.Id
                                                            ELSE apf.ParentId
                                                        END AS Id
                                                FROM    ArchShareFolderML afm
                                                        LEFT JOIN dbo.ArchPaperFolderType apf ON afm.FlderId = apf.Id
                                                WHERE   afm.Id = a.AttachRefID )
                                        )
                                END AS ML ,
                                CASE a.AttachRefTable
                                    WHEN 'ArchPaperFolder' THEN '档案文件'
                                    WHEN 'ArchShareFolderML'
                                    THEN ( SELECT '[' + afm.FileNumber + ']' + afm.FileName
                                            FROM   ArchShareFolderML afm
                                                LEFT JOIN dbo.ArchPaperFolderType apf ON afm.FlderId = apf.Id
                                            WHERE  afm.Id = a.AttachRefID
                                        )
                                END AS JNML ,
                                a.*";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Count(1) FROM    dbo.BaseAttach a WHERE   1 = 1");

            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.AppendFormat(" and (AttachName like '%{0}%' or AttachEmpName like '%{0}%') ", condition.TextCondtion);
            }
            

            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    if (!string.IsNullOrEmpty(de.Key.ToString()) && de.Key.ToString() == "0")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "State":
                            if (de.Value.ToString() == "无效")
                            {
                                strSql.AppendFormat("  AND AttachTag<>''");
                            }
                            else if (de.Value.ToString() == "有效") {
                                strSql.AppendFormat("  AND AttachTag=''");
                            }
                            break;
                        //扩展查询列
                        case "Id":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat("  AND AttachRefID IN (SELECT Id FROM F_GetChildArchById({0})) AND AttachRefTable='ArchShareFolderML' ", de.Value.ToString());
                            }
                            break;
                        case "ArchPaperFolderId":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(@" AND (( AttachRefID = {0}
                                                      AND AttachRefTable = 'ArchPaperFolder'
                                                    )
                                                OR ( AttachRefID IN (
                                                     SELECT Id
                                                     FROM   dbo.ArchShareFolderML
                                                     WHERE  FlderId IN ( SELECT Id
                                                                         FROM   dbo.ArchPaperFolderType
                                                                         WHERE  ArchPaperFolderId = {0} ) )
                                                     AND AttachRefTable = 'ArchShareFolderML'
                                                   ))", de.Value.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString());

            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);

            //------------------------------------------------------//

            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = " AttachName desc ";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString());
        }

        /// <summary>
        /// 删除纸质档案节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteNode(int id)
        {
            StringBuilder strSql = new StringBuilder();
            //1、删除附件表
            strSql.AppendFormat(@"DELETE  FROM dbo.BaseAttach
                                                WHERE AttachRefID IN(SELECT Id

                                                                        FROM   dbo.F_GetChildArchById({0}))
                                                        AND AttachRefTable = 'ArchShareFolderML';", id);
            //2、删除目录表
            strSql.AppendFormat(@"DELETE  FROM dbo.ArchShareFolderML
                                        WHERE   FlderId IN ( SELECT Id
                                                             FROM   dbo.F_GetChildArchById({0}) );", id);
            //3、删除档案表
            strSql.AppendFormat(@"DELETE  FROM dbo.ArchPaperFolderType
                                        WHERE   Id IN ( SELECT  Id
                                                        FROM    dbo.F_GetChildArchType({0}) );", id);

            return DBExecute.ExecuteNonQuery(strSql.ToString());
        }
    }
}
