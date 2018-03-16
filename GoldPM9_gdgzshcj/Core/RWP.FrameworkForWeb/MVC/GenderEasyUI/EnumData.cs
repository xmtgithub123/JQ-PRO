using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JQ.Web
{
    public class EnumData
    {
        /// <summary>
        /// 单选
        /// </summary>
        public enum EnumBoolDate
        {
            [EnumTitle("是")]
            Yes = 1,

            [EnumTitle("否")]
            No = 2,
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum EnumSex
        {
            [EnumTitle("先生", IsDisplay = false)]
            Sir = 1,

            [EnumTitle("女士")]
            Miss = 2
        }
        /// <summary>
        /// 预约时间
        /// </summary>
        public enum EnumBespeakDate
        {
            [EnumTitle("工作日（周一到周五）", IsDisplay = false)]
            BespeakDate1 = 1,

            [EnumTitle("双休日")]
            BespeakDate2 = 2,

            [EnumTitle("工作日或双休日")]
            BespeakDate3 = 3
        }
        /// <summary>
        /// 预约时段
        /// </summary>
        public enum EnumBespeakTime
        {

            [EnumTitle("上午", IsDisplay = false)]
            BespeakTime1 = 1,

            [EnumTitle("下午")]
            BespeakTime2 = 2,

            [EnumTitle("晚上")]
            BespeakDate3 = 3,

            [EnumTitle("随时")]
            BespeakDate4 = 4
        }

        /// <summary>
        /// 售价
        /// </summary>
        public enum EnumPriceGroup
        {
            [EnumTitle("不限", IsDisplay = false)]
            None = 0,

            [EnumTitle("30万以下")]
            Below30 = 1,

            [EnumTitle("30-50万")]
            From30To50 = 2,

            [EnumTitle("50-80万")]
            From50To80 = 3,

            [EnumTitle("80-100万")]
            From80To100 = 4,

            [EnumTitle("100-150万")]
            From100To50 = 5,

            [EnumTitle("150-200万")]
            From150To200 = 6,

            [EnumTitle("200万以上")]
            Above200 = 7
        }

        /// <summary>
        /// 面积
        /// </summary>
        public enum EnumAcreageGroup
        {

            [EnumTitle("不限", IsDisplay = false)]
            None = 0,

            [EnumTitle("50mm以下")]
            Below50 = 1,

            [EnumTitle("50-70mm")]
            From50To70 = 2,

            [EnumTitle("70-90mm")]
            From70To90 = 3,

            [EnumTitle("90-120mm")]
            From90To120 = 4,

            [EnumTitle("120-150mm")]
            From120To150 = 5,

            [EnumTitle("150-200mm")]
            From150To200 = 6,

            [EnumTitle("200-300mm")]
            From200To300 = 7,

            [EnumTitle("300mm以上")]
            Above300 = 8
        }

        /// <summary>
        /// 房型  二室 三室 四室 五室 五室以上
        /// </summary>
        public enum EnumRoomGroup
        {
            [EnumTitle("不限", IsDisplay = false)]
            None = 0,

            [EnumTitle("一室")]
            Room1 = 1,

            [EnumTitle("二室")]
            Room2 = 2,

            [EnumTitle("三室")]
            Room3 = 3,

            [EnumTitle("四室")]
            Room4 = 4,

            [EnumTitle("五室")]
            Room5 = 5,

            [EnumTitle("五室以上")]
            Above5 = 6
        }
    }
}
