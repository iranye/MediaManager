using MediaManager.API.Data;
using MediaManager.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.API.Services;

public interface IMediaManagerRepository
{
    Task<IEnumerable<Volume>> GetVolumesAsync();

    Task<Volume?> GetVolumeAsync(string moniker, bool includeM3us);
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

    public async Task<Volume?> GetVolumeAsync(string moniker, bool includeM3us)
    {
        if (includeM3us)
        {
            return await context.Volumes.Include(v => v.M3uFiles)
                .Where(v => v.Moniker == moniker).FirstOrDefaultAsync();
        }
        return await context.Volumes
            .Where(v => v.Moniker == moniker).FirstOrDefaultAsync();
    }
}
