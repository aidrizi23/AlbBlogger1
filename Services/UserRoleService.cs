using AlbBlogger1.Data;
using AlbBlogger1.Repositories;

namespace AlbBlogger1.Services;

public class UserRoleService : IUserRoleService
{
    private readonly UserRoleRepository _userRoleRepository;
    public UserRoleService(UserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task<IEnumerable<ApplicationUserRole>> GetAllAsync()
    {
        return await _userRoleRepository.GetAll();
    }

    public async Task CreateAsync(ApplicationUserRole entity)
    {
        await _userRoleRepository.Create(entity);
    }

    public async Task UpdateAsync(ApplicationUserRole entity)
    {
        await _userRoleRepository.Edit(entity);
    }

    public async Task DeleteAsync(ApplicationUserRole entity)
    {
           
        await _userRoleRepository.Delete(entity);
    }

    public async Task<ApplicationUserRole> GetUserRoleByRoleIdAsync(string id)
    {
        return await _userRoleRepository.GetUserRoleByRoleIdAsync(id);
    }
    public async Task<ApplicationUserRole> GetUserRoleByUserIdAsync(string id)
    {
        return await _userRoleRepository.GetUserRoleByUserIdAsync(id);
    }

}
public interface IUserRoleService
{

    Task<IEnumerable<ApplicationUserRole>> GetAllAsync();
    Task CreateAsync(ApplicationUserRole entity);
    Task UpdateAsync(ApplicationUserRole entity);
    Task DeleteAsync(ApplicationUserRole entity);
    Task<ApplicationUserRole> GetUserRoleByRoleIdAsync(string id);
    Task<ApplicationUserRole> GetUserRoleByUserIdAsync(string id);
}