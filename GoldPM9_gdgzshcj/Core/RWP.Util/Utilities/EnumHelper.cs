/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/17
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace JQ.Util
{
    /// <summary>
    /// 枚举助手
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 返回枚举值
        /// </summary>
        public static TEnum getValue<TEnum>(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }
        /// <summary>
        /// 验证枚举值
        /// </summary>
        public static bool tryValue<TEnum>(object value)
        {
            if (value == null) return false;
            try
            {
                TEnum t = getValue<TEnum>(value.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }


        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "请选择...", Value = "" } };

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = GetEnumDescription(value),
                                                    Value = value.ToString(),
                                                    Selected = value.Equals(metadata.Model)
                                                };

            // If the enum is nullable, add an 'empty' item to the collection
            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        /// <summary>
        /// 获取枚举下拉列表
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="selectValueStr">选中枚举值列表</param>
        /// <param name="excludeNoDescription">是否排除无description的枚举项</param>
        /// <returns></returns>
        public static IList<SelectListItem> getEnumDropDownList<TEnum>(string selectValueStr, bool excludeNoDescription)
        {
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            List<string> selectValues = new List<string>();
            if (!string.IsNullOrEmpty(selectValueStr))
            {   //获取选中的枚举值
                selectValues.AddRange(selectValueStr.Split(StringUtil.splitChar).Where(m => !string.IsNullOrEmpty(m)));
            }
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> enumItems = from value in values
                                                    select new SelectListItem
                                                    {
                                                        Text = GetEnumDescription(value),
                                                        Value = value.ToString(),
                                                        Selected = selectValues.Contains(value.ToString())
                                                    };
            if (excludeNoDescription)
            {
                enumItems = enumItems.Where(m => !StringUtil.compareIgnoreCase(m.Text, m.Value));
            }
            dropDownList.AddRange(enumItems);
            return dropDownList;
        }
        /// <summary>
        /// 返回列表
        /// </summary>
        //public static IEnumerable ToIList(Type enumType)
        //{
        //    return enumType.GetFields()
        //                   .Where(p => p.FieldType.IsEnum)
        //                   .Select(p => new { Text = GetDescription(p), Value = p.Name });
        //}
        //private static ConcurrentDictionary<string, > _enmuSelectCache = new ConcurrentDictionary<string, IEnumerable<SelectListItem>>();


    }
}
