using Microsoft.AspNetCore.Identity;

namespace AlbBlogger1.Data;

public class ApplicationUser : IdentityUser<string>
{
    public ICollection<Post>? Posts { get; set; }
    
    public ICollection<Bookmark> Bookmarks { get; set; }
    
}