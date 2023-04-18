using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;

namespace MediaManager.API.Profiles;

public class M3usProfile : Profile
{
    public M3usProfile()
    {
        CreateMap<M3uFile, M3uFileDto>();

   }
}
