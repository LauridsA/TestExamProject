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
using Microsoft.Extensions.Hosting;
using Services;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddControllers();
            //gotta figure out when we are in docker, local or dev env.
            var env = Environment.GetEnvironmentVariable("ASPNETENVIRONMENT");

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

            //room
            services.AddScoped<IRoomRepo, RoomRepo>(service =>
            {
                return new RoomRepo(conString);
            });
            services.AddScoped<IRoomService, RoomService>(service =>
            {
                return new RoomService(service.GetService<IRoomRepo>());
            });
            //booking
            services.AddScoped<IBookingService, BookingService>(service =>
            {
                return new BookingService(service.GetService<IBookingRepo>(), service.GetService<IRoomRepo>());
            });
            services.AddScoped<IBookingRepo, BookingRepo>(service =>
            {
                return new BookingRepo(conString, service.GetService<IRoomRepo>());
            });


            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            //});

            services.AddMvc();
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

        }
    }
}
