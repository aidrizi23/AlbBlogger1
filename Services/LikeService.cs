using AlbBlogger1.Data;
using AlbBlogger1.Repositories;

namespace AlbBlogger1.Services;

public class LikeService : ILikeService
{
    private readonly LikeRepository _repository;

    public LikeService(LikeRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> LikePostByUserAndPostId(string userId, int postId)
    {
        return await _repository.LikePostByUserAndPostId(userId, postId);
    }

    public async Task<IEnumerable<Post>> GetLikedPostsByUserId(string userId)
    {
        return await _repository.GetLikedPostsByUserId(userId);
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersWhoLikedPostByPostId(int postId)
    {
        return await _repository.GetUsersWhoLikedPostByPostId(postId);
    }
    
    public async Task<int> GetLikeCountForPost(int postId)
    {
        return await _repository.GetLikeCountForPost(postId);
    }
}

public interface ILikeService
{
    Task<int> LikePostByUserAndPostId(string userId, int postId);
    Task<IEnumerable<Post>> GetLikedPostsByUserId(string userId);
    Task<IEnumerable<ApplicationUser>> GetUsersWhoLikedPostByPostId(int postId);
    Task<int> GetLikeCountForPost(int postId);
}