using Microsoft.EntityFrameworkCore;
using Bloggin_platform.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using User = Bloggin_platform.Models.User;

namespace Bloggin_platform.Persistance.Context
{
    public class AppDbContext : BaseDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Name).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();
            builder.Entity<User>().Property(p => p.IsDeleted).IsRequired();
            builder.Entity<User>().HasMany(p => p.Posts).WithOne(p => p.Author).HasForeignKey(p => p.AuthorId);

            builder.Entity<User>().HasData
            (
               new User 
               {
                   Id = 100,
                   Name = "Jhon",
                   LastName = "Seena" 
               }   
            );

            builder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Post>().HasKey(p => p.Id);
            builder.Entity<Post>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.Entity<Post>().Property(p => p.Text).IsRequired();

            builder.Entity<Post>().HasData
            (
                new Post
                {
                    Id = 200,
                    AuthorId = 100,
                    Title = "The new API",
                    Text = "This is the"
                }
            );
        }
    }
}
