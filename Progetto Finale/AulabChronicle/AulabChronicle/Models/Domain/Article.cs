using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AulabChronicle.Models.Domain;

public class Article
{
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Subtitle { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(1000)]
    public string Body { get; set; } = string.Empty;

    public DateTime? PublishDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ValidateNever]
    public string? UserId { get; set; }

    [ForeignKey("UserId")]
    [ValidateNever]
    public IdentityUser? User { get; set; }

    [Required(ErrorMessage = "La categoria è obbligatoria")]
    [Range(1, long.MaxValue, ErrorMessage = "Seleziona una categoria valida")]
    public long? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category? Category { get; set; }

    [ValidateNever]
    public Image? Image { get; set; }

    public bool? IsAccepted { get; set; }

}