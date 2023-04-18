using MediaManager.API.Data.Entities;
using MediaManager.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.API.Data;

public class MediaManagerContext : DbContext
{
    public MediaManagerContext(DbContextOptions<MediaManagerContext> options)
        : base(options)
    { }

    public DbSet<Volume> Volumes => Set<Volume>();

    public DbSet<M3uFile> M3uFiles => Set<M3uFile>();

    public DbSet<FileEntry> FileEntries => Set<FileEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Volume>()
            .HasData(
                new Volume("KGON-01", "kgon-01")
                {
                    Id = 1
                },
                new Volume("Mellow-01", "mellow-01")
                {
                    Id = 2
                }
            );

        modelBuilder.Entity<M3uFile>()
            .HasData(
                new M3uFile("ShaNaNa.m3u")
                {
                    Id = 1,
                    VolumeId = 1
                },
                new M3uFile("WakeAndBake.m3u")
                {
                    Id = 2,
                    VolumeId = 1
                },
                new M3uFile("BravenHearts.m3u")
                {
                    Id = 3,
                    VolumeId = 2
                }
            );

        modelBuilder.Entity<FileEntry>()
            .HasData(
                new FileEntry("All of my love.mp3") { Id = 1, M3uId = 1 },
                new FileEntry("Rock and Roll All Nite.mp3") { Id = 2, M3uId = 1 },

                new FileEntry("Beat Box Extreme.mp3") { Id = 3, M3uId = 2 },

                new FileEntry("Lady in Red.mp3") { Id = 4, M3uId = 3 },
                new FileEntry("Back in the Saddle.mp3") { Id = 5, M3uId = 3 }
            );
    }
}
