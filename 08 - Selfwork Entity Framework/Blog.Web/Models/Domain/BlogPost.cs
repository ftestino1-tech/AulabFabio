namespace Blog.Web.Models.Domain;

public class BlogPost
{
    public Guid Id { get; set; }
    public string Heading { get; set; } = string.Empty;
    public string PageTitle { get; set; } = String.Empty;   
    public string Content { get; set; } = String.Empty;
    public string ShortDescription { get; set; } = String.Empty;
    public string FeaturedImageUrl { get; set; } = String.Empty;
    public string UrlHandle { get; set; } = String.Empty;
    public DateTime PublishedDate { get; set; }
    public string Author { get; set; } = String.Empty;
    public bool Visible { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

}
