using AulabChronicle.Models.Domain;

namespace AulabChronicle.Repositories;

public interface IArticleImageRepository
{
    Task<Image> AddAsync(Image image);
    Task<Image?> DeleteByPathAsync(string path);

}