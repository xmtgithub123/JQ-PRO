/********************************************************************
*           CLR Version:     v4.0        
*           Created by 冷聪 at 2014/11/11                 
********************************************************************/

using System;
using System.ComponentModel.DataAnnotations;

namespace JQ.Util
{
    /// <summary>
    /// 身份证号校验 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IdentifyCardAttribute : ValidationAttribute
    {
        /// <summary>
        /// 重写身份证效验
        /// </summary>
        /// <param name="value">身份证号</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var identifyCardNumber = TypeHelper.parseString(value);
            return identifyCardNumberValidate(identifyCardNumber);
        }

        #region 身份证号校验
        /// <summary>
        /// 身份证号校验
        /// </summary>
        /// <param name="identifyCardNumber">身份证号</param>
        /// <returns></returns>
        private bool identifyCardNumberValidate(string identifyCardNumber)
        {
            DateTime birthday;
            return identifyCardNumberValidate(identifyCardNumber, out birthday);
        }

        /// <summary>
        /// 身份证号校验
        /// </summary>
        /// <param name="identifyCardNumber">身份证号</param>
        /// <param name="birthdayTime">出生日期</param>
        /// <returns></returns>
        private bool identifyCardNumberValidate(string identifyCardNumber, out DateTime birthdayTime)
        {
            birthdayTime = default(DateTime);
            ErrorMessage = string.Empty;
            if (string.IsNullOrEmpty(identifyCardNumber))
            {
                ErrorMessage = "身份证号为空";
                return false;
            }
            if (!RegexPatterns.isMatch(identifyCardNumber, RegexPatterns.identityCard))
            {
                ErrorMessage = "身份证号格式不正确";
                return false;
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(identifyCardNumber.Remove(2)) == -1)  //省份验证
            {
                ErrorMessage = "身份证号中省份非法";
                return false;
            }
            if (!checkBirthday(identifyCardNumber, out birthdayTime))
            {
                ErrorMessage = "身份证号中出生日期不正确";
                return false;
            }
            if (!checkVerifyCode(identifyCardNumber))  //校验码验证
            {
                ErrorMessage = "身份证号中校验码不正确";
                return false;
            }
            return true;  //标识校验成功
        }

        /// <summary>
        /// 校验出生日期
        /// </summary>
        /// <param name="identifyCardNumber">身份证号</param>
        /// <param name="birthdayTime">出生日期</param>
        /// <returns></returns>
        private static bool checkBirthday(string identifyCardNumber, out DateTime birthdayTime)
        {
            birthdayTime = default(DateTime);
            if (string.IsNullOrEmpty(identifyCardNumber) || !RegexPatterns.isMatch(identifyCardNumber, RegexPatterns.identityCard))
                return false;

            string birthdayTimeString = string.Empty;
            if (identifyCardNumber.Length == 18)
                birthdayTimeString = identifyCardNumber.Substring(6, 4) + "-" + identifyCardNumber.Substring(10, 2) + "-" + identifyCardNumber.Substring(12, 2);
            else if (identifyCardNumber.Length == 15)
                birthdayTimeString = "19" + identifyCardNumber.Substring(6, 2) + "-" + identifyCardNumber.Substring(8, 2) + "-" + identifyCardNumber.Substring(10, 2);

            return DateTime.TryParse(birthdayTimeString, out birthdayTime);
        }

        /// <summary>
        /// 校验码验证
        /// </summary>
        /// <param name="identifyCardNumber">身份证号</param>
        /// <returns></returns>
        private static bool checkVerifyCode(string identifyCardNumber)
        {
            if (string.IsNullOrEmpty(identifyCardNumber) || identifyCardNumber.Length != 18)
                return true;   //15位身份证没有校验码
            string verify = "10x98765432";
            int[] Wi = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            char[] Ai = identifyCardNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += Wi[i] * int.Parse(Ai[i].ToString());
            }
            return StringUtil.compareIgnoreCase(verify[sum % 11].ToString(), identifyCardNumber.Substring(17, 1));
        }
        #endregion
    }
}
