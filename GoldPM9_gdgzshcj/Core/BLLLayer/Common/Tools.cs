 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Tools
    {
        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="formatType">格式化类型</param>
        /// <param name="isMin">是否输出1900-01-01</param>
        /// <returns></returns>
        public static string FormatTime(DateTime time,string formatType,bool isMin)
        {
            try
            {
                if (isMin)
                {
                    return time.ToString(formatType);
                }
                else
                {
                    string showTime = time.ToString(formatType);
                    if (showTime.Contains("1900"))
                    {
                        return "";
                    }
                    else
                    {
                        return showTime;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="formatType">格式化类型</param>
        /// <param name="isMin">是否输出1900-01-01</param>
        /// <returns></returns>
        public static string FormatTime(string time, string formatType, bool isMin)
        {
            try
            {
                if (isMin)
                {
                    return DateTime.Parse(time).ToString(formatType);
                }
                else
                {
                    string showTime = DateTime.Parse(time).ToString(formatType);
                    if (showTime.Contains("1900"))
                    {
                        return "";
                    }
                    else
                    {
                        return showTime;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
