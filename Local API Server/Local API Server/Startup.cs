using Local_API_Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Local_API_Server
{
    public class Startup
    {
        public string dataStringConnection = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StorageLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));
            services.AddDbContext<DataLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));
            services.AddDbContext<CustomListLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));
            services.AddDbContext<ListOptionLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));
            services.AddDbContext<LogLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));
            services.AddDbContext<StorageLibraryXCustomListLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));
            services.AddDbContext<UserLibraryContext>(opt =>
                opt.UseSqlServer(dataStringConnection));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Local_API_Server", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Local_API_Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
