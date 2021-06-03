using Bloggin_platform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Context
{
    public class MySqlDbContext : BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=P@ssw0rd;database=bloggin_platform");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
