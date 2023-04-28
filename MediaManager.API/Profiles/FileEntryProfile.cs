using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;

namespace MediaManager.API.Profiles;

public class FileEntryProfile : Profile
{
    public FileEntryProfile()
    {
        CreateMap<FileEntry, FileEntryDto>().ReverseMap();
    }
}
