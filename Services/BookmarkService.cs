using AlbBlogger1.Data;
using AlbBlogger1.Repositories;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace AlbBlogger1.Services;

public class BookmarkService : IBookmarkService
{
    private readonly BookmarkRepository _repository;

    public BookmarkService(BookmarkRepository repository)
    {
        _repository = repository;
    }

    public async Task<Bookmark> GetBookmarkByUserId(string userId)
    {
        return await _repository.GetBookmarkByUserId(userId);
    }

    public async Task BookmarkPostbyPostId(string userId, int postId)
    {
        await _repository.BookmarkPostbyPostId(userId, postId);
    }

    public async Task<List<Post>> GetBookmarkedPostsByUserIdAsync(string userId)
    {
        return await _repository.GetBookmarkedPostsByUserIdAsync(userId);
    }
}

public interface IBookmarkService
{
    Task<Bookmark> GetBookmarkByUserId(string userId);
    Task BookmarkPostbyPostId(string userId, int postId);
    Task<List<Post>> GetBookmarkedPostsByUserIdAsync(string userId);
}