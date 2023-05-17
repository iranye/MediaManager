using Microsoft.EntityFrameworkCore;

namespace MediaManager.API.Data;

public static class DataHelper
{
    public static async Task ManageDataAsync(IServiceProvider svcProvider)
    {
        var dbContextSvc = svcProvider.GetRequiredService<MediaManagerContext>();

        // Migration: the programmatic equivalent to Update-Database
        await dbContextSvc.Database.MigrateAsync();
    }
}
