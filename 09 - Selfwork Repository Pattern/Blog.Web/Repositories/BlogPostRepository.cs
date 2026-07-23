using System.Runtime.CompilerServices;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _blogDbContext; 

        public BlogPostRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public BlogPost Add(BlogPost blogPost)
        {
            _blogDbContext.BlogPosts.Add(blogPost);
            _blogDbContext.SaveChanges();
            return blogPost;
        }

        public BlogPost? Delete(Guid id)
        {
            var existing = _blogDbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == id); 

            if (existing != null)
            {
                _blogDbContext.BlogPosts.Remove(existing); 
                _blogDbContext.SaveChanges(); 
                return existing; 
            }

            return null; 

        }

        public List<BlogPost> GetAll()
        {
            return _blogDbContext.BlogPosts
                    .Include(x => x.Tags)
                    .ToList(); 
        }

        public BlogPost? GetById(Guid id)
        {
            return _blogDbContext.BlogPosts
                    .Include(x => x.Tags)
                    .FirstOrDefault(x => x.Id == id); 
        }

        public BlogPost? Update(BlogPost blogPost)
        {
            var existing = _blogDbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == blogPost.Id);

            if(existing != null)
            {
                existing.Heading = blogPost.Heading; 
                existing.PageTitle = blogPost.PageTitle; 
                existing.Content = blogPost.Content; 
                existing.ShortDescription = blogPost.ShortDescription;
                existing.FeaturedImageUrl = blogPost.FeaturedImageUrl; 
                existing.UrlHandle = blogPost.UrlHandle; 
                existing.PublishedDate = blogPost.PublishedDate; 
                existing.Author = blogPost.Author;
                existing.Visible = blogPost.Visible; 

                existing.Tags = blogPost.Tags;

                _blogDbContext.SaveChanges();
                return existing;
            }

            return null; 
            
        }
    }
}