using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Common.Data.Extenstions;
namespace Project.Controllers
{
    [Description("ProjCostModel")]
    public class ProjCostModelController : CoreController
    {
        private DBSql.Project.ProjCostModel op = new DBSql.Project.ProjCostModel();
        private DBSql.Sys.BaseData baseData = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private StringBuilder stb = new StringBuilder();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjCostConfig")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {

            stb = new StringBuilder("{\"rows\":[");
           // var _baseDataList = baseData.GetDataSetByEngName("ProjCostType").ToList();
            var TWhere = QueryBuild<DataModel.Models.ProjCostModel>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.ModelName.Contains(KeyJQSearch) );
            } 

            var projCostList = op.GetList(TWhere).ToList();
            int flag = 0;
            //foreach (var item in _baseDataList)
            //{
            //    if (flag != 0)
            //    {
            //        sb.Append(",");
            //    }
            //    flag++;
            //    sb.Append("{");
            //    sb.Append("\"ID\":" + item.BaseID + "");
            //    sb.Append(",\"ModelName\":\"" + item.BaseName + "\"");//成本类型
            //    sb.Append(",\"RowNoEdit\":-1");
            //    sb.Append(",\"RowNoDel\":-1");
            //    sb.Append("}");
            //}
            foreach (var item in projCostList)
            {
                if (flag != 0)
                {
                    stb.Append(",");
                }
                flag++;
                stb.Append("{");
                stb.Append("\"ID\":" + item.Id + "");
                stb.Append(",\"ModelName\":\"" + item.ModelName + "\"");//成本类型
                stb.Append(",\"ModelIsType\":" + item.ModelIsType + "");
                stb.Append(",\"ModelIsSum\":" + item.ModelIsSum + "");
                stb.Append(",\"ParentId\":" + item.ParentId + "");
                //if (item.ParentId == 0)
                //{
                //    sb.Append(",\"_parentId\":\"" + item.ModelType + "\"");
                //}
                //else
                //{
                //    sb.Append(",\"_parentId\":\"" + item.ParentId + "\"");
                //}
                if (item.ParentId!=0)
                {
                    stb.Append(",\"_parentId\":\"" + item.ParentId + "\"");
                }
                stb.Append("}");

                if (item.ParentId != 0)
                {
                    if (!string.IsNullOrEmpty(KeyJQSearch))
                    {
                        stb.Append(ParentStr(item.ParentId));
                    }
                }
            }
            stb.Append("]}");
            return stb.ToString();
        }

        public string ParentStr(int ParentId)
        {
            var sb = new StringBuilder("");
            ProjCostModel item = op.Get(ParentId);
            if (item != null)
            {
                sb.Append(",");

                sb.Append("{");
                sb.Append("\"ID\":" + item.Id + "");
                sb.Append(",\"ModelName\":\"" + item.ModelName + "\"");//成本类型
                sb.Append(",\"ModelIsType\":" + item.ModelIsType + "");
                sb.Append(",\"ModelIsSum\":" + item.ModelIsSum + "");
                sb.Append(",\"ParentId\":" + item.ParentId + "");
                if (item.ParentId != 0)
                {
                    sb.Append(",\"_parentId\":\"" + item.ParentId + "\"");
                }
                sb.Append("}");
               
                if (item.ParentId != 0)
                {
                    if (!stb.ToString().Contains("\"ID\":" + item.ParentId))
                    {
                        sb.Append(ParentStr(item.ParentId));
                    }
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new ProjCostModel();
            ViewBag.isAdd = 1;
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            DoResult dr = new DoResult();
            try
            {
                op.UnitOfWork.BeginTransaction();
                foreach (int item in id)
                {
                    if (HasChild(item))
                    {
                        DelChind(item);
                    }
                }
                op.Delete(s => id.Contains(s.Id));
                op.UnitOfWork.CommitTransaction();
                dr = DoResultHelper.doSuccess(0, "操作成功");
                return Json(dr);
            }
            catch (System.Exception)
            {
                op.UnitOfWork.RollBackTransaction();
                 dr = DoResultHelper.doSuccess(0, "操作失败");
                 return Json(dr);
            }
          
        }

        private void DelChind(int parentId)
        {
            var list=op.GetList(p => p.ParentId == parentId).ToList();
            foreach (var item in list)
            {
                if (HasChild(item.Id))
                {
                    DelChind(item.Id);
                }
            }
            foreach (var item in list)
            {
                op.Delete(item);
                
            }
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ProjCostModel();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
                if (HasChild(model.Id))
                {
                    ChangeChild(model);
                }
                SetOtherValueByEdit(model);
            }
            else
            {
                op.Add(model);
                SetOtherValueByAdd(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        private void ChangeChild(ProjCostModel model)
        {
            var list = op.GetList(p => p.ParentId == model.Id).ToList();
            if (list.FirstOrDefault().ModelType != model.ModelType)//成本类型修改
            {
                foreach (var item in list)
                {
                    item.ModelType = model.ModelType;
                    op.Edit(item);
                    if (HasChild(item.Id))
                    {
                        ChangeChild(item);
                    }
                }
            }
            if (model.ModelIsType == 0)
            {
                foreach (var item in list)
                {
                    item.ParentId = 0;
                    op.Edit(item);
                }
            }
        }

        private bool HasChild(int parentID)
        {
            bool b = false;
            var info = op.GetList(p => p.ParentId == parentID).FirstOrDefault();
            if (info != null)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 新增 其他赋值
        /// </summary>
        /// <param name="model"></param>
        private void SetOtherValueByAdd(ProjCostModel model)
        {
            model.CreationTime = System.DateTime.Now;//创建时间
            model.CreatorEmpId = userInfo.EmpID;//创建人ID
            model.CreatorEmpName = userInfo.EmpName;//创建人姓名
            model.LastModificationTime = System.DateTime.Now;//最后修改时间
            model.LastModifierEmpId = userInfo.EmpID;//最后修改人ID
            model.LastModifierEmpName = userInfo.EmpName;//最后修改人姓名

        }
        #endregion

        /// <summary>
        /// 修改 其他赋值
        /// </summary>
        /// <param name="model"></param>
        private void SetOtherValueByEdit(ProjCostModel model)
        {
            model.LastModificationTime = System.DateTime.Now;//最后修改时间
            model.LastModifierEmpId = userInfo.EmpID;//最后修改人ID
            model.LastModifierEmpName = userInfo.EmpName;//最后修改人姓名
        }

        /// <summary>
        /// 所有是类别的成本项json
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProjCostType(int ModelType=0, int id=0 )
        {
           // var modelType = Request.QueryString["ModelType"];
          //  var id = Request.QueryString["id"];

            bool isSubModel = false;
            List<object> list = new List<object>();
            List<int> listint = new List<int>();
            if (ModelType == 0)
            {

                list.Add(new { id = 0, text = "无父节点" });
            }
            else
            {
                int _modelType = int.Parse(ModelType.ToString());
                list.Add(new { id = 0, text = "无父节点" });
                var list_temp = op.GetList(p => p.ModelIsType == 1 && p.ModelType == _modelType).Select(p => new
                {
                    id = p.Id,
                   ParentId=  p.ParentId,
                    text = p.ModelName
                }).ToList();

                List<int> seft = GetSubmodel(id, ModelType, listint);
                seft.Add(id);
                foreach (var item in list_temp)
                {
                    if (id != 0)
                    {

                        //在修改的时候排除到自己和自己的下级
                        foreach (int modleid in seft)
                        {
                            // if (item.id.ToString().Equals(id) == false && item.ParentId.ToString().Equals(id) == false)
                            if (item.id == modleid)
                            {
                                isSubModel = true;
                                break;
                            }
                            else
                            {
                                isSubModel = false;
                            }
                        }

                        if (isSubModel == false)
                        {
                            list.Add(item);
                        }
                    }
                    else
                    {
                        list.Add(item);
                    }
                   
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        private List<int> GetSubmodel(int ParentID, int modelType, List<int> list)
        {
            // int _modelType = int.Parse(modelType.ToString());

            var list_temp = op.GetList(p => p.ModelIsType == 1 && p.ModelType == modelType && p.ParentId == ParentID).Select(p => new
                {
                    id = p.Id,
                   ParentId=  p.ParentId,
                    text = p.ModelName
                }).ToList();
           

            foreach (var item in list_temp)
            {
                if (item.ParentId == ParentID)
                {
                    list.Add(item.id);
                    GetSubmodel(item.id, modelType, list);
                }
            }


            return list;
        }

    }
}
