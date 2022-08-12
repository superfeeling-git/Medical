using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Medical.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MedicalEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MedicalDbContext>(opt => {
                opt.AddDefaultRepositories(includeAllEntities:true);
            });

            Configure<AbpDbContextOptions>(opt => {
                opt.UseSqlServer();
            });
        }
    }
}
