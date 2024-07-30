namespace AlbBlogger1.Data;

public class Reply
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public int? PostId { get; set; }
    public Post? Post { get; set; }
}