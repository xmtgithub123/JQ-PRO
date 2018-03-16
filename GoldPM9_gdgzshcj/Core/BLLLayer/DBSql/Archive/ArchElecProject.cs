﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-30 16:36:50
#endregion
using System.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using System.Collections.Generic;
namespace DBSql.Archive
{
    public class ArchElecProject : EFRepository<DataModel.Models.ArchElecProject>
    {
        /// <summary>
        /// 查看节点是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        private int IsExistsNode(DataModel.Models.ArchElecProject model, IEnumerable<DataModel.Models.ArchElecProject> arr)
        {
            var n = arr.Where(p => p.ElecType == model.ElecType && p.ElecCode == model.ElecCode).FirstOrDefault();

            if (model.ElecType == Convert.ToInt32(DataModel.VolCatalogType.专业))
            {
                n = arr.Where(p => p.ElecType == model.ElecType && p.ElecCode == model.ElecCode && p.ElecName == model.ElecName).FirstOrDefault();
            }

            if (n == null) return model.Id;
            return n.Id;
        }
        private List<int> add = new List<int>() ;
        private void MarkDir(DataModel.Models.Project projModel, DataModel.EmpSession emp, string parentID, DataTable dtDir, IEnumerable<DataModel.Models.ArchElecProject> arr,string ParentType="Project",int ParentID = 0)
        {
            #region 目录节点
            var row = dtDir.Select(string.Format("ParentId={0} and ParentType='{1}'", parentID, ParentType));
            //int ParentID = 0;
            for (int i = 0; i < row.Count(); i++)
            {
                var item = row[i];

                string ElecName = item["Name"].ToString();
                int ElecCode = Convert.ToInt32(item["Id"]);
                string Type = item["Type"].ToString();
                string _parentType = item["ParentType"].ToString();
                int ElecType = 0;
                switch (Type)
                {
                    case "Project":
                        ElecType = Convert.ToInt32(DataModel.VolCatalogType.项目);
                        if(item["ParentId"].ToString() != "0") ElecType = Convert.ToInt32(DataModel.VolCatalogType.工程);
                        break;
                    case "TaskGroup":
                        if (_parentType == "Project")
                        {
                            ElecType = Convert.ToInt32(DataModel.VolCatalogType.工程); // 子项
                        }
                        else
                        {
                            ElecType = Convert.ToInt32(DataModel.VolCatalogType.阶段);
                        }
                        break;
                    case "Task":
                        ElecType = Convert.ToInt32(DataModel.VolCatalogType.设计成品);
                        break;
                }


                var PModel = new DataModel.Models.ArchElecProject()
                {
                    ProjectId = projModel.Id,
                    ElecCode = ElecCode,
                    ElecGuid = System.Guid.NewGuid().ToString(),
                    ElecNumber = "",
                    ElecName = ElecName,
                    Note = "",
                    ParentId = ParentID,
                    ElecType = ElecType,

                };
                ParentID = IsExistsNode(PModel, arr);
                EditNode(PModel, projModel,emp);
                if (ParentID == 0)
                {
                    PModel.ProjEmpId = projModel.ProjEmpId;
                    PModel.ProjEmpName = projModel.ProjEmpName;
                    Add(PModel);
                    DbContext.SaveChanges();
                    ParentID = PModel.Id;
                }
                add.Add(ParentID);
                MarkDir(projModel, emp, ElecCode.ToString(), dtDir, arr, Type, ParentID);
            }

            
            #endregion
        }

        private void EditNode(DataModel.Models.ArchElecProject m , DataModel.Models.Project model, DataModel.EmpSession emp)
        {
            if (m.ParentId == 0)
            {
                if (model == null) return;

                m.ProjEmpId = model.ProjEmpId;
                m.ProjEmpName = model.ProjEmpName;
                m.CustName = model.CustName;
                m.AttTypeID1 = model.ProjVoltID;
                m.AttTypeID2 = model.ProjTypeID;
                m.AttTypeID3 = model.ProjPropertyID;
               

                if (!string.IsNullOrEmpty(model.ProjPhaseIds)) m.PhaseName = GetBaseName(model.ProjPhaseIds);

                m.ElecName = model.ProjName;
                m.ElecNumber = model.ProjNumber;
                m.DateCreate = model.DateCreate;
            }

            Common.ModelConvert.MvcDefaultSave<DataModel.Models.ArchElecProject>(m, emp);
        }

        private string GetBaseName(string BaseIDs)
        {
            if (BaseIDs == "") return "";

            var Arr = BaseIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var AllData = new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase");
            List<string> result = new List<string>();
            foreach (var item in AllData)
            {
                if (Arr.Contains(item.BaseID.ToString()))
                {
                    result.Add(item.BaseName);
                }
            }

            return string.Join(",",result);
        }


        /// <summary>
        /// 批准完成后，项目进行归档
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public int TaskArch(long taskId,DataModel.EmpSession emp)
        {
            Archive.ArchElecFile dsFile = new Archive.ArchElecFile();
            dsFile.DbContextRepository(this.UnitOfWork, this.DbContext);

            //var arr = GetList(p => p.ProjectId == projID);

            DataTable dtDir = new DBSql.Design.DesTask().GetTaskArchPath(taskId);
            DataTable dtFile = new DBSql.Design.DesTask().GetTaskArchAttach(taskId);

            long projID = dtDir.AsEnumerable().Where(x => x.Field<string>("ParentType") == "Project" && x.Field<long>("ParentId") == 0).Select(x => x.Field<long>("Id")).FirstOrDefault();
            var arr = GetList(p => p.ProjectId == projID);

            var projModel = new DBSql.Project.Project().Get(projID);

            add = new List<int>();
            MarkDir(projModel, emp, "0", dtDir, arr);
            for (int i = 0; i < dtFile.Rows.Count; i++)
            {
                var item = dtFile.Rows[i];
                var elecFiletype = (int)item.Field<long>("AttachID");
                var archProjId = add.First();
                var archElecFileId = add.Last();

                if (item["AttachExt"].ToString() == ".") continue;

                // 防止多次提交归档时附件不会插入多次
                var ba = dsFile.FirstOrDefault(
                    x => x.ArchProjId == archProjId && x.ElecFileType == elecFiletype
                );
                if (ba == null)
                {
                    ba = new DataModel.Models.ArchElecFile();
                    Common.ModelConvert.MvcDefaultSave<DataModel.Models.ArchElecFile>(ba, emp);
                    ba.ParentId = 0;
                    ba.ArchProjId = archProjId;
                    ba.ArchElecFileRefTable = "ArchElecFile";
                    ba.ArchElecFileId = archElecFileId;
                    ba.FileEmpId = emp.EmpID;
                    ba.FileEmpName = emp.EmpName;
                    ba.ElecFileType = elecFiletype;
                    ba.ElecFileName = item["AttachName"].ToString();
                    ba.ElecFileVersionId = int.Parse(item["AttachVer"].ToString());
                    ba.ElecFilePath = item["AttachDir"].ToString();
                    ba.ElecSize = long.Parse(item["AttachSize"].ToString());
                    ba.ElecExt = item["AttachExt"].ToString();
                    ba.ElecFileUnit = "";
                    ba.ElecScret = 0;
                    ba.ElecFileXml = ""; 
                    ba.Note = "";
                    dsFile.Add(ba);
                }
                else
                {
                    Common.ModelConvert.MvcDefaultEdit<DataModel.Models.ArchElecFile>(ba, emp);
                    ba.ElecFileName = item["AttachName"].ToString();
                    ba.ElecFileVersionId = int.Parse(item["AttachVer"].ToString());
                    ba.ElecFilePath = item["AttachDir"].ToString();
                    ba.ElecSize = long.Parse(item["AttachSize"].ToString());
                    ba.ElecExt = item["AttachExt"].ToString();
                    dsFile.Edit(ba);
                }
            }

            this.UnitOfWork.SaveChanges();
            return dtFile.Rows.Count ;
        }
    }
}
