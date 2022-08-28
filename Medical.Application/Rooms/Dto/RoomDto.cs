using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Medical.Application.Rooms.Dto
{
    public class RoomDto: EntityDto<Guid>
    {
        /// <summary>
        /// 病房
        /// </summary>
        public string RoomName { get; set; }
        /// <summary>
        /// 病床
        /// </summary>
        public string BedNum { get; set; }
        /// <summary>
        /// 病区Id
        /// </summary>
        public Guid RegionId { get; set; }
    }
}
