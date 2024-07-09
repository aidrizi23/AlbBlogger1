using AlbBlogger1.Data;
using AlbBlogger1.Repositories;

namespace AlbBlogger1.Services;

public class LikeService : ILikeService
{
    private readonly LikeRepository _likeRepository;
    public LikeService(LikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

 

    public async Task<IEnumerable<Like>> GetAllAsync()
    {
        return await _likeRepository.GetAll();
    }


    public async Task CreateAsync(Like entity)
    {
        await _likeRepository.Create(entity);
    }

    public async Task UpdateAsync(Like entity)
    {
        await _likeRepository.Edit(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _likeRepository.GetById(id);
        await _likeRepository.Delete(role);
    }

    public async Task LikePostAsync(int postId, string userId)
    {
        await _likeRepository.LikePostAsync(postId, userId);
    }

    public async Task<int> GetLikesCountAsync(int postyId)
    {
        return await _likeRepository.GetLikesCountAsync(postyId);
    }

    public async Task<Like> GetByPostAndUserAsync(int postId, string userId)
    {
        return await _likeRepository.GetByPostAndUserAsync(postId, userId);
    }
}
public interface ILikeService
{
    Task<IEnumerable<Like>> GetAllAsync();
    Task CreateAsync(Like entity);
    Task UpdateAsync(Like entity);
    Task DeleteAsync(int id);
    Task LikePostAsync(int postId, string userId);
    Task<int> GetLikesCountAsync(int postyId);
    Task<Like> GetByPostAndUserAsync(int postId, string userId);

}