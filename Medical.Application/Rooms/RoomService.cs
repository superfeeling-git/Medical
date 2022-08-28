﻿using Medical.Application.Menus;
using Medical.Application.Rooms.Dto;
using Medical.Domain.Menus;
using Medical.Domain.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Medical.Application.Rooms
{
    public class RoomService : ApplicationService, IRoomService
    {
        private readonly IRepository<Room> repository;
        private readonly IRepository<Menu> menurepository;

        public RoomService(IRepository<Room> repository, IRepository<Menu> menurepository)
        {
            this.repository = repository;
            this.menurepository = menurepository;
        }

        /// <summary>
        /// 根据病区Id查所有的病房
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/Room/QueryByRoomId")]
        public async Task<List<RoomDto>> QueryByRegionId(Guid id)
        {
            var list = await repository.Where(m => m.RegionId == id).ToListAsync();
            return ObjectMapper.Map<List<Room>, List<RoomDto>>(list);
        }


        [HttpGet("/Room/Insert")]
        public async Task BulkInsert()
        {
            //找到所有病区的末级
            var list = await menurepository.GetListAsync();
            
            //找到的            
            list.ForEach(async menu => { 
                if(!list.Any(a => a.ParnetId == menu.Id))
                {
                    Random random = new Random();
                    var code = random.Next(3, 6);
                    for (int i = 1; i < code; i++)
                    {
                        List<int> beds = new List<int>();
                        for (int a = 1; a <= 8; a++)
                        {
                            beds.Add(a);
                        }
                        await repository.InsertAsync(new Room
                        {
                            RegionId = menu.Id,
                            RoomName = $"1{i.ToString().PadLeft(2, '0')}",
                            BedNum = string.Join(',', beds)
                        });
                    }
                }
            });
        }
    }
}