using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RequestsMicroservice.Database;
using RequestsMicroservice.MessageBroker;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace RequestsMicroservice
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Requests API", Version = "v1" });
            });

            var dbType = Configuration["RequestsDb:DbType"]?.ToLower();
            
            var connectionString = Configuration["RequestsDb:ConnectionString"];

            ConfigureDatabase(services, dbType, connectionString);

            var connectionSettings = Configuration.GetConnectionSettings();

            services
                .AddSingleton(connectionSettings)
                .AddSingleton<IMessagingService, MessagingService>();
        }
        
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            DbContextOptions<RequestsDbContext> dbOptions)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseMvc();

            var virtualDirectory = Configuration["Hosting:VirtualDirectory"];

            var swaggerUrl = (!string.IsNullOrWhiteSpace(virtualDirectory) ?
                $"/{virtualDirectory}" : string.Empty) + "/swagger/v1/swagger.json";

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerUrl, "My API V1");
            });

            using (var db = new RequestsDbContext(dbOptions))
            {
                db.Database.Migrate();
            }
        }

        protected void ConfigureDatabase(IServiceCollection services, string dbType, string connectionString)
        {
            Console.WriteLine($"Db Type: {dbType}");
            
            Console.WriteLine($"Connection String: '{connectionString}'");

            switch (dbType)
            {
                case "sqlite":
                    services.AddDbContext<RequestsDbContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
                case "postgres":
                case "postgresql":
                    services.AddDbContext<RequestsDbContext>(options =>
                        options.UseNpgsql(connectionString));
                    break;
            }
        }
    }
}