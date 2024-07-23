using System.ComponentModel.DataAnnotations.Schema;

namespace AlbBlogger1.Data;

public class Bookmark : BaseEntity
{
    // [ForeignKey("PostId")]
    // public int PostId { get; set; }
    // public virtual Post Post { get; set; }
    
    // [ForeignKey("UserId")]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    public IList<Post> BookmarkedPosts { get; set; }
    
    // public int Count { get; set; }
}