using Medical.Application.Admins.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;
using Namotion.Reflection;
using System.Security.Cryptography;
using NPOI.HSSF.Util;
using Volo.Abp.Uow;
using System.Net;
using Medical.Utility;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Medical.Domain.Admins;

namespace Medical.Application.Admins.Service
{
    public class AdminService : ApplicationService, IAdminService
    {
        private readonly IRepository<Admin> rep;
        private readonly IHttpContextAccessor httpContext;
        private readonly IConfiguration configuration;

        public AdminService(IRepository<Admin> rep, IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            this.rep = rep;
            this.httpContext = httpContext;
            this.configuration = configuration;
        }

        [HttpPost("/Admin/Register")]
        public async Task<ResultDto<AdminDto>> Register(RegisterDto adminDto)
        {
            var Admin = await rep.AnyAsync(m => m.UserName == adminDto.UserName);
            if (Admin)
            {
                return new ResultDto<AdminDto> { Code = HttpStatusCode.OK, Msg = "用户已存在" };
            }
            adminDto.Password = MD5Helper.GetPassword(adminDto.Password);
            var entity = ObjectMapper.Map<RegisterDto, Admin>(adminDto);
            var admin = await rep.InsertAsync(entity);
            var dto = ObjectMapper.Map<Admin,AdminDto>(admin);
            return new ResultDto<AdminDto> { Code = HttpStatusCode.OK, Msg = "注册成功", Data = dto };
        }

        [HttpPost("/Admin/Login")]
        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            var Admin = await rep.FirstOrDefaultAsync(m => m.UserName == loginDto.UserName);
            if(Admin == null)
            {
                return new LoginResultDto { Code = HttpStatusCode.OK, Msg = "用户不存在" };
            }
            else
            {
                //解锁
                if(Admin.IsLock && Admin.LockTime < DateTime.Now)
                {
                    Admin.IsLock = false;
                    Admin.ErrorLoginCount = 0;
                    Admin.LockTime = null;
                    await rep.UpdateAsync(Admin);
                }

                if (Admin.IsLock)
                {
                    return new LoginResultDto { Code = HttpStatusCode.OK, Msg = "账号已锁定" };
                }
                else
                {
                    if (Admin.Password != MD5Helper.GetPassword(loginDto.Password))
                    {
                        if (Admin.ErrorLoginCount >= 3)
                        {
                            Admin.LockTime = DateTime.Now.AddMinutes(30);
                            Admin.IsLock = true;
                            await rep.UpdateAsync(Admin);
                            return new LoginResultDto { Code = HttpStatusCode.OK, Msg = "账号已锁定" };
                        }
                        Admin.ErrorLoginCount += 1;
                        await rep.UpdateAsync(Admin);
                        return new LoginResultDto { Code = HttpStatusCode.OK, Msg = "密码错误" };
                    }
                    else
                    {
                        //更新末次登录时间
                        Admin.LastLoginTime = DateTime.Now;
                        Admin.ErrorLoginCount = 0;
                        Admin.LockTime = null;
                        Admin.LastLoginIP = httpContext.HttpContext.GetRemoteIPAddress().ToString();
                        await rep.UpdateAsync(Admin);

                        //var roles = string.Join(",", await adminRepository.GetRoleAsync(admin.AdminId));

                        IList<Claim> claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Id,Admin.Id.ToString()),
                        new Claim(JwtClaimTypes.Name,loginDto.UserName),
                        new Claim(JwtClaimTypes.Email,loginDto.UserName),
                        //new Claim(JwtClaimTypes.Role,roles)
                    };

                        //JWT密钥
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Bearer:SecurityKey"]));

                        //算法
                        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        //过期时间
                        DateTime expires = DateTime.UtcNow.AddHours(10);


                        //Payload负载
                        var token = new JwtSecurityToken(
                            issuer: configuration["JwtConfig:Bearer:Issuer"],
                            audience: configuration["JwtConfig:Bearer:Audience"],
                            claims: claims,
                            notBefore: DateTime.UtcNow,
                            expires: expires,
                            signingCredentials: cred
                            );

                        var handler = new JwtSecurityTokenHandler();

                        //生成令牌
                        string jwt = handler.WriteToken(token);

                        return new LoginResultDto { Code = HttpStatusCode.OK, Token = jwt };
                    }
                }
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("/Admin/GetUserInfo")]
        public async Task<ResultDto<ClaimDto>> GetUserInfo(string Token)
        {
            var claim = Token.decode();
            var tokenData = JsonConvert.DeserializeObject<ClaimDto>(claim);
            var admin = await rep.FirstOrDefaultAsync(m => m.UserName == tokenData.name);
            if(admin == null)
            {
                return new ResultDto<ClaimDto> { Code = HttpStatusCode.OK, Msg = "无此用户" };
            }
            else
            {
                var role = new string[] { "admin" };
                return new ResultDto<ClaimDto> { Code = HttpStatusCode.OK, Data = new ClaimDto { id = admin.Id, name = admin.UserName, roles = role } };
            }
        }
    }
}
