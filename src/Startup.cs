using CQRSApplication.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CQRSApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CQRSDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("CQRSConnection"));
                options.EnableSensitiveDataLogging();
            });
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
