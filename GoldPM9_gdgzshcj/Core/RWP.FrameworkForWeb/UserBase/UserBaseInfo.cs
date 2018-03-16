/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System;
using System.ComponentModel.DataAnnotations;

namespace JQ.Web
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    [Serializable]
    public class UserBaseInfo
    {
        public long ID { get; set; }

        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        [StringLength(20, ErrorMessage = "用户名不能超过{1}字符")]
        public string userName { get; set; }

        [Display(Name = "密码")]      
        [StringLength(32, ErrorMessage = "密码长度必须是4到32个字符", MinimumLength = 4)]
        public string passWord { get; set; }

        [Display(Name = "Mark地址")]
        public string markIP { get; set; }

        [Display(Name = "加密密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string HFPassword { get; set; }
    }
}
