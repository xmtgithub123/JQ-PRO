using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Consts
    { 
        /// <summary>
        ///操作中的状态
        /// </summary>
        public static List<int> OperateingStatus
        {
            get
            {
                return new List<int> 
                {
                    Convert.ToInt32(DataModel.NodeStatus.进行),
                    Convert.ToInt32(DataModel.NodeStatus.轮到),
                    Convert.ToInt32(DataModel.NodeStatus.会签中), 
                };
            }
        }


    }
}
