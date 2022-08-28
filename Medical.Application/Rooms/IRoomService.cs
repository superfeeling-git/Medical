using Medical.Application.Rooms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Medical.Application.Rooms
{
    public interface IRoomService : IApplicationService
    {
        Task BulkInsert();
        Task<List<RoomDto>> QueryByRegionId(Guid id);
    }
}
