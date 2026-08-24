namespace AulabChronicle.Models.ViewModels; 

public class CategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int NumberOfArticles { get; set; }
}