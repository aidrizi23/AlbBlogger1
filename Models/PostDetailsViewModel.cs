using AlbBlogger1.Data;

namespace AlbBlogger1.Models;

public class PostDetailsViewModel
{
    public Post Post { get; set; }
    public IEnumerable<Reply> Replies { get; set; }
}