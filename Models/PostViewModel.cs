
using System.ComponentModel.DataAnnotations;

namespace AlbBlogger1.Models;

public class PostViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Title field is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The Content field is required.")]
    public string Content { get; set; }

    // public DateTime PublishDate { get; set; }
    // public DateTime LastEdited { get; set; }
    public List<string> Tags { get; set; }

    public int Likes { get; set; }

    public int Views { get; set; }

    public List<string>? Images { get; set; }
    // Additional properties or methods as needed

    public string UserId { get; set; }  // Example: If you need to display user-related information

    // Constructors if needed
    public PostViewModel()
    {
        Tags = new List<string>();
    }

    // Methods for additional functionality, if any
}