using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical.Utility;
using Medical.Application.Admins.Dto;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Medical.Application.Menus;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Medical.Domain.Menus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Medical.Application.Auth
{
    /// <summary>
    /// 自定义授权过滤器
    /// </summary>
    public class CustomerAuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public ILogger<MenuService> logger { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;

            var menuRepository = serviceProvider.GetService(typeof(IRepository<Menu>)) as IRepository<Menu>;

            //1、用户信息
            var token = context.HttpContext.Request.Headers["authorization"].ToString().Split(new char[] { ' ' }).Last();
            if (string.IsNullOrWhiteSpace(token))
            {
                //401
                context.Result = new UnauthorizedResult();
                return;
            }

            var claim = token.decode();
            var tokenData = JsonConvert.DeserializeObject<ClaimDto>(claim);

            //2、当前正在访问的接口
            string Url = context.ActionDescriptor.AttributeRouteInfo.Template;

            //3、访问数据库
        }
    }

    /// <summary>
    /// 自定义授权过滤器
    /// </summary>
    public class CustomerAuthAttributeAsync : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated || context.Filters.Any(item => item is IAllowAnonymousFilter)) 
                return;

            var serviceProvider = context.HttpContext.RequestServices;

            var menuRepository = serviceProvider.GetService(typeof(IRepository<Menu>)) as IRepository<Menu>;

            var list = await menuRepository.GetListAsync();

            //1、用户信息
            var token = context.HttpContext.Request.Headers["authorization"].ToString().Split(new char[] { ' ' }).Last();
            if (string.IsNullOrWhiteSpace(token))
            {
                //401
                context.Result = new UnauthorizedResult();
                return;
            }

            var claim = token.decode();
            var tokenData = JsonConvert.DeserializeObject<ClaimDto>(claim);

            //2、当前正在访问的接口
            string Url = context.ActionDescriptor.AttributeRouteInfo.Template;

            //3、访问数据库
        }
    }
}
