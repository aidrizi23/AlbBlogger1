using System.ComponentModel.DataAnnotations;

namespace AlbBlogger1.Data;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
}