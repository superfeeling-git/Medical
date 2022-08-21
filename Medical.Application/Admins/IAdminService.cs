using Medical.Application.Admins.Dto;
using Medical.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Medical.Application.Admins
{
    public interface IAdminService : IApplicationService
    {
        Task<ResultDto<AdminDto>> Register(RegisterDto adminDto);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        Task<LoginResultDto> Login(LoginDto loginDto);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        Task<ResultDto<ClaimDto>> GetUserInfo(string Token);
    }
}
