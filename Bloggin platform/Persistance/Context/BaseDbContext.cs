using Bloggin_platform.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Context
{
    public class BaseDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public BaseDbContext() { }

        public BaseDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries()
               .Where(i => i.State == EntityState.Deleted &&
               i.Metadata.GetProperties().Any(x => x.Name == "IsDeleted")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
