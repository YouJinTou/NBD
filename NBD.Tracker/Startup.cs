﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBD.SDK;
using NBD.Tracker.Configuration;
using NBD.Tracker.DAL;

namespace NBD.Tracker
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(builder => builder.AddPolicy("UIPolicy", p =>
            {
                p.WithOrigins(Configuration.GetSection("allowedOrigins").Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddDbContext<TrackerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TrackerDb")));
            services.AddScoped<DbContext, TrackerContext>();
            services.AddScoped<IRepository<Goal>, Repository<Goal>>();
            services.AddScoped<IRepository<GoalTree>, Repository<GoalTree>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            MapperConfig.RegisterMappings();

            app.UseMvc();

            app.UseCors("UIPolicy");

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(string.Empty);
            });
        }
    }
}
