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
        Task<AdminDto> Create(AdminDto adminDto);
        Task<string> ExportExcel();
        string GetMd5(string input);
        Task<string> MiniExportExcel();
        Task<List<Admin>> MiniImportExcel();
    }
}
