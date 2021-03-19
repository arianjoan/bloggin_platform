using Bloggin_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloggin_platform.Persistance.Context
{
    public class SqlDbContext : BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESARROLLO67;Initial Catalog=Bloggin_Platform;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>().Property(p => p.Role).HasConversion<string>();

            builder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
