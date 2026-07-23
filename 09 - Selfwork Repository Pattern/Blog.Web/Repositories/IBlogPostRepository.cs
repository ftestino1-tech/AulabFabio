using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostRepository
    {
        List<BlogPost> GetAll();
        BlogPost? GetById(Guid id);
        BlogPost Add(BlogPost blogPost); 
        BlogPost? Update(BlogPost blogPost); 
        BlogPost? Delete(Guid id); 
    }
}