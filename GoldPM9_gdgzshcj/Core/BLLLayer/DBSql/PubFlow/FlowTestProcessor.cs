using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using DataModel;
using DataModel.Models;

namespace DBSql.PubFlow
{
    /// <summary>
    /// //TODO 测试流程数据所用，过后可删除
    /// </summary>
    public class FlowTestProcessor : IFlowProcessor
    {

        public DbContext CurrentDbContext
        {
            get; set;
        }

        public FlowWareOptions Options
        {
            get; set;
        }

        public HttpRequest Request
        {
            get; set;
        }

        public HttpSessionState Session
        {
            get; set;
        }

        public Action<object> SetCreateProperties
        {
            get; set;
        }

        public Action<object> SetModifyProperties
        {
            get; set;
        }

        public EmpSession CurrentUser
        {
            get; set;
        }

        public void OnAfterApproveFinished()
        {

        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }

        public void OnSaveForm()
        {
            DataModel.Models.FlowTest flowTest = null;
            if (this.Options.RefID == 0)
            {
                flowTest = new FlowTest();
                this.CurrentDbContext.Set<DataModel.Models.FlowTest>().Add(flowTest);
            }
            else
            {
                flowTest = this.CurrentDbContext.Set<DataModel.Models.FlowTest>().FirstOrDefault(m => m.ID == this.Options.RefID);
                if (flowTest == null)
                {
                    throw new Common.JQException("无法找到相关实体");
                }
            }
            flowTest.Test1 = "Test1";
            flowTest.Test2 = "Test2";
            if (this.Options.RefID == 0)
            {
                this.CurrentDbContext.SaveChanges();
                this.Options.RefID = flowTest.ID;
            }
        }
    }
}
