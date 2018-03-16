using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System.Data;
using System;
namespace Oa.Controllers
{
[Description("OaEquipReturn")]
public class OaEquipReturnController : CoreController
    {
        private DBSql.Oa.OaEquipReturn op = new DBSql.Oa.OaEquipReturn();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.EmpID = userInfo.EmpID;
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
                PageModel.SelectOrder = "os.Id desc";
            }

            List<string> permissionList = PermissionList("OaEquipReturn");

            if (!permissionList.Contains("allview"))
            {
                if (permissionList.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("CreatorDepId", userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("CreateEmpId", userInfo.EmpID);
                }
            }
            PageModel.TextCondtion = Request.Params["Keys"];

            //if (condition.Filter != "")
            //{
            //    PageModel.TextCondtion = condition.Filter;
            //}

            //var list = op.GetPagedList(Thwere, PageModel).ToList();

            //var showList = (from p in list
            //                join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaEquipReturn") on p.Id equals t1.FlowRefID into nodes
            //                from temp in nodes.DefaultIfEmpty()
            //                select new
            //                {
            //                    Id = p.Id,
            //                    EquipBackDate = p.EquipBackDate,
            //                    CreatorEmpName = p.CreatorEmpName,
            //                    p.CreatorEmpId,
            //                    EquipLendNote = p.EquipLendNote,
            //                    FlowID = temp == null ? 0 : temp.Id,
            //                    FlowName = temp == null ? "" : temp.FlowName,
            //                    FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
            //                    FlowStatusName = temp == null ? "" : temp.FlowStatusName,
            //                    FlowXml = temp == null ? "" : temp.FlowXml
            //                }).Select(m => new { m.Id, m.EquipBackDate, m.CreatorEmpName, m.CreatorEmpId, m.EquipLendNote, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

            string EquipOrOffice = Request["EquipOrOffice"];
            DataTable dt = new DBSql.Oa.OaEquipReturn().json(PageModel, EquipOrOffice);
            var showList = (from p in dt.AsEnumerable() 
                            //    into nodes
                            //from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                Id = p["EquipFormID"],
                                //EquipBackDate = p["EquipBackDate"],
                                CreatorEmpName = p["CreatorEmpName"],
                                EquipLendNote = p["EquipLendNote"],
                                EquipName = p["EquipName"],
                                EquipModel = p["EquipModel"],
                                EquipCount = p["EquipCount"],
                                CreatorEmpId = p["CreatorEmpId1"] == null?"":p["CreatorEmpId1"].ToString(),
                                FlowID = p["Idd"] == null ? "" : p["Idd"].ToString(),
                                FlowName = p["FlowName"] == null ? "" : p["FlowName"].ToString(),
                                FlowStatusID = p["FlowStatusID"] == null ? "" : p["FlowStatusID"].ToString(),
                                FlowStatusName = p["FlowStatusName"] == null ? "" : p["FlowStatusName"].ToString(),
                                FlowXml = p["FlowXml"] == null ? "" : p["FlowXml"].ToString()
                            });

           

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList
            });
        } 
        #endregion

        #region 选择json
        public string choosejson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var Thwere = QueryBuild<DataModel.Models.OaEquipUse>.True();

            List<string> permissionList = PermissionList("EquipmentBorrow");

            if (!permissionList.Contains("dep") && !permissionList.Contains("allview"))
            {
                Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
            }
            else
            {
                if (!permissionList.Contains("allview") && permissionList.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);//部门查看权
                }
            }

            string EquIds = "";
            if (GetRequestValue<string>("EquIds") != null)
            {
                EquIds = GetRequestValue<string>("EquIds").Trim(',');
            }

            //List<string> EquId = new List<string>(EquIds.Split(','));

            int EquipOrOffice = GetRequestValue<int>("EquipOrOffice");
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);
            Thwere = Thwere.And(p => p.EquipOrOffice == EquipOrOffice);
            //Thwere = Thwere.And(p => !EquId.Contains(p.Id.ToString()));

            var list = new DBSql.Oa.OaEquipUse().GetPagedList(Thwere, PageModel).ToList();

            //DataTable ss = new DBSql.Oa.OaEquipReturn().GetUseEquip(GetRequestValue<int>("EquIds"));

            var xx = (from a in list
                      join temp in op.DbContext.Set<DataModel.Models.OaEquipStock>().Where(f => f.EquipActionID == 2) on a.Id equals temp.EquipFormID
                      select new
                      {
                          Id = a.Id,
                          EquipLendDate = a.EquipLendDate.ToString("yyyy/MM/dd HH:mm:ss"),
                          EquipLendNote = a.EquipLendNote,
                          EquipBackDate = a.EquipBackDate.ToString("yyyy/MM/dd HH:mm:ss"),
                      }).Distinct().ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = xx.Count,
                rows = xx
            });
        }
        #endregion
        #region 刚进页面时刷新的领用单
        public string choosejsonId()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var Thwere = QueryBuild<DataModel.Models.OaEquipUse>.True();


            //List<string> EquId = new List<string>(EquIds.Split(','));

            string EquipOrOffice = Request["EquipOrOffice"] == "" ? "0" : Request["EquipOrOffice"];
            int Id = int.Parse(GetRequestValue<string>("id"));
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);
            if (Request["EquipOrOffice"] != "")
            {
                int e = int.Parse(EquipOrOffice);
                Thwere = Thwere.And(p => p.EquipOrOffice == e);
            }
            Thwere = Thwere.And(p => p.Id == Id);
            //if(Id != "0")
                //Thwere = Thwere.And(p => p.Id == Id);
            //Thwere = Thwere.And(p => !EquId.Contains(p.Id.ToString()));

            var list = new DBSql.Oa.OaEquipUse().GetPagedList(Thwere, PageModel).ToList();

            //DataTable ss = new DBSql.Oa.OaEquipReturn().GetUseEquip(GetRequestValue<int>("EquIds"));

            var xx = (from a in list
                      join temp in op.DbContext.Set<DataModel.Models.OaEquipStock>().Where(f => f.EquipActionID == 2) on a.Id equals temp.EquipFormID
                      select new
                      {
                          Id = a.Id,
                          EquipLendDate = a.EquipLendDate.ToString("yyyy/MM/dd HH:mm:ss"),
                          EquipLendNote = a.EquipLendNote.ToString(),
                          EquipBackDate = a.EquipBackDate.ToString("yyyy/MM/dd HH:mm:ss"),
                      }).Distinct().ToList();
        
            return JavaScriptSerializerUtil.getJson(new
            {
                total = xx.Count,
                rows = xx
            });
        }
        #endregion

        #region 选择json
        public string choosejson1()
        {
            DataTable ss = new DBSql.Oa.OaEquipReturn().GetUseRecord(GetRequestValue<int>("id"));

            var xx = (from a in ss.AsEnumerable()
                      select new
                      {
                          Id = a["Id"],
                          EquipId = a["EquipId"],
                          EquipNumber = a["EquipNumber"].ToString(),
                          EquipName = a["EquipName"].ToString(),
                          EquipModel = a["EquipModel"].ToString(),
                          Count = a["Count"].ToString(),
                      }).Distinct().ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = xx.Count,
                rows = xx
            });
        }
        #endregion

        #region 选择json
        public string choosejson2()
        {
            DataTable ss = new DBSql.Oa.OaEquipReturn().GetUseRecord1(GetRequestValue<int>("id"), GetRequestValue<int>("rid"),GetRequestValue<DateTime>("time"));

            var xx = (from a in ss.AsEnumerable()
                      select new
                      {
                          Id = a["Id"],
                          EquipId = a["EquipId"],
                          EquipNumber = a["EquipNumber"].ToString(),
                          EquipName = a["EquipName"].ToString(),
                          EquipModel = a["EquipModel"].ToString(),
                          Count = int.Parse(a["Count"].ToString()),// + int.Parse(a["EquipCount"].ToString()),
                          EquipCount = a["eCount"].ToString(),
                          EquipCount1 = a["eCount"].ToString(),
                      }).Distinct().ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = xx.Count,
                rows = xx
            });
        }
        #endregion

        #region 选择领用单
        public ActionResult EquipmentChoose()
        {
            return View();
        }
        #endregion


        #region 选择领用单
        public ActionResult EquipmentChoose1()
        {
            return View();
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipReturn();
            ViewBag.EquipBackDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            //int id1 = int.Parse(new DBSql.Oa.OaEquipReturn().GetRID(id));
            var model = op.Get(id);
            ViewBag.EquipBackDate = model.EquipBackDate.ToString("yyyy-MM-dd");
            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));

            try
            {
                var flowIds = new DBSql.PubFlow.Flow().GetList(p => p.Id > 0).Where(p => p.FlowRefTable == "OaEquipReturn" && id.Contains(p.FlowRefID)).Select(p => p.Id);
                var flowNodeExe = new DBSql.PubFlow.FlowNodeExe();
                flowNodeExe.Delete(p => flowIds.Contains(p.FlowID));
                flowNodeExe.UnitOfWork.SaveChanges();
                var flowNode = new DBSql.PubFlow.FlowNode();
                flowNode.Delete(p => flowIds.Contains(p.FlowID));
                flowNode.UnitOfWork.SaveChanges();
                var flow = new DBSql.PubFlow.Flow();
                flow.Delete(p => flowIds.Contains(p.Id));
                flow.UnitOfWork.SaveChanges();

                op.UnitOfWork.SaveChanges();
                reuslt = 1;
            }
            catch { }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 查询设备数量
        public string QueryNumber(string Id,string Rid)
        {
            return "{\"result\":\"" + new DBSql.Oa.OaEquipReturn().GetNumber(int.Parse(Id), int.Parse(Rid)) + "\"}";
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaEquipReturn();
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
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
