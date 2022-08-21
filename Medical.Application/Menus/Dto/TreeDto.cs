using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application.Menus.Dto
{
    public class TreeDto
    {
        public Guid Id { get; set; }
        public Guid Pid { get; set; }
        public string Name { get; set; }
    }
}
