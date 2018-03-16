/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9               
********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JQ.Util
{
    /// <summary>
    /// 实体验证助手
    /// </summary>
    public static class DataAnnotationHelper
    {
        /// <summary>
        /// 默认验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DoResult isValid<T>(this T entity)
        {
            return isValid<T>(entity, false, null);
        }
        /// <summary>
        /// 验证实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="modelIsValid">绑定验证结果 true则不做基础验证</param>
        /// <param name="Others">其他验证委托</param>
        /// <returns></returns>
        public static DoResult isValid<T>(this T entity, bool modelIsValid, Func<T, List<ValidationResult>> Others)
        {
            List<ValidationResult> vrs = new List<ValidationResult>();
            bool isvalid = true;
            if (!modelIsValid)
            {
                isvalid = entity.isValid(out vrs);
            }
            if (Others != null)
            {
                //获取其他验证
                List<ValidationResult> ovrs = Others(entity);
                if (vrs.Count > 0)
                {
                    if (!isvalid)
                    {
                        foreach (var item in ovrs)
                        {
                            vrs.Add(item);
                        }
                    }
                }
                else
                {
                    vrs = ovrs;
                }
            }
            return DoResultHelper.doIsValid(vrs);
        }
        /// <summary>
        /// 验证实体 返回错误信息列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool isValid<T>(this T entity, out List<ValidationResult> validationResults)
        {
            var _validationResults = new List<ValidationResult>();
            
            var validationContext = new ValidationContext(entity, null, null);
            bool isvalid = Validator.TryValidateObject(entity, validationContext, _validationResults, true);
            validationResults = _validationResults;
            return isvalid;
        }
        /// <summary>
        /// 生成错误信息
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="args">可变字符组</param>
        /// <returns></returns>
        public static ValidationResult validResult(string errorMessage, params string[] args)
        {
            IEnumerable<string> memberNames = args.Select(s => s);
            return new ValidationResult(errorMessage, memberNames);
        }


      
    }
}
