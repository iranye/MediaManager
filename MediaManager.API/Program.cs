using MediaManager.API.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

            //if (_env.IsProduction())
            //{
            //    services.AddApplicationDbContext(HerokuConnectingString.Get());
            //}
            //else
            //{
            //    services.AddApplicationDbContext(Configuration.GetConnectionString("MediaManagerConnectionString"));
            //}
            builder.Services.AddDbContext<MediaManagerContext>(dbContextOptions =>
                dbContextOptions.UseNpgsql(builder.Configuration.GetConnectionString("MediaManagerConnectionString"))
            );
            var app = builder.Build();

            // TODO: remove if not really needed
            // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
}