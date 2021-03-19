using Bloggin_platform.Persistance.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // var host = CreateHostBuilder(args).Build();
            CreateHostBuilder(args).Build().Run();
            //using (var scope = host.Services.CreateScope())
            //using (var context = scope.ServiceProvider.GetService<BaseDbContext>())
            //{
            //    context.Database.EnsureCreated();
            //}
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
