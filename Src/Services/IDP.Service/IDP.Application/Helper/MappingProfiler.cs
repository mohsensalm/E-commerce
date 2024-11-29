using AutoMapper;
using IDP.Application.Comands.Auth;
using IDP.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Helper
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<AuthComand, User>().ReverseMap();
        }
    }
}
