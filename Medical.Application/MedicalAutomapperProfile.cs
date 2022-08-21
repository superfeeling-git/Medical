using AutoMapper;
using Medical.Application.Admins.Dto;
using Medical.Application.Menus.Dto;
using Medical.Domain.Admins;
using Medical.Domain.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application
{
    public class MedicalAutomapperProfile : Profile
    {
        public MedicalAutomapperProfile()
        {
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Admin, RegisterDto>().ReverseMap();
            CreateMap<MenuDto, Menu>().ReverseMap();
            CreateMap<Menu, TreeDto>()
            .ForMember(m => m.Id, opt =>
            {
                opt.MapFrom(src => src.Id);
            })
            .ForMember(m => m.Pid, opt =>
            {
                opt.MapFrom(src => src.ParnetId);
            })
            .ForMember(m => m.Name, opt =>
            {
                opt.MapFrom(src => src.MenuName);
            }).ReverseMap();
        }
    }
}
