using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Medical.Application.Admins.Dto
{
    public class AdminDto : EntityDto<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIP { get; set; }
        public int? ErrorLoginCount { get; set; }
        public bool IsLock { get; set; }
    }
}
