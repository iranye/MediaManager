using MediaManager.API.Data;
using MediaManager.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson()
          .AddXmlDataContractSerializerFormatters();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

        builder.Services.AddSingleton<VolumesDataStore>();

        builder.Services.AddDbContext<MediaManagerContext>(dbContextOptions =>
            dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("MediaManagerConnectionString"))
        );

        builder.Services.AddScoped<IMediaManagerRepository, MediaManagerRepository>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}