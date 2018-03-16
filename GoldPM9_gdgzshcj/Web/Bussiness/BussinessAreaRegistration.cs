using System.Web.Mvc;

namespace Bussiness
{
    public class BussinessAreaRegistration: AreaRegistration
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public override string AreaName
        {
            get
            {
                //项目名称 如与区域名称不一致，则需要更改生成事件的批处理
                return "Bussiness";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //路由规则小写
            context.MapRoute(
                "bussiness_default",
                "bussiness/{controller}/{action}/{id}",
                new { controller = "layout", action = "Default", id = UrlParameter.Optional }
            );
        }
    }
}