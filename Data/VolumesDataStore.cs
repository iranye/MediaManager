using MediaManager.API.Model;

namespace MediaManager.API.Data
{
    public class VolumesDataStore
    {
        public List<VolumeDto> Volumes { get; set; }
        public static VolumesDataStore Current { get; } = new VolumesDataStore();

        public VolumesDataStore()
        {
            var songList = new List<FileEntryDto>()
            {
                new FileEntryDto() { Id = 1, Title="All of my love.mp3" },
                new FileEntryDto() { Id = 2, Title="Beat Box Extreme.mp3" },
                new FileEntryDto() { Id = 3, Title= "Lady in Red.mp3" }
            };
            Volumes = new List<VolumeDto>()
            {
                new VolumeDto()
                {
                    Id=1,
                    Title="KGON-01",
                    Moniker="kgon-01",
                    Created = DateTime.Now,
                    M3uFiles = new List<M3uFileDto>()
                    {
                        new M3uFileDto()
                        {
                            Id=1,
                            Name="ShaNaNa.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        },
                        new M3uFileDto()
                        {
                            Id=4,
                            Name="WakeAndBake.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        }
                    }
                },
                new VolumeDto()
                {
                    Id=2,
                    Title="KGON-02",
                    Moniker="kgon-02",
                    Created = DateTime.Now,
                    M3uFiles = new List<M3uFileDto>()
                    {
                        new M3uFileDto()
                        {
                            Id=2,
                            Name="KillerBs.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        },
                        new M3uFileDto()
                        {
                            Id=5,
                            Name="Moonlight.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        }
                    }
                },
                new VolumeDto()
                {
                    Id=2,
                    Title="Mellow-01",
                    Moniker="mellow-01",
                    Created = DateTime.Now,
                    M3uFiles = new List<M3uFileDto>()
                    {
                        new M3uFileDto()
                        {
                            Id=3,
                            Name="DontFallAsleep.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        },
                        new M3uFileDto()
                        {
                            Id=6,
                            Name="FeelinGood.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        }
                    }
                },
            };
        }

    }
}
