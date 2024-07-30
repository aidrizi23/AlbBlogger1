using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity;

namespace AlbBlogger1.Data;

public class ApplicationUser : IdentityUser<string>
{
    public ICollection<Post>? Posts { get; set; }
    
    public ICollection<Bookmark> Bookmarks { get; set; }
    
    public ICollection<Like> Likes { get; set; }
    
    
    
    public string? ProfilePicture { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CustomUserName { get; set; } 
    
}