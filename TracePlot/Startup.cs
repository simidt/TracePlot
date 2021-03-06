using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using TracePlot.Data;

namespace TracePlot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // services.AddDbContext<TraceRouteDbContext>(options => options.UseSqlite("Data Source=traceroutes2.db"));
            switch (Configuration["DatabaseProvider"])
            {
                case "postgres":
                    services.AddDbContext<TraceRouteDbContext>(options => options.UseNpgsql(Configuration["DatabaseConnection"]));
                    break;
                case "sqlite":
                    services.AddDbContext<TraceRouteDbContext>(options => options.UseSqlite(Configuration["DatabaseConnection"]));
                    break;
                default:
                    Console.WriteLine("No database provider specified. Defaulting to SQLite using the file \"traceroutes.db\" ");
                    services.AddDbContext<TraceRouteDbContext>(options => options.UseSqlite("Data Source=traceroutes.db"));
                    break;
            }
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "frontend/build");

            //Taken from the ASP.NET Core integration part of the Quartz documentation (https://www.quartz-scheduler.net/documentation/quartz-3.x/packages/aspnet-core-integration.html)
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("TraceRouteJob");
            });

            services.AddQuartzHostedService(
                    q => q.WaitForJobsToComplete = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TraceRouteDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            context.Database.Migrate();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "frontend";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
