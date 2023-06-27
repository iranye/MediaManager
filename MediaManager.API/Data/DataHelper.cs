using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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

    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetService<MediaManagerContext>();
        if (context == null)
        {
            return;
        }

        string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

        foreach (string role in roles)
        {
            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == role))
            {
                await roleStore.CreateAsync(new IdentityRole(role));
            }
        }
        await context.SaveChangesAsync();
    }
}
