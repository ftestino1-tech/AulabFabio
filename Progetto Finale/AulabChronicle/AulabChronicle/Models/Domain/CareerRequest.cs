using Microsoft.AspNetCore.Identity; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; 

namespace AulabChronicle.Models.Domain; 

public class CareerRequest
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Body { get; set; } = string.Empty; 
    public bool IsChecked { get; set; }

    [ValidateNever]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public IdentityUser? User { get; set; }

    [Required]
    public string RoleId { get; set; } = string.Empty; 

    [ForeignKey("RoleId")]
    public IdentityRole? Role { get; set; }
}