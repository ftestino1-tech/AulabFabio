namespace Blog.Web.Repositories
{
    public interface IImageRepository
    {
        string? Upload(IFormFile file); 
    }    
}