using MediaManager.API.Model;

namespace MediaManager.API.Data
{
    public class VolumesDataStore
    {
        public List<VolumeDto> Volumes { get; set; }

        public VolumesDataStore()
        {
            var songList = new List<FileEntryDto>()
            {
                new FileEntryDto() { Id = 1, Name="All of my love.mp3" },
                new FileEntryDto() { Id = 2, Name="Beat Box Extreme.mp3" },
                new FileEntryDto() { Id = 3, Name= "Lady in Red.mp3" }
            };
            Volumes = new List<VolumeDto>()
            {
                new VolumeDto()
                {
                    Id=1,
                    Title="KGON01",
                    Moniker="kgon01",
                    Created = DateTime.Now,
                    M3uFiles = new List<M3uFileDto>()
                    {
                        new M3uFileDto()
                        {
                            Id=1,
                            Title="ShaNaNa.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        },
                        new M3uFileDto()
                        {
                            Id=4,
                            Title="WakeAndBake.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        }
                    }
                },
                new VolumeDto()
                {
                    Id=2,
                    Title="KGON02",
                    Moniker="kgon02",
                    Created = DateTime.Now,
                    M3uFiles = new List<M3uFileDto>()
                    {
                        new M3uFileDto()
                        {
                            Id=2,
                            Title="KillerBs.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        },
                        new M3uFileDto()
                        {
                            Id=5,
                            Title="Moonlight.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        }
                    }
                },
                new VolumeDto()
                {
                    Id=3,
                    Title="Mellow01",
                    Moniker="mellow01",
                    Created = DateTime.Now,
                    M3uFiles = new List<M3uFileDto>()
                    {
                        new M3uFileDto()
                        {
                            Id=3,
                            Title="DontFallAsleep.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        },
                        new M3uFileDto()
                        {
                            Id=6,
                            Title="FeelinGood.m3u",
                            Created=DateTime.Now,
                            FilesInM3U = songList
                        }
                    }
                },
            };
        }

    }
}
