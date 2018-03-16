﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-21 14:33:53
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

namespace DBSql.Sys
{
    public class BaseAttachVer : EFRepository<DataModel.Models.BaseAttachVer>, IDisposable
    {

        private bool isDisposed;
        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }

        /// <summary>
        /// 获取 会签文件的版本
        /// </summary>
        /// <param name="MutiSignID"></param>
        /// <param name="SelIds"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDesTaskAttachVerByMuli(int MutiSignID, string SelIds)
        {
            string sql = @"
                -- 获取 待校审附件
                SELECT  ba.AttachID ,
                        bat.AttachParentID AS _parentId,
                        bat.AttachName ,
                        bat.AttachExt ,
                        bat.AttachOrderPath ,
                        bat.AttachPathIDs ,
                        ba.AttachSize ,
                        ba.AttachDateUpload ,
                        ba.AttachDateChange ,
                        ba.AttachEmpID ,
                        ba.AttachEmpName ,
                        ba.AttachVer ,
                        bat.AttachVer as NewAttachVer,
                        bat.AttachTag ,
                        bat.AttachGrade 
                        --ISNULL(dbo.F_GetBaseAttachPathJson(ba.AttachID), '') AS BaseAttachPathJson ,
                        --ta.AttachFlowNode
                INTO    #approveAttach
                FROM    BaseAttachVer AS ba
                        LEFT JOIN DesMutiSignAttach ta ON ba.AttachID = ta.AttachId AND ba.AttachVer = ta.AttachVer
                        LEFT JOIN BaseAttach bat on ba.AttachID=bat.AttachID 
                WHERE   1=1
				        -- 节点状态: 0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 停止（红色） 5 回退（橙色）
                        AND bat.AttachId IN (SELECT ID FROM dbo.split(@SelIds, ','))
                        AND bat.AttachExt <> '.'
                        AND ta.MutiSignID=@MutiSignID ";
                        
            sql += @"
                -- 取得待校审附件的父节点信息（即文件夹）
                DECLARE @approveAttachPathIds VARCHAR(MAX)
                SET @approveAttachPathIds = (SELECT    AttachPathIDs + ','
                                                FROM      #approveAttach
                                                WHERE     AttachPathIDs <> ''
                                            FOR
                                                XML PATH('')
                                            )

                SELECT a.*
                FROM(
                    SELECT    
                        ba.AttachID,
                        ba.AttachParentID AS _parentId,
                        ba.AttachName,
                        ba.AttachExt,
                        ba.AttachOrderPath,
                        ba.AttachPathIDs,
                        ba.AttachSize,
                        ba.AttachDateUpload,
                        ba.AttachDateChange,
                        ba.AttachEmpID,
                        ba.AttachEmpName,
                        ba.AttachVer,
                        ba.AttachVer as NewAttachVer,
                        ba.AttachTag,                        
                        ba.AttachGrade
                        --'' AS BaseAttachPathJson,
                        --'' AS AttachFlowNode
                    FROM      BaseAttach AS ba
                    WHERE     ba.AttachID IN(
                            SELECT  ID
                            FROM    dbo.Split(@approveAttachPathIds, ','))

                    UNION ALL

                    SELECT    ba.AttachID,
                            ba._parentId,
                            ba.AttachName,
                            ba.AttachExt,
                            ba.AttachOrderPath,
                            ba.AttachPathIDs,
                            ba.AttachSize,
                            ba.AttachDateUpload,
                            ba.AttachDateChange,
                            ba.AttachEmpID,
                            ba.AttachEmpName,
                            ba.AttachVer,
                            ba.NewAttachVer,
                            ba.AttachTag,
                            ba.AttachGrade
                            --ba.BaseAttachPathJson,
                            --ba.AttachFlowNode

                    FROM      #approveAttach AS ba
                ) AS a
                ORDER BY a.AttachExt, a.AttachOrderPath

                IF OBJECT_ID('tempdb..#approveAttach') IS NOT NULL
                    BEGIN
                        DROP TABLE #approveAttach
                    END
                --END IF
            ";

            SqlParameter[] paras = {
                new SqlParameter("@MutiSignID",SqlDbType.BigInt),
                new SqlParameter("@SelIds",SqlDbType.VarChar)
            };
            paras[0].Value = MutiSignID;
            paras[1].Value = SelIds;

            DataTable dt = DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);

            var r = dt.AsEnumerable().Select(ba => new
            {
                AttachID = ba.Field<long>("AttachID"),
                _parentId = ba.Field<long>("_parentId"),
                AttachName = ba.Field<string>("AttachName"),
                AttachExt = ba.Field<string>("AttachExt"),
                AttachOrderPath = ba.Field<string>("AttachOrderPath"),
                AttachPathIDs = ba.Field<string>("AttachPathIDs"),
                AttachSize = ba.Field<long>("AttachSize"),
                AttachDateUpload = ba.Field<DateTime>("AttachDateUpload"),
                AttachDateChange = ba.Field<DateTime>("AttachDateChange"),
                AttachEmpID = ba.Field<int>("AttachEmpID"),
                AttachEmpName = ba.Field<string>("AttachEmpName"),
                AttachVer = ba.Field<int>("AttachVer"),
                NewAttachVer=ba.Field<int>("NewAttachVer"),
                AttachTag = ba.Field<string>("AttachTag"),
                AttachGrade = ba.Field<int>("AttachGrade"),
                //AttachFlowNodeJson = Common.ModelConvert.Xml2Json(ba.Field<string>("AttachFlowNode"))
            });

            return r;
        }
    }
}
