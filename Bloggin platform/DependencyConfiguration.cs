using Bloggin_platform.Persistance.Context;
using Bloggin_platform.Persistance.Repositories;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Bloggin_platform.Services;
using Bloggin_platform.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection configureDependencies (this IServiceCollection services)
        {
            services.AddDbContext<BaseDbContext, MySqlDbContext>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
