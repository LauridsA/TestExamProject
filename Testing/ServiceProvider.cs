using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace Testing
{
    public class ServiceProvider
    {
        private readonly IWebHost _webHost;
        public ServiceProvider(IWebHost WebHost) => _webHost = WebHost;
        public T GetService<T>()
        {

            var serviceScope = _webHost.Services.CreateScope();
            var services = serviceScope.ServiceProvider;
            try
            {
                var scopedService = services.GetRequiredService<T>();
                return scopedService;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
