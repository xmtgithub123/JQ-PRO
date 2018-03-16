using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace DBSql.PubFlow
{
    /// <summary>
    /// 流程业务处理器接口
    /// </summary>
    public interface IFlowProcessor
    {
        FlowWareOptions Options
        {
            get; set;
        }

        DbContext CurrentDbContext
        {
            get; set;
        }

        HttpRequest Request
        {
            get; set;
        }

        HttpSessionState Session
        {
            get; set;
        }

        Action<object> SetCreateProperties
        {
            get; set;
        }

        Action<object> SetModifyProperties
        {
            get; set;
        }

        /// <summary>
        /// 保存表单
        /// </summary>
        void OnSaveForm();

        /// <summary>
        /// 在操作完之前调用
        /// </summary>
        void OnExecuting();

        /// <summary>
        /// 在操作完之后调用
        /// </summary>
        void OnExecuted(bool isSuccess);

        /// <summary>
        /// 在流程结束完成后调用
        /// </summary>
        void OnAfterApproveFinished();
    }
}
