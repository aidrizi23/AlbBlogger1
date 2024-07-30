using System.ComponentModel.DataAnnotations.Schema;

namespace AlbBlogger1.Data;

public class Like : BaseEntity
{
    // Foreign key for the user who liked the post
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
        
    // Navigation property to the user who liked the post
    public virtual ApplicationUser? User { get; set; }
        
    // Foreign key for the post that was liked
    [ForeignKey("PostId")]
    public int PostId { get; set; }
        
    // Navigation property to the post that was liked
    public virtual Post Post { get; set; }
}