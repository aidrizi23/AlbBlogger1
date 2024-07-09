using System.ComponentModel.DataAnnotations.Schema;

namespace AlbBlogger1.Data;

public class Like : BaseEntity
{
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
    
    [ForeignKey("PostId")]
    public int? PostId { get; set; }
    public virtual Post? Post { get; set; }
}