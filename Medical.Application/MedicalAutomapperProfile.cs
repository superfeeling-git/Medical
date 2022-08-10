using AutoMapper;
using Medical.Application.Admins.Dto;
using Medical.Domain;
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
        }
    }
}
