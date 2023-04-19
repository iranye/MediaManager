using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;

namespace MediaManager.API.Profiles;

public class M3uProfile : Profile
{
    public M3uProfile()
    {
        CreateMap<M3uFile, M3uFileDto>();
        CreateMap<M3uFile, M3uWithoutFileEntriesDto>();
   }
}
