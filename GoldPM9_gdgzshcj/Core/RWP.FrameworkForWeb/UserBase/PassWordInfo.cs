/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JQ.Web
{
    /// <summary>
    /// 密码修改
    /// </summary>
    public class PassWordInfo
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Display(Name = "旧密码")]
        [Required(ErrorMessage = "请输入旧密码")]
        [StringLength(32, ErrorMessage = "输入的旧密码长度必须是6到32个字符", MinimumLength = 6)]
        public string oldPassWord { get; set; }


        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "请输入新密码")]
        [StringLength(32, ErrorMessage = "输入的新密码长度必须是6到32个字符", MinimumLength = 6)]
        public string newPassWord { get; set; }

        /// <summary>
        /// 确认新密码
        /// </summary>
        [Display(Name = "确认新密码")]
        [System.ComponentModel.DataAnnotations.Compare("newPassWord", ErrorMessage = "密码必须一致")]
        public string inputNewPassWord { get; set; }
    }
}
