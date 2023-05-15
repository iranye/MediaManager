using MediaManager.API.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MediaManager.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IWebHostEnvironment env = builder.Environment;
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

            builder.Services.AddDbContext<MediaManagerContext>(dbContextOptions =>
                dbContextOptions.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration))
            );

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            await DataHelper.ManageDataAsync(scope.ServiceProvider);

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