using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace AulabChronicle.Models.Domain;

public class Image
{
    public int Id { get; set; }

    [Required]
    public string Path { get; set; } = string.Empty;

    public long ArticleId { get; set; }

    [ForeignKey("ArticleId")]
    public Article? Article { get; set; }
}
