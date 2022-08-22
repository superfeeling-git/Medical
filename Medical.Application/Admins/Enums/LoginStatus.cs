using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application.Admins.Enums
{
    public enum LoginStatus
    {
        /// <summary>
        /// 用户不存在
        /// </summary>
        UserNoExists = 1,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordWrong = 2,
        /// <summary>
        /// 账号锁定
        /// </summary>
        AccountLocked = 3,
        /// <summary>
        /// 登录成功
        /// </summary>
        Success = 0
    }
}
