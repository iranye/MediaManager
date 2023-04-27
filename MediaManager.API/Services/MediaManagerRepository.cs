using MediaManager.API.Data;
using MediaManager.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.API.Services;

public interface IMediaManagerRepository
{
    Task<IEnumerable<Volume>> GetVolumesAsync();

    Task<Volume?> GetVolumeAsync(string moniker, bool includeM3us = false);

    Task<bool> VolumeExistsAsync(string moniker);

    void AddVolume(Volume volume);

    Task<IEnumerable<M3uFile>?> GetM3usByVolumeAsync(string moniker, bool includeFileEntries = false);

    Task<bool> SaveChangesAsync();
}

public class MediaManagerRepository : IMediaManagerRepository
{
    private readonly MediaManagerContext context;

    public MediaManagerRepository(MediaManagerContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Volume>> GetVolumesAsync()
    {
        return await context.Volumes.OrderBy(c => c.Title).ToListAsync();
    }

    public async Task<Volume?> GetVolumeAsync(string moniker, bool includeM3us = false)
    {
        if (includeM3us)
        {
            return await context.Volumes.Include(v => v.M3uFiles)
                .Where(v => v.Moniker == moniker).FirstOrDefaultAsync();
        }
        return await context.Volumes
            .Where(v => v.Moniker == moniker).FirstOrDefaultAsync();
    }

    public async Task<bool> VolumeExistsAsync(string moniker)
    {
        return await context.Volumes.AnyAsync(v => v.Moniker == moniker);
    }

    public async Task<IEnumerable<M3uFile>?> GetM3usByVolumeAsync(string moniker, bool includeFileEntries = false)
    {
        IQueryable<M3uFile> query = context.M3uFiles;

        if (includeFileEntries)
        {
            query = query
                .Include(m => m.FilesInM3U);
        }

        query = query
            .Where(m => m.Volume != null && m.Volume.Moniker == moniker);

        return await query.ToArrayAsync();
    }

    public async Task<IEnumerable<M3uFile>?> GetM3usAsync(int volumeId)
    {
        return await context.M3uFiles.Include(m=>m.FilesInM3U)
           .Where(m => m.VolumeId == volumeId)
           .ToListAsync();
    }

    //public async Task<M3uFile> GetM3uAsync(int volumeId, int m3uId, bool includeFileEntries = false)
    //{
    //    var volume = await GetVolumeAsync(moniker);

    //    return await context.M3uFiles
    //       .Where(m => m.VolumeId == volume.Id && m.Id == m3uId)
    //       .ToListAsync();
    //}

    public void AddVolume(Volume volume)
    {
        context.Volumes.Add(volume);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await context.SaveChangesAsync() >= 0);
    }
}
