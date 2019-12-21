using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;

namespace HotelBooking
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //gotta figure out when we are in docker, local or dev env.
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            string conString;
            if (env == "Docker")
            {
                conString = ConfigurationExtensions.GetConnectionString(Configuration, "Docker");
            }
            else if (env == "Development")
            {
                conString = ConfigurationExtensions.GetConnectionString(Configuration, "Development");
            }
            else
            {
                conString = ConfigurationExtensions.GetConnectionString(Configuration, "Local");
            }
            
            services.AddScoped<IBookingRepo, BookingRepo>(service =>
            {
                return new BookingRepo(conString);
            });
            services.AddScoped<IBookingService, BookingService>(service =>
            {
                return new BookingService(service);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
