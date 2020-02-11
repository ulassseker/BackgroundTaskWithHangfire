using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundTaskWithHangfire.Job;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BackgroundTaskWithHangfire.Data;
using BackgroundTaskWithHangfire.Models;
using Microsoft.Extensions.Options;
using BackgroundTaskWithHangfire.Services;

namespace BackgroundTaskWithHangfire
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
            services.AddControllers();
            //adding hangfire configurations
            services.AddHangfire(_ => _.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDbConn")));

            services.AddDbContext<BackgroundTaskWithHangfireContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("HangfireDbConn")));
            //adding mongodb conigurations
            services.Configure<InformationMongoSettings>(
        Configuration.GetSection(nameof(InformationMongoSettings)));

            services.AddSingleton<IInformationMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<InformationMongoSettings>>().Value);

            services.AddSingleton<InformationMongoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //hangfire stuff
            app.UseHangfireDashboard();
            app.UseHangfireServer();
           
        }
    }
}
