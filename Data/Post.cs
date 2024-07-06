using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbBlogger1.Data;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    // public string Author { get; set; } // Change to type representing user (e.g., string for username or User type)
    public DateTime PublishDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public List<string>? Tags { get; set; }
    // public List<Comment> Comments { get; set; }
    
    public int Likes { get; set; }
    public int Views { get; set; }
    public string Image { get; set; }
    
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public ApplicationUser User;

}
