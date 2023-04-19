using AutoMapper;
using MediaManager.API.Data.Entities;
using MediaManager.API.Model;

namespace MediaManager.API.Profiles;

public class VolumeProfile:Profile
{
    public VolumeProfile()
    {
        CreateMap<Volume, VolumeDto>();
        CreateMap<Volume, VolumeWithoutM3us>();
        CreateMap<VolumeForUpsert, Volume>();       
    }
}
