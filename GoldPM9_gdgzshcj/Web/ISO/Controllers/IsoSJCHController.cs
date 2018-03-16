using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web;
using Aspose.Words;
using System.Text;
using Common.Data.Extenstions;

namespace Iso.Controllers
{
    [Description("IsoSJCH")]
    public class IsoSJCHController : CoreController
    {
        private DBSql.Iso.IsoSJCH op = new DBSql.Iso.IsoSJCH();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var per = PermissionList("Iso_SJCH");

            var There = QueryBuild<DataModel.Models.IsoSJCH>.True();

            if (per.Contains("allview"))
            {

            }
            else if (per.Contains("dep"))
            {
                There = There.And(p => p.CreatorDepId == userInfo.EmpDepID);
            }
            else if (per.Contains("emp"))
            {
                There = There.And(p => p.CreatorEmpId == userInfo.EmpID);
            }

            var list = op.GetPagedList(There,PageModel).ToList();
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoSJCH") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.Number,
                                p.ProjID,
                                p.ProjNumber,
                                p.ProjName,
                                p.ProjPhaseId,
                                p.ProjPhaseName,
                                p.JoinSpecIds,
                                p.JoinSpecName,
                                p.JoinDepIds,
                                p.JoinDepName,
                                p.CooperativeDesign,
                                CooperativeDesignShow = GetBaseInfo(p.CooperativeDesign).BaseName,
                                p.MeritGoal,
                                MeritGoalShow= GetBaseInfo(p.MeritGoal).BaseName,
                                p.ProjNote,
                                p.PlanFinishTime,
                                p.IsReview,
                                p.ReviewTime,
                                p.ReviewType,
                                p.ProjResources,
                                p.Attachment,
                                p.PlanReport,
                                p.ChangePlanNote,
                                p.CreatorEmpId,
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new
                            {
                                m.Id,
                                m.Number,
                                m.ProjID,
                                m.ProjNumber,
                                m.ProjName,
                                m.ProjPhaseId,
                                m.ProjPhaseName,
                                m.JoinSpecIds,
                                m.JoinSpecName,
                                m.JoinDepIds,
                                m.JoinDepName,
                                m.CooperativeDesign,
                                m.CooperativeDesignShow,
                                m.MeritGoal,
                                m.MeritGoalShow,
                                m.ProjNote,
                                m.PlanFinishTime,
                                m.IsReview,
                                m.ReviewTime,
                                m.ReviewType,
                                m.ProjResources,
                                m.Attachment,
                                m.PlanReport,
                                m.ChangePlanNote,
                                m.CreatorEmpId,
                                m.FlowID,
                                m.FlowName,
                                m.FlowStatusID,
                                m.FlowStatusName,
                                m.FlowXml,
                                FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml)
                            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            var model = new IsoSJCH();

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            model.TableNumber = "SCX02-03";
            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);

            if (model != null)
            {
                ViewBag.CurrEmpID = model.CreatorEmpId;
            }
            else
            {
                ViewBag.CurrEmpID = userInfo.EmpID;
            }

            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;

            try
            {
                op.Delete(s => id.Contains(s.Id));
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoSJCH");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoSJCH");
                }

                reuslt = 1;
            }
            catch (Exception ex)
            {
                reuslt = 0;
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doError("操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new IsoSJCH();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 导出Word
        [Description("导出WORD")]
        [HttpPost]
        public void ExpDoc(int id)
        {
            string docName = "SJCH";
            string ExportName = "设计策划单";

            var model = op.Get(id);
            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.Clear();
            resp.ClearHeaders();
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

            string Url = System.Web.HttpContext.Current.Server.MapPath("~/") + @"Download\{0}.doc";
            Url = string.Format(Url, docName);
            if (!System.IO.File.Exists(Url))
            {
                resp.Flush();
                resp.End();
            }

            if (string.IsNullOrEmpty(ExportName)) ExportName = DateTime.Now.ToString("yyyyMMddHHmm");

            Aspose.Words.Document doc = new Aspose.Words.Document(Url);
            StringBuilder strTxt = new StringBuilder();
            foreach (Bookmark mark in doc.Range.Bookmarks)
            {
                switch (mark.Name)
                {
                    case "BH":
                        mark.Text = model.Number;
                        break;
                    case "GCBH":
                        mark.Text = model.ProjNumber;
                        break;
                    case "GCMC":
                        mark.Text = model.ProjName;
                        break;
                    case "SJJD":
                        List<string> ckjd = model.ProjPhaseId.Split(',').ToList();
                        mark.Text = stxt("ProjectPhase", ckjd);
                        break;
                    case "SJZY":
                        List<string> cksjzy = model.JoinSpecIds.Split(',').ToList();
                        mark.Text = stxt("Special", cksjzy);
                        break;
                    case "CYBM":
                        List<string> ckcybm = model.JoinSpecIds.Split(',').ToList();
                        mark.Text = stxt("department", ckcybm);
                        break;
                    case "HZSJ":
                        strTxt = new StringBuilder();
                        switch (model.CooperativeDesign)
                        {
                            case 1:
                                strTxt.Append("☑合作设计    □分包设计    □外聘人员设计");
                                break;
                            case 2:
                                strTxt.Append("□合作设计    ☑分包设计    □外聘人员设计");
                                break;
                            case 3:
                                strTxt.Append("□合作设计    □分包设计    ☑外聘人员设计");
                                break;
                            default:
                                strTxt.Append("□合作设计    □分包设计    □外聘人员设计");
                                break;
                        }
                        mark.Text = strTxt.ToString();
                        break;
                    case "CYMB":
                        strTxt = new StringBuilder();
                        switch (model.CooperativeDesign)
                        {
                            case 1:
                                strTxt.Append("☑不报优    □院优    □市优    □行优    □国优");
                                break;
                            case 2:
                                strTxt.Append("□不报优    ☑院优    □市优    □行优    □国优");
                                break;
                            case 3:
                                strTxt.Append("□不报优    □院优    ☑市优    □行优    □国优");
                                break;
                            case 4:
                                strTxt.Append("□不报优    □院优    □市优    ☑行优    □国优");
                                break;
                            case 5:
                                strTxt.Append("□不报优    □院优    □市优    □行优    ☑国优");
                                break;
                            default:
                                strTxt.Append("□不报优    □院优    □市优    □行优    □国优");
                                break;
                        }
                        mark.Text = strTxt.ToString();
                        break;
                    case "XMGK":
                        mark.Text = model.ProjNote;
                        break;
                    case "JHWC":
                        mark.Text = Common.Tools.FormatTime(model.PlanFinishTime, "yyyy-MM-dd", false);
                        break;
                    case "CHTZ":
                        mark.Text = model.ChangePlanNote;
                        break;
                    case "SJPS":
                        strTxt = new StringBuilder();
                        switch (model.IsReview)
                        {
                            case 1:
                                strTxt.AppendLine("☑要求评审    □不要求评审");
                                break;
                            case 2:
                                strTxt.AppendLine("□要求评审    ☑不要求评审");
                                break;
                            default:
                                strTxt.AppendLine("□要求评审    □不要求评审");
                                break;
                        }
                        strTxt.AppendLine("设计评审时机：" + Common.Tools.FormatTime(model.ReviewTime, "yyyy-MM-dd", false));
                        switch (model.ReviewType)
                        {
                            case 1:
                                strTxt.Append("设计评审方式：☑专业委员会会议    □其他会议");
                                break;
                            case 2:
                                strTxt.Append("设计评审方式：□专业委员会会议    ☑其他会议");
                                break;
                            default:
                                strTxt.Append("设计评审方式：□专业委员会会议    □其他会议");
                                break;
                        }
                        mark.Text = strTxt.ToString();
                        break;
                    case "XMSX":
                        strTxt = new StringBuilder();
                        List<string> xmsx = model.ProjResources.Split(',').ToList();
                        for (int i = 1; i <= 5; i++)
                        {
                            if (xmsx.Contains(i.ToString()))
                            {
                                switch (i)
                                {
                                    case 1:
                                        strTxt.Append("☑现场设计    ");
                                        break;
                                    case 2:
                                        strTxt.Append("☑BIM设计    ");
                                        break;
                                    case 3:
                                        strTxt.Append("☑特殊软件    ");
                                        break;
                                    case 4:
                                        strTxt.Append("☑现场安全防护用品    ");
                                        break;
                                    case 5:
                                        strTxt.Append("☑其他需求    ");
                                        break;
                                }
                            }
                            else
                            {
                                switch (i)
                                {
                                    case 1:
                                        strTxt.Append("□现场设计    ");
                                        break;
                                    case 2:
                                        strTxt.Append("□BIM设计    ");
                                        break;
                                    case 3:
                                        strTxt.Append("□特殊软件    ");
                                        break;
                                    case 4:
                                        strTxt.Append("□现场安全防护用品    ");
                                        break;
                                    case 5:
                                        strTxt.Append("□其他需求    ");
                                        break;
                                }
                            }
                        }
                        mark.Text = strTxt.ToString();
                        break;
                    case "FJ":
                        strTxt = new StringBuilder();
                        List<string> fj = model.Attachment.Split(',').ToList();
                        for (int i = 1; i <= 3; i++)
                        {
                            if (fj.Contains(i.ToString()))
                            {
                                switch (i)
                                {
                                    case 1:
                                        strTxt.Append("☑拟报出的文件册总目录    ");
                                        break;
                                    case 2:
                                        strTxt.Append("☑进度计划表    ");
                                        break;
                                    case 3:
                                        strTxt.Append("☑策划调整说明    ");
                                        break;
                                }
                            }
                            else
                            {
                                switch (i)
                                {
                                    case 1:
                                        strTxt.Append("□拟报出的文件册总目录    ");
                                        break;
                                    case 2:
                                        strTxt.Append("□进度计划表    ");
                                        break;
                                    case 3:
                                        strTxt.Append("□策划调整说明    ");
                                        break;
                                }
                            }
                        }
                        switch (model.PlanReport)
                        {
                            case 1:
                                strTxt.Append("是否需要编制设计策划报告？    ☑是    □否");
                                break;
                            case 2:
                                strTxt.Append("是否需要编制设计策划报告？    □是    ☑否");
                                break;
                            default:
                                strTxt.Append("是否需要编制设计策划报告？    □是    □否");
                                break;
                        }
                        mark.Text = strTxt.ToString();
                        break;

                }
            }

            doc.Save(resp, ExportName + ".doc", Aspose.Words.ContentDisposition.Attachment, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc));
        }

        private string stxt(string baseNameEng, List<string> ckData)
        {
            var baseData = new DBSql.Sys.BaseData().GetDataSetByEngName(baseNameEng).ToList();
            StringBuilder stxt = new StringBuilder();
            foreach (DataModel.Models.BaseData bd in baseData)
            {
                if (ckData.Contains(bd.BaseID.ToString()))
                {
                    stxt.Append("☑" + bd.BaseName + "    ");
                }
                else
                {
                    stxt.Append("□" + bd.BaseName + "    ");
                }
            }
            return stxt.ToString();
        }
        #endregion
    }
}
