using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;

namespace MediaManager.API.Profiles;

public class M3uProfile : Profile
{
    public M3uProfile()
    {
        CreateMap<M3uFile, M3uFileDto>()
                .ForMember(c => c.VolumeMoniker, o => o.MapFrom(m => m.Volume == null ? "" : m.Volume.Moniker));
        CreateMap<M3uFile, M3uWithoutFileEntriesDto>()
                .ForMember(c => c.VolumeMoniker, o => o.MapFrom(m => m.Volume == null ? "" : m.Volume.Moniker));
        CreateMap<M3uFileDtoForUpsert, M3uFile>();
    }
}
