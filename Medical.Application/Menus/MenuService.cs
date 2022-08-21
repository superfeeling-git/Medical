using Medical.Application.Menus.Dto;
using Medical.Domain.Menus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Medical.Application.Menus
{
    public class MenuService : ApplicationService, IMenuService
    {
        private readonly IRepository<Menu> repository;

        public MenuService(IRepository<Menu> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("/Menu/List")]
        public async Task<ResultDto<List<TreeDto>>> GetTreeNodes()
        {
            var list = await repository.GetListAsync();
            return new ResultDto<List<TreeDto>> { Code = HttpStatusCode.OK, Data = ObjectMapper.Map<List<Menu>,List<TreeDto>>(list) };
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("/Menu/Add")]
        public async Task<ResultDto<MenuDto>> Add(MenuDto dto)
        {
            var entity = ObjectMapper.Map<MenuDto, Menu>(dto);
            await repository.InsertAsync(entity);
            return new ResultDto<MenuDto> { Code = HttpStatusCode.OK, Data = ObjectMapper.Map<Menu, MenuDto>(entity) };
        }
    }
}