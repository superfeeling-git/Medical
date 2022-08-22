using Medical.Application.Admins.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application.Admins.Dto
{
    /// <summary>
    /// 登录Dto
    /// </summary>
    public class LoginResultDto : ResultDto
    {
        /// <summary>
        /// 登录状态
        /// </summary>
        public LoginStatus LoginStatus { get; set; }
        /// <summary>
        /// 返回的token
        /// </summary>
        public string Token { get; set; }
    }
}
