using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResultsMicroservice.Database;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace ResultsMicroservice
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

            var dbType = Configuration["ResultsDb:DbType"]?.ToLower();

            var connectionString = Configuration["ResultsDb:ConnectionString"];

            ConfigureDatabase(services, dbType, connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            DbContextOptions<ResultsDbContext> dbOptions)
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
                $"/{virtualDirectory}" : string.Empty) +"/swagger/v1/swagger.json";

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerUrl, "My API V1");
            });

            using (var db = new ResultsDbContext(dbOptions))
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
                    services.AddDbContext<ResultsDbContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
                case "postgres":
                case "postgresql":
                    services.AddDbContext<ResultsDbContext>(options =>
                        options.UseNpgsql(connectionString));
                    break;
            }
        }
    }
}