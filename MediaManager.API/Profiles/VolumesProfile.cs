using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;

namespace MediaManager.API.Profiles;

public class VolumesProfile:Profile
{
    public VolumesProfile()
    {
        CreateMap<Volume, VolumeDto>();
        CreateMap<Volume, VolumeWithoutM3us>();
    }
}
