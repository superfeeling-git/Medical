using System;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Medical.Domain
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class MedicalDomainModule : AbpModule
    {
    }
}
