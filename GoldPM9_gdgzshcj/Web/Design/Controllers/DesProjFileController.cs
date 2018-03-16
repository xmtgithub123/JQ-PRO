using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Design.Controllers
{
    [Description("DesProjFile")]
    public partial class DesProjFileController : CoreController
    {
        private DBSql.Sys.BaseAttach baseAttachDB = new DBSql.Sys.BaseAttach();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
