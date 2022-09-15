using Medical.Application;
using Medical.Application.Auth;
using Medical.Domain;
using Medical.Domain.Admins;
using Medical.Domain.Menus;
using Medical.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Autofac;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;

namespace Medical.WebApi
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(MedicalEntityFrameworkCoreModule),
        typeof(MedicalDomainModule),
        typeof(MedicalApplicationModule)
        )]
    public class MedicalWebApiModule : AbpModule
    {
        #region ConfigureServices
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var config = context.Services.GetConfiguration();

            services.AddHttpContextAccessor();
            services.AddControllers(option => {
                //全局过滤器
                //option.Filters.Add<CustomerAuthAttributeAsync>();
            });

            #region 自动WebApi
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options
                    .ConventionalControllers
                    .Create(typeof(MedicalApplicationModule).Assembly);
            });
            #endregion

            #region 允许Post提交
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });
            #endregion            

            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.WithOrigins(config["Origin"].Split(','))
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            #region Jwt认证参数(身份认证)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = config["JwtConfig:Bearer:Issuer"],//发行人

                    //是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = config["JwtConfig:Bearer:Audience"],//受众人

                    //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:Bearer:SecurityKey"])),

                    ValidateLifetime = true, //验证生命周期

                    RequireExpirationTime = true, //过期时间

                    ClockSkew = TimeSpan.Zero   //平滑过期偏移时间
                };
            });
            #endregion

            services.AddSingleton<IAuthorizationHandler, CustomPolicyHandler>();

            #region 策略授权
            services.AddAuthorization(options => {
                options.AddPolicy("EditorPolicy", configurePolicy => {
                    /*configurePolicy.RequireAuthenticatedUser();
                    configurePolicy.RequireClaim("UserName", "zhangsan", "lisi", "admin");
                    configurePolicy.RequireRole("editor");
                    configurePolicy.RequireUserName("admin");*/
                    configurePolicy.Requirements.Add(new CustomPolicyRequirment());
                });
            });
            #endregion

            #region Swagger配置
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical.WebApi", Version = "v1" });
                options.DocInclusionPredicate((doc, desc) => true);
                //开启权限小锁
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //在header中添加token，传递到后台
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格) \"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion

        }
        #endregion

        #region ApplicationInitialization
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical.WebApi v1"));

            app.UseStaticFiles();

            //app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();            

            //认证——JWT
            app.UseAuthentication();

            //授权——授权业务逻辑
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion
    }

    public class CustomPolicyRequirment: IAuthorizationRequirement
    {

    }

    public class CustomPolicyHandler : AuthorizationHandler<CustomPolicyRequirment>
    {
        private readonly IRepository<Admin> admins;

        public CustomPolicyHandler(IRepository<Admin> admins)
        {
            this.admins = admins;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomPolicyRequirment requirement)
        {
            //1、用户信息
            var UserName = context.User.Identity.Name;

            IEnumerable<Claim> claims = context.User.Claims;

            //2、当前正在访问的接口
            if (context.Resource is HttpContext httpContext)
            {
                var serviceProvider = httpContext.RequestServices;
                var menuRepository = serviceProvider.GetService(typeof(IRepository<Menu>)) as IRepository<Menu>;
                var menulist = await menuRepository.GetListAsync();

                var endpoint = httpContext.GetEndpoint();
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                string Url = actionDescriptor.AttributeRouteInfo.Template;
            }

            //3、访问数据库
            var list = await admins.GetListAsync();

            
        }
    }
}
