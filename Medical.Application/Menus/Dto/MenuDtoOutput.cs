using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application.Menus.Dto
{
    public class MenuDtoOutput : MenuDto
    {
        public List<MenuDtoOutput> children { get; set; } = new List<MenuDtoOutput>();
    }
}
