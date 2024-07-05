using AlbBlogger1.Areas;
using AlbBlogger1.Data;
using AlbBlogger1.Repositories.Pagination;

namespace AlbBlogger1.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PaginatedList<ApplicationUser>> GetPaginatedUsersAsync(int page = 1, int pagesize = 10)
    {
        return await _userRepository.GetPaginatedUsersAsync(page, pagesize);
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }
    public async Task<IEnumerable<ApplicationUser>> FindAllAsync()
    {
        return await _userRepository.FindAllAsync();
    }
    public async Task UpdateUserAsync(ApplicationUser user)
    {
        await _userRepository.UpdateUserAsync(user);
    }
    public async Task DeleteUserAsync(ApplicationUser user)
    {
        await _userRepository.DeleteUserAsync(user);
    }
    public async Task SaveChangesAsync()
    {
        await _userRepository.SaveChangesAsync();
    }
}

public interface IUserService
{
    Task<PaginatedList<ApplicationUser>> GetPaginatedUsersAsync(int page = 1, int pagesize = 10);

    Task<ApplicationUser> GetUserByIdAsync(string id);

    Task<IEnumerable<ApplicationUser>> FindAllAsync();

    Task UpdateUserAsync(ApplicationUser user);

    Task DeleteUserAsync(ApplicationUser user);

    Task SaveChangesAsync();
}