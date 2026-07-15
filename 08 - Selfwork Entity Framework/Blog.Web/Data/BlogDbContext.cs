using Microsoft.EntityFrameworkCore;
using Blog.Web.Models;
using Blog.Web.Models.Domain;

namespace Blog.Web.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options){}
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }

}
