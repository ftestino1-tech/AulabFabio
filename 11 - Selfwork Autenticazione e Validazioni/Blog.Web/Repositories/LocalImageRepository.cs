namespace Blog.Web.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _env;

        public LocalImageRepository(IWebHostEnvironment env)
        {
            _env = env; 
        }

        public string? Upload(IFormFile file)
        {
            if (file == null || file.Length == 0) 
                return null; 

            var allowedExtensions = new[] {".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return null; 


            var uniqueName = $"{Guid.NewGuid():N}{extension}";

            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads"); 
            Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, uniqueName); 

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/uploads/{uniqueName}";
        }
    }

}