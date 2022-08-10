using Medical.Application.Admins.Dto;
using Medical.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Medical.Application.Admins.Service
{
    public class AdminService : ApplicationService, IAdminService
    {
        /*private readonly IRepository<Admin> rep;

        public AdminService(IRepository<Admin> rep)
        {
            this.rep = rep;
        }*/

        [HttpPost("/Admin/Add")]
        public async Task<AdminDto> Create(AdminDto adminDto)
        {
            /*var entity = ObjectMapper.Map<AdminDto, Admin>(adminDto);
            var admin = await rep.InsertAsync(entity);
            return ObjectMapper.Map<Admin,AdminDto >(admin);*/
            return null;
        }
    }
}
