namespace Blog.Web.Repositories;
using Blog.Web.Models.Domain; 

public interface ITagRepository
{
    List<Tag> GetAll();
    Tag GetByID(Guid id); 
    Tag Add(Tag tag);
    Tag Update(Tag tag);    
    Tag Delete(Guid id);
}
