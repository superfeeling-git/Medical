using Medical.Application.Admins.Dto;
using Medical.Application.Menus.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Medical.Application.Menus
{
    public interface IMenuService : IApplicationService
    {
        Task<ResultDto<MenuDto>> Add(MenuDto dto);
        Task<ResultDto<List<TreeDto>>> GetTreeNodes();
    }
}
