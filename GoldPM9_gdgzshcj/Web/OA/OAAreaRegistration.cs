using System.Web.Mvc;

namespace OA
{
    public class OAAreaRegistration: AreaRegistration
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public override string AreaName
        {
            get
            {
                //项目名称 如与区域名称不一致，则需要更改生成事件的批处理
                return "OA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //路由规则小写
            context.MapRoute(
                "oa_default",
                "oa/{controller}/{action}/{id}",
                new { controller = "layout", action = "Default", id = UrlParameter.Optional }
            );
        }
    }
}