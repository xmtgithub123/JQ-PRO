using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Web;
using System.IO;

namespace Design.Controllers
{
    [Description("DesTaskCheck")]
    public partial class DesTaskCheckController : CoreController
    {
        private DBSql.Design.DesTaskCheck op = new DBSql.Design.DesTaskCheck();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult TaskCheckList()
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.taskId = Request.Params["taskId"] == null ? "0" : Request.Params["taskId"].ToString();
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.DesignEditType = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseData().GetDataSetByEngName("DesignEditType").Select(p => new { BaseID = p.BaseID, BaseName = p.BaseName }));

            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (Request.Params["taskId"] != null)
            {
                PageModel.SelectCondtion.Add("TaskID", Request.Params["taskId"].ToString());
            }

            var list = op.DesTaskCheckList(PageModel);


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list,
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult DesTaskCheckInfo()
        {
            var model = new DesTaskCheck();
            model.CheckEmpIDName = userInfo.EmpName;
            model.CheckDate = System.DateTime.Now;

            ViewBag.fileIds = "";
            if (Request.Params["fileIds"] != null)
            {
                ViewBag.fileIds = string.Join(",", (from n in Request.Params["fileIds"].ToString().Split(',') where n != "" select n).ToArray());
            }
            if (Request.Params["taskId"] != null)
            {
                ViewBag.TaskID = Request.Params["taskId"].ToString();
            }

            ViewBag.Url = string.Format("http://{0}:{1}{2}", Request.Url.Host, Request.Url.Port, Url.Action("SaveImg"));
            ViewBag.CheckCurrentEmpID = userInfo.EmpID;

            //删除临时贴图
            List<DataModel.Models.DesTaskCheckImage> ImgList = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Where(p => p.CheckID == 0 && p.CreateEmpID == userInfo.EmpID).ToList();
            if (ImgList.Count > 0)
            {
                foreach (var item in ImgList)
                {
                    op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Remove(item);
                }
                op.DbContext.SaveChanges();
            }

            return View("DesTaskCheckInfo", model);
        }
        #endregion


        #region 删除
        [Description("删除")]
        public ActionResult del(int id)
        {
            int reuslt = 0;
            reuslt = op.DeleteDesTaskCheck(id.ToString());
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new DesTaskCheck();
            DoResult dr = new DoResult();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            try
            {
                int reuslt = 0;
                if (model.Id > 0)
                {
                    op.Edit(model);
                    op.UnitOfWork.SaveChanges();
                }
                else
                {
                    DataModel.Models.DesTask dmTask = new DBSql.Design.DesTask().Get(model.TaskID);
                    if (dmTask == null) throw new Exception("无法获取任务信息");
                    model.ProjID = dmTask.ProjId;
                    model.PhaseID = dmTask.TaskPhaseId;
                    model.SpecialID = dmTask.TaskSpecId;
                    model.CheckEmpId = userInfo.EmpID;
                    model.CheckEmpIDName = userInfo.EmpName;
                    model.CheckDate = System.DateTime.Now;
                    model.CheckImage = new byte[] { };
                    op.InsertDesTaskCheck(model, Request.Params["AttachIDDate"] == null ? "" : Request.Params["AttachIDDate"].ToString());
                    //贴图保存
                    List<DataModel.Models.DesTaskCheckImage> ImgList = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Where(p => p.CheckID == 0 && p.CreateEmpID == userInfo.EmpID).ToList();
                    if (ImgList.Count > 0)
                    {
                        foreach (var item in ImgList)
                        {
                            DataModel.Models.DesTaskCheckImage Imgmodel = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().FirstOrDefault(p => p.Id == item.Id);
                            Imgmodel.CheckID = model.Id;
                            op.DbContext.Entry<DataModel.Models.DesTaskCheckImage>(Imgmodel).State = System.Data.Entity.EntityState.Modified;
                            //op.DbContex.set<DataModel.Models.DesTaskCheckImage>().Entry(ImgList).State = System.Data.Entity.EntityState.Modified;
                        }
                        op.UnitOfWork.SaveChanges();
                    }
                    reuslt = model.Id;
                }

                dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            }
            catch (System.Exception)
            {
                dr = DoResultHelper.doSuccess(model.Id, "操作失败");
            }

            return Json(dr);
        }
        #endregion

        public JsonResult Update()
        {
            int reuslt = 0;
            //return  reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            List<DataModel.Models.DesTaskCheckAttach> attachList = new List<DesTaskCheckAttach>();
            if (Request.Params["upData"] != null)
            {
                attachList = new JavaScriptSerializer().Deserialize<List<DataModel.Models.DesTaskCheckAttach>>(Request.Params["upData"].ToString());
                reuslt = op.UpdateDesTaskAttach(attachList);
            }
            DoResult dr = new DoResult();
            dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt.ToString(), "校审意见修改成功！") : DoResultHelper.doSuccess(reuslt.ToString(), "校审意见修改失败！"); ;
            return Json(dr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MisTakeList()
        {
            ViewBag.from = Request.Params["from"] == null ? "" : Request.Params["from"].ToString();
            ViewBag.taskId = Request.Params["taskId"] == null ? "0" : Request.Params["taskId"].ToString();
            return View();
        }

        public string MisTakeJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (Request.Params["taskId"] == null)
            {
                throw new Common.JQException("无法获取数据");
            }

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            DataModel.Models.DesTask TaskModel = new DBSql.Design.DesTask().Get(Common.ExtensionMethods.Value<int>(Request.Params["taskId"].ToString()));

            DBSql.Core.ModelIsoCheck opModel = new DBSql.Core.ModelIsoCheck();
            var list = opModel.GetPagedList((p => p.SpecialID == TaskModel.TaskSpecId && p.PhaseID == TaskModel.TaskPhaseId && p.DeleterEmpId == 0), PageModel).ToList();
            var IsoCheckList = op.DbContext.Set<DesTaskCheck>().Where(p => p.TaskID == TaskModel.Id).ToList();
            var target = from item in list
                         select new
                         {
                             item.Id,
                             item.PhaseID,
                             PhaseName = item.FK_ModelIsoCheck_PhaseID == null ? "" : item.FK_ModelIsoCheck_PhaseID.BaseName,
                             item.SpecialID,
                             SpecialName = item.FK_ModelIsoCheck_SpecialID == null ? "" : item.FK_ModelIsoCheck_SpecialID.BaseName,
                             item.CheckType,
                             CheckTypeName = item.FK_ModelIsoCheck_CheckType == null ? "" : item.FK_ModelIsoCheck_CheckType.BaseName,
                             item.CheckItem,
                             item.CheckNote,
                             IsCreate = IsoCheckList.Where(p => p.ModelIsoCheckID == item.Id).Count() > 0 ? 1 : 0,
                         };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }

        /// <summary>
        /// 添加到校审意见中
        /// </summary>
        /// <returns></returns>
        public JsonResult AddToDesCheck(int taskId, string isoCheckIds)
        {
            int reuslt = 0;
            DataModel.Models.DesTask dmTask = new DBSql.Design.DesTask().Get(taskId);
            if (dmTask == null) throw new Exception("无法获取任务信息");

            int[] MisTakeIds = (from n in isoCheckIds.Split(',') select Common.ExtensionMethods.Value<int>(n)).ToArray();
            List<ModelIsoCheck> MisTakeList = op.DbContext.Set<DataModel.Models.ModelIsoCheck>().Where(p => MisTakeIds.Contains(p.Id)).ToList();
            try
            {
                foreach (ModelIsoCheck item in MisTakeList)
                {
                    var model = new DesTaskCheck();
                    model.TaskID = taskId;
                    model.CheckErrTypeID = item.CheckErrTypeID;
                    model.CheckNote = item.CheckItem;
                    model.ProjID = dmTask.ProjId;
                    model.PhaseID = dmTask.TaskPhaseId;
                    model.SpecialID = dmTask.TaskSpecId;
                    model.CheckEmpId = userInfo.EmpID;
                    model.CheckEmpIDName = userInfo.EmpName;
                    model.CheckDate = System.DateTime.Now;
                    model.CheckImage = new byte[] { };
                    model.ModelIsoCheckID = item.Id;
                    reuslt = op.InsertDesTaskCheck(model, "");

                    //插入任务、差错关系表中
                    //var IsoCheckModle = new IsoCheck();
                    //IsoCheckModle.DesTaskId = taskId;
                    //IsoCheckModle.IsoCheckModelId = item.Id;
                    //IsoCheckModle.MvcDefaultSave(userInfo.EmpID);
                    //op.DbContext.Set<IsoCheck>().Add(IsoCheckModle);
                    //op.DbContext.SaveChanges();
                }
                reuslt = 1;
            }
            catch (Exception ex)
            {
                reuslt = -1;
            }

            DoResult dr = new DoResult();
            dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt.ToString(), "校审意见添加成功！") : DoResultHelper.doSuccess(reuslt.ToString(), "校审意见添加失败！"); ;
            return Json(dr, JsonRequestBehavior.AllowGet);
        }


        #region 留言

        public string jsonWord(int CheckId)
        {
            var list = op.DbContext.Set<DataModel.Models.DesTaskCheckTalk>().Where(p => p.CheckId == CheckId).Select(p => new { p.TalkContent, p.CreatorEmpId, p.CreatorEmpName, p.CreationTime, p.CreatorDepName }).OrderByDescending(p => p.CreationTime);
            return JavaScriptSerializerUtil.getJson(list);
        }

        public string CreateWord(string TalkContent, int CheckId)
        {
            string talk = HttpUtility.HtmlDecode(TalkContent);
            try
            {
                if (talk.Trim() != "")
                {
                    DataModel.Models.DesTaskCheckTalk TaskModel = new DesTaskCheckTalk();
                    TaskModel.TalkContent = TalkContent;
                    TaskModel.CheckId = CheckId;
                    TaskModel.MvcDefaultSave(userInfo.EmpID);
                    TaskModel.CreatorDepName = userInfo.EmpDepName;
                    op.DbContext.Set<DataModel.Models.DesTaskCheckTalk>().Add(TaskModel);
                    op.UnitOfWork.SaveChanges();
                    return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(1, "留言成功！"));
                }
                else
                {
                    return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "留言内容不能为空！"));
                }
            }
            catch (Exception)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "留言失败！！！"));
            }

        }

        #endregion

        /// <summary>
        /// 保存贴图
        /// </summary>
        /// <returns></returns>
        public int SaveImg(int EmpID)
        {
            if (Request.Files.Count > 0)
            {
                var stream = Request.Files[0].InputStream;
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, System.IO.SeekOrigin.Begin);

                DataModel.Models.DesTaskCheckImage model = new DesTaskCheckImage();
                DataModel.Models.BaseEmployee dmEmp = new DBSql.Sys.BaseEmployee().Get(EmpID);
                model.CreateEmpID = dmEmp.EmpID;
                model.CreateEmpName = dmEmp.EmpName;
                model.CheckImage = bytes;
                model.CreateDateTime = System.DateTime.Now;
                model.CheckID = 0;
                op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Add(model);
                op.UnitOfWork.SaveChanges();
                return model.Id;
            }
            else
            {
                return 0;
            }
        }

        public FileResult GetImg(int Id)
        {
            //var ImgFile = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().FirstOrDefault(p => p.Id == Id).CheckImage;
            ////int i = ImgFile.GetUpperBound(0);
            //int i = ImgFile.Length;
            //Response.ContentType = "jpg";
            //Response.OutputStream.Write(ImgFile, 0, i);
            //Response.End();
            byte[] ImgFile = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().FirstOrDefault(p => p.Id == Id).CheckImage;
            string FileName = string.Format("TaskCheckImage_{0}.jpg", Id);
            string TargetPath = Path.Combine(JQ.Util.IOUtil.GetTempPath(), FileName);
            if (!System.IO.File.Exists(TargetPath))
            {
                FileStream fs = new FileStream(TargetPath, FileMode.Create);
                fs.Write(ImgFile, 0, ImgFile.Length);
                fs.Close();
            }
            if (!System.IO.File.Exists(TargetPath))
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                //return File(TargetPath, "application/octet-stream", string.Format("图片{0}.jpg", Id));
                return File(TargetPath, "image/jpeg", string.Format("图片{0}.jpg", Id));
            }
        }

        public void DelImg(int Id)
        {
            DataModel.Models.DesTaskCheckImage model = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Remove(model);
                op.UnitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// 获取所有图片
        /// </summary>
        public string GetImgList()
        {
            List<DataModel.Models.DesTaskCheckImage> ImgList = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Where(p => p.CheckID == 0 && p.CreateEmpID == userInfo.EmpID).ToList();
            return JavaScriptSerializerUtil.getJson(ImgList.Select(p => p.Id));
        }

        /// <summary>
        /// 获取Id 图片
        /// </summary>
        /// <returns></returns>
        public string GetImgListById(int Id)
        {
            List<DataModel.Models.DesTaskCheckImage> ImgList = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Where(p => p.CheckID == Id).ToList();
            return JavaScriptSerializerUtil.getJson(ImgList.Select(p => p.Id));
        }
    }
}
