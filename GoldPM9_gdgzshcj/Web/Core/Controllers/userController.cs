using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Core.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Description("用户管理")]
    public class userController : CoreController
    {
        private string IDs = string.Empty;
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DoResult doResult = DoResultHelper.doSuccess("");
        DBSql.Sys.LinkRTX linkRTX = new DBSql.Sys.LinkRTX();
        #region 用户列表
        [Description("用户列表")]
        public ActionResult list()
        {
            ViewBag.deptID = userInfo.EmpDepID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("UserAdmin")));
            return View();
        }
        [Description("用户绑定设置列表")]
        public ActionResult empmarklist()
        {
            ViewBag.deptID = userInfo.EmpDepID;
            return View();
        }

        [Description("用户通讯录")]
        public ActionResult empcontact()
        {
            ViewBag.deptID = userInfo.EmpDepID;
            return View();
        }
        #endregion

        #region 用户列表json
        [Description("用户列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "DepartmentOrder asc,EmpOrder asc";
            }
            var list = new DBSql.Sys.AllBaseEmployee().GetPagedDynamic(PageModel).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 添加用户
        [Description("添加用户")]
        public ActionResult add()
        {
            var model = new BaseEmployee();
            ViewBag.IDs = "";
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑用户
        [Description("编辑用户")]
        public ActionResult edit(int id)
        {
            var model = emp.Get(id);
            if (model.FK_BaseEmpPermission_PermissionEmpID.isNotEmpty())
            {
                IDs = string.Join(",", model.FK_BaseEmpPermission_PermissionEmpID.ToList().Select(m => m.PermissionEmpValue));
            }
            ViewBag.IDs = !string.IsNullOrEmpty(IDs) ? IDs : "";
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 手机端编辑用户
        [Description("手机端编辑用户")]
        public String editInfo(int id)
        {
            var model = emp.Get(id);
            DataModel.Models.BaseEmployee mdlEvent = new DataModel.Models.BaseEmployee();
            Common.ModelConvert.MvcChangeTarget<DataModel.Models.BaseEmployee, DataModel.Models.BaseEmployee>(mdlEvent, model);
            ViewBag.model = mdlEvent;
            if (model.FK_BaseEmpPermission_PermissionEmpID.isNotEmpty())
            {
                IDs = string.Join(",", model.FK_BaseEmpPermission_PermissionEmpID.ToList().Select(m => m.PermissionEmpValue));
            }
            ViewBag.IDs = !string.IsNullOrEmpty(IDs) ? IDs : "";
            ViewBag.permission = "['add','submit']";
            ViewBag.EmpHead = model.EmpHead;
            return JavaScriptSerializerUtil.getJson(ViewBag);
        }
        #endregion

        #region 保存用户
        [Description("保存用户")]
        [HttpPost]
        public ActionResult save(int EmpID, int[] PermissionEmpValue = null)
        {
            var model = new BaseEmployee();
            if (EmpID > 0)
            {
                model = emp.Get(EmpID);
            }
            model.MvcSetValue();
            doResult = model.isValid(true, Others);
            if (doResult.stateType == DoResultType.validFail)
            {
                return Json(doResult);
            }
            int reuslt = 0;
            var list = new List<BaseEmpPermission>();
            if (PermissionEmpValue.isNotEmpty())
            {
                foreach (var item in PermissionEmpValue)
                {
                    list.Add(new BaseEmpPermission()
                    {
                        PermissionEmpID = model.EmpID,
                        PermissionEmpValue = item,
                        PermissionEmpNote = ""
                    });
                }
            }
            if (model.EmpDepID > 0)
            {
                model.EmpDepName = GetBaseInfo(model.EmpDepID).BaseName;
            }
            if (model.EmpID > 0)
            {
                reuslt = emp.UpdateBaseEmployeeInfo(model, list);
            }
            else
            {
                model.EmpPassword = EncryInfo.EncrypPassword("pass");
                reuslt = emp.InsertBaseEmployeeInfo(model, list);
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.EmpID, "操作成功") : DoResultHelper.doSuccess(model.EmpID, "操作失败");
            return Json(dr);
        }

        [Description("保存其它信息")]
        [HttpPost]
        public ActionResult saveinfo(List<BaseEmployee> dto)
        {
            int reuslt = 0;
            if (dto != null)
            {
                foreach (var item in dto)
                {
                    emp.Edit(s => s.EmpID == item.EmpID, u => new BaseEmployee { EmpIPAddress = item.EmpIPAddress ?? "", EmpMacAddress = item.EmpMacAddress ?? "", EmpIsBind = item.EmpIsBind, EmpNote = item.EmpNote ?? "" });
                }
                emp.DbContext.SaveChanges();
                reuslt = 1;
                emp.CacheRemove();
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess("操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);
        }

        [Description("保存其它信息")]
        [HttpPost]
        public ActionResult UpdateEmpOrder(int EmpID, int OrderType)
        {
            int reuslt = 0;
            if (EmpID != 0 && OrderType != 0)
            {
                emp.UpdateEmpOrder(EmpID, OrderType);
                reuslt = 1;
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess("操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);
        }

        #endregion

        #region 禁用用户

        [Description("禁用用户")]
        [HttpPost]
        public ActionResult del(int EmpID, int isDel)
        {
            var model = emp.Get(EmpID);
            model.EmpIsDeleted = isDel == 1 ? true : false;
            int reuslt = emp.UpdateBaseEmployeeInfo(model, null);

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);
        }

        [Description("恢复密码")]
        [HttpPost]
        public ActionResult DefaultPass(int EmpID)
        {
            var model = emp.Get(EmpID);

            model.EmpPassword = EncryInfo.EncrypPassword("pass");
            int reuslt = emp.UpdateBaseEmployeeInfo(model, null);

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);
        }

        [Description("切换用户")]
        [HttpPost]
        public ActionResult ToEmp(int EmpID)
        {
            var model = emp.Get(EmpID);

            SaveUserInfo(model);

            DoResult dr = new DoResult();
            dr.url = Url.Content("~");

            return Json(dr);
        }


        [Description("移动用户签名")]
        [HttpPost]
        public ActionResult signMove()
        {
            string UpFileRoot = Server.MapPath("~/SignImages");
            string path = ConfigurationManager.AppSettings["FtpLocalPath"].ToString() + "\\Emp" + userInfo.EmpID.ToString();

            var FileList = Directory.GetFiles(path).ToList();
            foreach (var file in FileList)
            {
                string FileName = file.Substring(file.LastIndexOf("\\") + 1);
                try
                {
                    string fullname = UpFileRoot + "\\" + FileName;
                    if (file.EndsWith(".png"))
                    {
                        if (System.IO.File.Exists(fullname)) System.IO.File.Delete(fullname);
                        System.IO.File.Move(file, fullname);
                    }
                    else
                    {
                        System.IO.File.Delete(file);
                    }
                }
                catch { }
            }
            return Json(DoResultHelper.doSuccess("操作成功"));
        }




        #region 登录成功后，保存用户信息到Session中;写入登录日志；判断代理信息。

        public void SaveUserInfo(DataModel.Models.BaseEmployee emp)
        {
            int EmpID = userInfo.EmpID;
            string EmpName = userInfo.EmpName;
            if (Session["Employee"] != null) Session.Remove("Employee");
            if (emp == null) return;
            new DBSql.Sys.BaseEmployee().SaveSessionInfo(Session, emp);
            CookieUtil.saveCookie("UID", emp.EmpID.ToString(), 24);
            CookieUtil.clear("AgentID");
            InsertLog(EmpID, EmpName,emp);
        }

        private void InsertLog(int EmpID,string EmpName, BaseEmployee emp)
        {
            DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
            log.BaseLogEmpID = EmpID;
            log.EmpName = EmpName;
            log.BaseLogTypeID = 0;
            log.BaseLogDate = DateTime.Now;
            log.BaseLogIP = GetIP();
            log.BaseLogRefTable = "BaseEmployee";
            log.BaseLogRefID = emp.EmpID;
            log.BaseLogRefHTML = "切换用户:" + emp.EmpName ;

            DBSql.Sys.BaseLog logAdd = new DBSql.Sys.BaseLog();
            logAdd.Add(log);
            logAdd.UnitOfWork.SaveChanges();
        }
        #endregion
        #endregion

        #region 扩展验证
        /// <summary>
        /// 扩展验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<ValidationResult> Others(BaseEmployee model)
        {
            int count = emp.GetCountByUserName(model.EmpLogin);
            int size = model.EmpID > 0 ? 1 : 0;
            List<ValidationResult> vrs = new List<ValidationResult>();
            if (model.EmpDepID <= 0)
                vrs.Add(DataAnnotationHelper.validResult("请选择用户所属部门", "EmpDepID"));

            if (emp.GetCountByUserName(model.EmpLogin) > size)
            {
                vrs.Add(DataAnnotationHelper.validResult("用户名已经被使用", "EmpLogin"));
            }
            return vrs;
        }
        #endregion

        #region  根据部门获取下拉人员

        public string getJsonByDeptID()
        {
            int DeptID = TypeHelper.parseInt(Request.QueryString["DeptId"]);
            var list = emp.GetList(p => p.EmpDepID == DeptID).ToList();
            if (DeptID == 0)
            {
                list = emp.GetList(p => p.EmpDepID != 0).ToList();
            }

            var targetList = from item in list
                             orderby item.EmpOrder
                             select new
                             {
                                 item.EmpID,
                                 item.EmpName
                             };
            return JavaScriptSerializerUtil.getJson(targetList.ToList());

        }
        #endregion

        #region 同步部门人员RTX
        public string LinkSynchDeptEmp()
        {
            try
            {
                linkRTX.LinkSynchDeptEmp();
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception)
            {
                doResult = DoResultHelper.doError("操作失败");
            }
            return JavaScriptSerializerUtil.getJson(doResult);
        }
        #endregion

        #region
        public ActionResult SetAvatar()
        {
            return View();
        }

        public ActionResult ProfileSetting()
        {
            return View(emp.Get(userInfo.EmpID));
        }

        public JsonResult ChangeAvatar()
        {
            //var path = Path.Combine(JQ.Util.IOUtil.GetTempPath(), Request.Form["fileName"]);
            //if (!System.IO.File.Exists(path))
            //{
            //    return Json(new { Result = false, Message = "无法找到图片文件" });
            //}
            //var x1 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["x1"]), 0).ToString());
            //var x2 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["x2"]), 0).ToString());
            //var y1 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["y1"]), 0).ToString());
            //var y2 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["y2"]), 0).ToString());
            //using (Bitmap fromImage = new Bitmap(Image.FromFile(path), int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["imgWidth"]), 0).ToString()), int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["imgHeight"]), 0).ToString())))
            //{
            //    using (Bitmap bitmap = new Bitmap(x2 - x1, y2 - y1))
            //    {
            //        Graphics graphic = Graphics.FromImage(bitmap);
            //        graphic.DrawImage(fromImage, 0, 0, new Rectangle(x1, y1, x2 - x1, y2 - y1), GraphicsUnit.Pixel);
            //        //获取出存放路径
            //        var savePath = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar");
            //        if (!Directory.Exists(savePath))
            //        {
            //            Directory.CreateDirectory(savePath);
            //        }
            //        bitmap.Save(Path.Combine(savePath, userInfo.EmpID + ".jpg"), ImageFormat.Jpeg);
            //    }
            //}
            //JQ.Util.IOUtil.TryDeleteFile(path);
            //return Json(new { Result = true });
            var path = Path.Combine(JQ.Util.IOUtil.GetTempPath(), Request.Form["fileName"]);
            if (!System.IO.File.Exists(path))
            {
                return Json(new { Result = false, Message = "无法找到图片文件" });
            }
            var x1 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["x1"]), 0).ToString());
            var x2 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["x2"]), 0).ToString());
            var y1 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["y1"]), 0).ToString());
            var y2 = int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["y2"]), 0).ToString());
            using (Bitmap fromImage = new Bitmap(Image.FromFile(path), int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["imgWidth"]), 0).ToString()), int.Parse(Math.Round(JQ.Util.TypeParse.parse<double>(Request.Form["imgHeight"]), 0).ToString())))
            {
                using (Bitmap bitmap = new Bitmap(x2 - x1, y2 - y1))
                {
                    Graphics graphic = Graphics.FromImage(bitmap);
                    graphic.DrawImage(fromImage, 0, 0, new Rectangle(x1, y1, x2 - x1, y2 - y1), GraphicsUnit.Pixel);
                    //获取出存放路径
                    var savePath = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar");
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    bitmap.Save(Path.Combine(savePath, userInfo.EmpID + ".jpg"), ImageFormat.Jpeg);
                    //插入数据库
                    try
                    {
                        DataModel.Models.BaseEmployee dmModel = emp.Get(userInfo.EmpID);
                        if (dmModel != null)
                        {
                            dmModel.EmpHead = Guid.NewGuid().ToString("N");
                            emp.Edit(dmModel);
                            emp.DbContext.SaveChanges();
                            emp.CacheRemove();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            JQ.Util.IOUtil.TryDeleteFile(path);
            return Json(new { Result = true });
        }

        public JsonResult ChangeAvatar1()
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { Result = false });
            }
            try
            {
                var savePath = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                Request.Files[0].SaveAs(Path.Combine(savePath, userInfo.EmpID + ".jpg"));
                DataModel.Models.BaseEmployee dmModel = emp.FirstOrDefault(p => p.EmpID == userInfo.EmpID);
                if (dmModel != null)
                {
                    dmModel.EmpHead = Guid.NewGuid().ToString("N");
                    emp.DbContext.Entry(dmModel).State = System.Data.Entity.EntityState.Modified;
                    emp.DbContext.SaveChanges();
                    emp.CacheRemove();
                }
                return Json(new { Result = true });
            }
            catch
            {
                return Json(new { Result = false });
            }
        }

        public FileResult GetAvatar()
        {
            var empID = Request.QueryString["id"];
            if (string.IsNullOrEmpty(empID))
            {
                return null;
            }
            var path = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar", empID + ".jpg");
            if (!System.IO.File.Exists(path))
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            return File(path, "image/jpeg");
        }

        public FileResult GetHeadUrl(string gid)
        {
            if (string.IsNullOrEmpty(gid))
            {
                return null;
            }
            var model = emp.GetListFromCache(p => p.EmpHead == gid).FirstOrDefault();
            if (model == null)
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            var path = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar", model.EmpID + ".jpg");
            if (!System.IO.File.Exists(path))
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            return File(path, "image/jpeg");

        }

        public JsonResult SaveProfileSetting()
        {
            DataModel.Models.BaseEmployee data = new BaseEmployee();
            data.EmpID = userInfo.EmpID;
            data.MvcSetValue();
            new DBSql.Sys.BaseEmployee().SaveProfileSetting(data);
            new DBSql.Sys.BaseEmployee().SaveSessionInfo(Session, emp.Get(data.EmpID));
            return Json(DoResultHelper.doSuccess(data.EmpID, "操作成功"));
        }

        #endregion
    }
}
