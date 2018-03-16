/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/11
*           175417739@qq.com                   
********************************************************************/

using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace JQ.Log
{
    /// <summary>
    /// 日志服务接口
    /// </summary>
    [ServiceContract]
    public interface ILogHost
    {
        [Description("日志接口")]
        [OperationContract, WebInvoke]
        void postLog(List<LogModel> logList);
    }
}
