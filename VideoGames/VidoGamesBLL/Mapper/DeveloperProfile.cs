using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.BLL.Dtos;
using VideoGames.DAL.Entities;

namespace VideoGames.BLL.Mapper
{
    public class DeveloperProfile : Profile
    {
        public DeveloperProfile()
        {
            CreateMap<CreateDeveloperDto, Developer>();
            CreateMap<Developer, DeveloperDto>();
        }
    }
}
