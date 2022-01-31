using Local_API_Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Local_API_Server
{
    public class Startup
    {
        public string serverName = "localhost";
        public string userId = "root";
        public string password = "root";
        public bool persistsecurityinfo = true;
        public string databaseName = "project_inventory";
        public string dataStringConnection;

        public string databaseVersion = "V2";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            dataStringConnection = "server=" + serverName + ";" +
                                    "Uid=" + userId + ";" +
                                    "password=" + password + ";" +
                                    "persistsecurityinfo=" + persistsecurityinfo + ";" +
                                    "database=" + databaseName;

            services.AddDbContext<StorageLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<CustomListLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<DataLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<ListOptionLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<LogLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<StorageLibraryXCustomListLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<UserLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<BugLibraryContext>(opt =>
                opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<SaveContext>(Opt =>
                Opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));
            services.AddDbContext<VersionLibraryContext>(Opt =>
                Opt.UseMySql(dataStringConnection, ServerVersion.AutoDetect(dataStringConnection)));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = databaseName, Version = databaseVersion });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", databaseName));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void DataRestoration(MySqlCommand cmd)
        {
            cmd.CommandText = "";

            cmd.ExecuteNonQuery();
        }
    }
}
