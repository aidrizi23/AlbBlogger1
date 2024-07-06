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

    public async Task LikePostByIdAsync(int id)
    {
        await _postRepository.LikePostByIdAsync(id);
    }
    public async Task ViewPostByIdAsync(int id)
    {
        await _postRepository.ViewPostByIdAsync(id);
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
     Task LikePostByIdAsync(int id);
     Task ViewPostByIdAsync(int id);


}