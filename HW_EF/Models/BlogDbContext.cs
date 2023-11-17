using Microsoft.EntityFrameworkCore;

namespace HW_EF.Models
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostsTags> PostsTags { get; set; }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
