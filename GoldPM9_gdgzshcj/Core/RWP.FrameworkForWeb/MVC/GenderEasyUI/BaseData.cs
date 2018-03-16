using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.IO;
using JQ.Util;

namespace JQ.Web
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public class BaseData
    {
        #region 生成基础数据控件一般用于添加编辑页面便于赋值
        /// <summary>
        /// 生成基础数据控件一般用于添加编辑页面便于赋值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static MvcHtmlString getBase(Params item)
        {
            StringBuilder sb = new StringBuilder();
            var model = new DBSql.Sys.BaseData().GetBaseDataInfoByEngName(item.engName.ToString());
            if (model == null)
            {
                return MvcHtmlString.Create(string.Format("未找到此{0}分类", item.engName));
            }
            string id = string.IsNullOrEmpty(item.controlName) ? item.engName.ToString() : item.controlName;
            string url = Path.Combine("/" + HttpContext.Current.Request.Url.Segments[1], "Core/basedata/treejson?engName=" + item.engName);

            if (item.typeId != null)
            {
                if (item.typeName != "")
                {
                    url += "&typeName=" + item.typeName;
                }
                if (item.typeAtt != "")
                {
                    url += "&typeAtt=" + item.typeAtt;
                }
                if (!string.IsNullOrEmpty(item.typeId))
                {
                    url += "&typeId=" + item.typeId;
                }
            }

            if (!string.IsNullOrEmpty(item.engName.ToString()))
            {
                sb.Append(string.Format("<select id=\"{0}\" name=\"{0}\"  style=\"width:{1};height:28px;\"></select>&nbsp;&nbsp;"
                , id
                , !string.IsNullOrEmpty(item.width) ? item.width : "200px"
                ));
                sb.Append("<script type=\"text/javascript\">"
                                    + "$(function(){"
                                             + "$('#" + id + "').combotree({"
                                                     + "animate:true,"
                                                     + "cascadeCheck: false,"
                                                     + "multiple:" + item.isMult.ToString().ToLower() + ","
                                                     + "required:" + item.isRequired.ToString().ToLower() + ","
                                                     + "url: '" + url + "',"
                                                     + "prompt:'请选择" + model.BaseName + "',"
                                                     + "onlyLeafCheck:true,"
                                                     + (item.queryOptions.isNotEmpty() ? "queryOptions:" + item.queryOptions + "," : "")
                                                     + "onLoadSuccess:function(row, data) {"
                                                        + (item.ids.isNotEmpty() && item.ids != "0" ? "$('#" + id + "').combotree('setValues', [" + item.ids + "])" : "")
                                                     + "},"
                                                     + "onSelect:function(node){ "
                                                     + "if('" + item.filterProjEmpId.ToString().ToLower() + "'== 'true') {  SetProjEmpId(node); }"
                                                     + "if('" + item.isVerify.ToString().ToLower() + "'== 'true') {  if(node.BaseAtt2=='1'){  JQ.dialog.warning('请选择子项!!!');   $('#" + id + "').combotree('clear');} }"
                                                     + "}"
                                             + "})"
                                        + "})"
                          + "</script>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString getBaseSelect(Params item)
        {
            StringBuilder sb = new StringBuilder();
            var model = new DBSql.Sys.BaseData().GetBaseDataInfoByEngName(item.engName.ToString());
            if (model == null)
            {
                return MvcHtmlString.Create(string.Format("未找到此{0}分类", item.engName));
            }
            string id = string.IsNullOrEmpty(item.controlName) ? item.engName.ToString() : item.controlName;

            if (!string.IsNullOrEmpty(item.engName.ToString()))
            {
                sb.Append(string.Format("<select id=\"{0}\" name=\"{0}\" class=\"aui-input\" style =\"width:{1}\" value=\"178\" {2} {3}>"
                , id
                , !string.IsNullOrEmpty(item.width) ? item.width : "200px"
                , item.isMult ? String.Format("multiple=\"{1}\"", item.isMult.ToString().ToLower()) : ""
                , "prompt=\"请选择" + model.BaseName + "\""
                ));
                var bd = new DBSql.Sys.BaseData();
                var baseData = bd.GetDataSetByEngName(model.BaseNameEng);
                foreach (var dt in baseData)
                {
                    sb.Append(String.Format("<option value=\"{0}\">{1}</option>", dt.BaseID, dt.BaseName));
                }
                sb.Append("</select>");

            }
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 生成BaseDataSystem表基础数据控件一般用于添加编辑页面便于赋值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static MvcHtmlString getBaseSystem(Params item)
        {
            StringBuilder sb = new StringBuilder();
            var model = new DBSql.Sys.BaseDataSystem().GetBaseDataInfoByEngName(item.engName.ToString());
            if (model == null)
            {
                return MvcHtmlString.Create(string.Format("未找到此{0}分类", item.engName));
            }
            string id = string.IsNullOrEmpty(item.controlName) ? item.engName.ToString() : item.controlName;
            string url = Path.Combine("/" + HttpContext.Current.Request.Url.Segments[1], "Core/BaseDataSystem/treejson?engName=" + item.engName);

            if (!string.IsNullOrEmpty(item.engName.ToString()))
            {
                sb.Append(string.Format("<select JQWhereOptions=\"{2}\" id=\"{0}\" name=\"{0}\"  style=\"width:{1};height:28px;\"></select>&nbsp;&nbsp;"
                , id
                , !string.IsNullOrEmpty(item.width) ? item.width : "200px"
                , item.queryOptions
                ));
                sb.Append("<script type=\"text/javascript\">"
                                    + "$(function(){"
                                             + "$('#" + id + "').combotree({"
                                                     + "animate:true,"
                                                     + "cascadeCheck: false,"
                                                     + "multiple:" + item.isMult.ToString().ToLower() + ","
                                                     + "required:" + item.isRequired.ToString().ToLower() + ","
                                                     + "url: '" + url + "',"
                                                     + "prompt:'请选择" + model.BaseName + "',"
                                                     + "onlyLeafCheck:true,"
                                                     + (item.queryOptions.isNotEmpty() ? "queryOptions:" + item.queryOptions + "," : "")
                                                     + "onLoadSuccess:function(row, data) {"
                                                        + (item.ids.isNotEmpty() && item.ids != "0" ? "$('#" + id + "').combotree('setValues', [" + item.ids + "])" : "")
                                                     + "}"
                                             + "})"
                                        + "})"
                          + "</script>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion


        #region 生成基础数据控件一般用于列表搜索无法赋值
        /// <summary>
        /// 生成基础数据控件一般用于列表搜索无法赋值
        /// </summary>
        /// <param name="btnParams"></param> 
        /// <returns></returns>
        public static MvcHtmlString getBases(params Params[] btnParams)
        {
            StringBuilder sb = new StringBuilder();
            if (btnParams.isNotEmpty())
            {
                foreach (var item in btnParams)
                {
                    var model = new DBSql.Sys.BaseData().GetBaseDataInfoByEngName(item.engName.ToString());
                    string id = string.IsNullOrEmpty(item.controlName) ? item.engName.ToString() : item.controlName;
                    string url = Path.Combine("/" + HttpContext.Current.Request.Url.Segments[1], "Core/basedata/treejson?engName=" + item.engName);

                    //if (item.typeId != null)
                    //{
                    //    if (item.typeName != "")
                    //    {
                    //        url += "&typeName=" + item.typeName;
                    //    }
                    //    if (item.typeAtt != "")
                    //    {
                    //        url += "&typeAtt=" + item.typeAtt;
                    //    }
                    //    if (item.typeId == "1")
                    //    {
                    //        url += "&typeId=" + item.typeId;
                    //    }
                    //}

                    if (item.typeId != null)
                    {
                        if (item.typeName != "")
                        {
                            url += "&typeName=" + item.typeName;
                        }
                        if (item.typeAtt != "")
                        {
                            url += "&typeAtt=" + item.typeAtt;
                        }
                        if (item.typeId == "1")
                        {
                            url += "&typeId=" + item.typeId;
                        }
                    }

                    if (string.IsNullOrEmpty(item.engName.ToString()))
                        continue;
                    sb.Append(string.Format("<select JQWhereOptions=\"{4}\" class='easyui-combotree' id=\"{0}\" name=\"{0}\"  style=\"width:{1};height:28px;\" data-options=\"onlyLeafCheck:true,cascadeCheck: false,multiple:{2},url:'{3}',prompt:'{5}',animate:true\"></select>&nbsp;&nbsp;"
                        , id
                        , !string.IsNullOrEmpty(item.width) ? item.width : "200px"
                        , item.isMult.ToString().ToLower()
                        , url
                        , item.queryOptions
                        , "请选择" + model.BaseName
                        ));
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion


        #region 生成基础数据控件一般用于列表搜索无法赋值
        /// <summary>
        /// 生成基础数据控件一般用于列表搜索无法赋值
        /// </summary>
        /// <param name="btnParams"></param>
        /// <returns></returns>
        public static MvcHtmlString getBaseDataSystem(params Params[] btnParams)
        {
            StringBuilder sb = new StringBuilder();
            if (btnParams.isNotEmpty())
            {
                foreach (var item in btnParams)
                {
                    var model = new DBSql.Sys.BaseDataSystem().GetBaseDataInfoByEngName(item.engName.ToString());
                    string id = string.IsNullOrEmpty(item.controlName) ? item.engName.ToString() : item.controlName;
                    string url = Path.Combine("/" + HttpContext.Current.Request.Url.Segments[1], "Core/BaseDataSystem/treejson?engName=" + item.engName);
                    if (item.isfilter == 1)
                    {
                        url += "&isfilter=" + item.isfilter;
                    }
                    if (string.IsNullOrEmpty(item.engName.ToString()))
                        continue;
                    sb.Append(string.Format("<select JQWhereOptions=\"{4}\" class='easyui-combotree' id=\"{0}\" name=\"{0}\"  style=\"width:{1};height:28px;\" data-options=\"onlyLeafCheck:true,cascadeCheck: false,multiple:{2},url:'{3}',prompt:'{5}',animate:true\"></select>&nbsp;&nbsp;"
                        , id
                        , !string.IsNullOrEmpty(item.width) ? item.width : "200px"
                        , item.isMult.ToString().ToLower()
                        , url
                        , item.queryOptions
                        , "请选择" + model.BaseName
                        ));
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion
    }


    //#region 参数
    ///// <summary>
    ///// 按钮参数
    ///// </summary>
    //public class Params
    //{
    //    /// <summary>
    //    /// 数据库英文标题
    //    /// </summary>
    //    public string engName { get; set; }

    //    /// <summary>
    //    /// 生成控件的Name和ID
    //    /// </summary>
    //    public string controlName { get; set; }

    //    /// <summary>
    //    /// 查询参数字符串
    //    /// </summary>
    //    public string queryOptions { get; set; }

    //    /// <summary>
    //    /// 需要选中的IDS
    //    /// </summary>
    //    public string ids { get; set; }

    //    /// <summary>
    //    /// 是否多选 
    //    /// </summary>
    //    public bool isMult { get; set; }


    //    private bool _isSel = true;
    //    /// <summary>
    //    /// 是否显示提示文字
    //    /// </summary>
    //    public bool isSel
    //    {
    //        get { return _isSel; }
    //        set { _isSel = value; }
    //    }


    //    /// <summary>
    //    /// 是否需要验证
    //    /// </summary>
    //    public bool isRequired { get; set; }

    //    /// <summary>
    //    /// 控件高
    //    /// </summary>
    //    public string height { get; set; }

    //    /// <summary>
    //    /// 控件款
    //    /// </summary>
    //    public string width { get; set; }

    //    //private bool _iscallOnChange = false;
    //    ///// <summary>
    //    ///// 是否触发其他的下拉框远程加载(默认fasle)
    //    ///// </summary>
    //    //public bool iscallOnChange
    //    //{
    //    //    get { return _iscallOnChange; }
    //    //    set { _iscallOnChange = value; }
    //    //}
    //    /// <summary>
    //    /// 控件款
    //    /// </summary>
    //    public bool isVerify { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int isfilter { get; set; }



    //    /// <summary>
    //    /// 用于过滤项目负责人
    //    /// </summary>
    //    public bool filterProjEmpId { get; set; }

    //    /// <summary>
    //    /// BaseNameEng 名称
    //    /// </summary>
    //    public string typeName { get; set; }

    //    /// <summary>
    //    /// typeName BaseAtt类型
    //    /// </summary>
    //    public string typeAtt { get; set; }

    //    /// <summary>
    //    /// typeId BaseAtt类型 对应的数值
    //    /// </summary>

    //    public string typeId { get; set; }

    //    /// <summary>
    //    ///过滤人员所在的部门
    //    /// </summary>
    //    public string includetype { get; set; }
    //}
    //#endregion


    #region 参数
    /// <summary>
    /// 按钮参数
    /// </summary>
    public class Params
    {
        /// <summary>
        /// 数据库英文标题
        /// </summary>
        public string engName { get; set; }

        /// <summary>
        /// 生成控件的Name和ID
        /// </summary>
        public string controlName { get; set; }

        /// <summary>
        /// 查询参数字符串
        /// </summary>
        public string queryOptions { get; set; }

        /// <summary>
        /// 需要选中的IDS
        /// </summary>
        public string ids { get; set; }

        /// <summary>
        /// 是否多选 
        /// </summary>
        public bool isMult { get; set; }


        private bool _isSel = true;
        /// <summary>
        /// 是否显示提示文字
        /// </summary>
        public bool isSel
        {
            get { return _isSel; }
            set { _isSel = value; }
        }


        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool isRequired { get; set; }

        /// <summary>
        /// 控件高
        /// </summary>
        public string height { get; set; }

        /// <summary>
        /// 控件宽
        /// </summary>
        public string width { get; set; }

        //private bool _iscallOnChange = false;
        ///// <summary>
        ///// 是否触发其他的下拉框远程加载(默认fasle)
        ///// </summary>
        //public bool iscallOnChange
        //{
        //    get { return _iscallOnChange; }
        //    set { _iscallOnChange = value; }
        //}
        /// <summary>
        /// 控件款
        /// </summary>
        public bool isVerify { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int isfilter { get; set; }



        /// <summary>
        /// 用于过滤项目负责人
        /// </summary>
        public bool filterProjEmpId { get; set; }

        /// <summary>
        /// BaseNameEng 名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// typeName BaseAtt类型
        /// </summary>
        public string typeAtt { get; set; }

        /// <summary>
        /// typeId BaseAtt类型 对应的数值
        /// </summary>

        public string typeId { get; set; }

        /// <summary>
        ///过滤人员所在的部门
        /// </summary>
        public string includetype { get; set; }
        /// <summary>
        /// 是否有全部
        /// 1:有全部选项，2:没有全部选项
        /// 为1时，必须isMult属性为false才起效果
        /// </summary>
        public string isAll { get; set; }
        /// <summary>
        /// 全部选项的值
        /// 建议使用“0”
        /// </summary>
        public string allVal { get; set; }
        /// <summary>
        /// 全部选项的显示文本
        /// 建议使用“全部”
        /// </summary>
        public string allText { get; set; }


    }
    #endregion
}
