using AulabChronicle.Models.Domain;
using AulabChronicle.Repositories;
using System.Net.Http.Headers;

namespace AulabChronicle.Services;

public class SupabaseImageService : IImageService
{
    private readonly IConfiguration configuration;
    private readonly IArticleImageRepository imageRepository;
    private readonly HttpClient httpClient;
    
    public SupabaseImageService(IConfiguration configuration, IArticleImageRepository imageRepository)
    {
        this.configuration = configuration;
        this.imageRepository = imageRepository;
        this.httpClient = new HttpClient();
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var supabaseUrl = configuration["Supabase:Url"];
        var supabaseKey = configuration["Supabase:Key"];
        var supabaseBucket = configuration["Supabase:Bucket"];

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var uploadUrl = $"{supabaseUrl}/{supabaseBucket}{fileName}";

        using var content = new StreamContent(file.OpenReadStream());
        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", supabaseKey);
        httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
        
        var response = await httpClient.PostAsync(uploadUrl, content);

        if (response.IsSuccessStatusCode)
        {
            return uploadUrl;
        }
        throw new Exception($"Failed to upload image: {response.ReasonPhrase}");
    }

    public async Task SaveToDbAsync(string url, long articleId)
    {
        var publicUrlPrefix = configuration["Supabase:PublicUrl"];
        var bucketPrefix = configuration["Supabase:Bucket"];

        var publicUrl = url.Replace(bucketPrefix, publicUrlPrefix);
        
        await imageRepository.AddAsync(new Image
        {
            Path = publicUrl,
            ArticleId = articleId
        });
    }

    public async Task DeleteAsync(string path)
    {
        var supabaseKey = configuration["Supabase:Key"];
        var publicUrlPrefix = configuration["Supabase:PublicUrl"];
        var bucketPrefix = configuration["Supabase:Bucket"];

        var deleteUrl = path.Replace(publicUrlPrefix, bucketPrefix);

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", supabaseKey);
        httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);

        var response = await httpClient.DeleteAsync(deleteUrl);

        if (response.IsSuccessStatusCode)
        {
            await imageRepository.DeleteByPathAsync(path);
        }
    }
}