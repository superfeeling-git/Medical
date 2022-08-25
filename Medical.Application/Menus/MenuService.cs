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
        /// 获取递归菜单树
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("/Menu/Table")]
        public async Task<ResultDto<List<MenuDtoOutput>>> GetTableTree()
        {
            var list = await repository.GetListAsync();            

            var menu = list.Where(m => m.ParnetId == Guid.Empty).Select(m => new MenuDtoOutput
            {
                Id = m.Id,
                ParnetId = m.ParnetId,
                ComponentPath = m.ComponentPath,
                IsShow = m.IsShow,
                MenuName = m.MenuName,
                MenuNameEn = m.MenuNameEn,
                MenuPath = m.MenuPath
            }).ToList();

            GetNodes(menu, list);

            return new ResultDto<List<MenuDtoOutput>> { Code = HttpStatusCode.OK, Data = menu };
        }

        /// <summary>
        /// 递归--终止条件
        /// </summary>
        /// <param name="menus"></param>
        private void GetNodes(List<MenuDtoOutput> menus, List<Menu> list)
        {
            foreach (var item in menus)
            {
                var _list = list.Where(s => s.ParnetId == item.Id).Select(m => new MenuDtoOutput
                {
                    Id = m.Id,
                    ParnetId = m.ParnetId,
                    ComponentPath = m.ComponentPath,
                    IsShow = m.IsShow,
                    MenuName = m.MenuName,
                    MenuNameEn = m.MenuNameEn,
                    MenuPath = m.MenuPath
                }).ToList();

                item.children.AddRange(_list);

                GetNodes(_list, list);
            }
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