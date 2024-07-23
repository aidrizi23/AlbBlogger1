using AlbBlogger1.Data;
using AlbBlogger1.Repositories;
using AlbBlogger1.Repositories.Pagination;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;

namespace AlbBlogger1.Services;

public class PostService : IPostService
{
    private readonly PostRepository _postRepository;

    public PostService(PostRepository postRepository) 
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _postRepository.GetAll();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _postRepository.GetById(id);
    }

    public async Task CreatePostAsync(Post post)
    {
        await _postRepository.Create(post);
    }

    public async Task DeletePostAsync(Post post)
    {
        await _postRepository.Delete(post);
    }

    public async Task EditPostAsync(Post post)
    {
        await _postRepository.Edit(post);
    }
    
    
    // the posts special methods
    public async Task<PaginatedList<Post>> GetAllPaginatedPostsByUserId(string userId)
    {
        return await _postRepository.GetAllPaginatedPostsByUserId(userId);
    }

   
    public async Task ViewPostByIdAsync(int id)
    {
        await _postRepository.ViewPostByIdAsync(id);
    }

    public async Task<PaginatedList<Post>> GetAllPaginatedPostsAsync(int pageIndex = 1, int pageSize = 10)
    {
        return await _postRepository.GetAllPaginatedPostAsync(pageIndex, pageSize);
    }
    
    // ------------------------------- Filters ---------------------------
    // public async Task<PaginatedList<Post>> GetPaginatedPostsByHighestLikes(int pageIndex = 1, int pageSize = 10)
    // {
    //     return await _postRepository.GetPaginatedPostsByHighestLikes(pageIndex, pageSize);
    // } 
    // public async Task<PaginatedList<Post>> GetPaginatedPostsByLowestLikes(int pageIndex = 1, int pageSize = 10)
    // {
    //     return await _postRepository.GetPaginatedPostsByLowestLikes(pageIndex, pageSize);
    // }
    public async Task<PaginatedList<Post>> GetPaginatedPostsByHighestViews(int pageIndex = 1, int pageSize = 10)
    {
        return await _postRepository.GetPaginatedPostsByHighestViews(pageIndex, pageSize);
    }    
    public async Task<PaginatedList<Post>> GetPaginatedPostsByLowestViews(int pageIndex = 1, int pageSize = 10)
    {
        return await _postRepository.GetPaginatedPostsByLowestViews(pageIndex, pageSize);
    }
    
    
}


public interface IPostService
{
    Task<IEnumerable<Post>> GetAllPostsAsync();
     Task<Post> GetPostByIdAsync(int id);
     Task CreatePostAsync(Post post);
     Task DeletePostAsync(Post post);
     Task EditPostAsync(Post post);
     Task<PaginatedList<Post>> GetAllPaginatedPostsByUserId(string userId);
     Task ViewPostByIdAsync(int id);
     Task<PaginatedList<Post>> GetAllPaginatedPostsAsync(int pageIndex = 1, int pageSize = 10);
     // Task<PaginatedList<Post>> GetPaginatedPostsByHighestLikes(int pageIndex = 1, int pageSize = 10);
     // Task<PaginatedList<Post>> GetPaginatedPostsByLowestLikes(int pageIndex = 1, int pageSize = 10);
     Task<PaginatedList<Post>> GetPaginatedPostsByHighestViews(int pageIndex = 1, int pageSize = 10);
     Task<PaginatedList<Post>> GetPaginatedPostsByLowestViews(int pageIndex = 1, int pageSize = 10);

}