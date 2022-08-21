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
        public string Token { get; set; }
    }
}
