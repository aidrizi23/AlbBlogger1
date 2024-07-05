using AlbBlogger1.Data;
using AlbBlogger1.Repositories;

namespace AlbBlogger1.Services;

public class RoleService : IRoleService
{
    private readonly RoleRepository _roleRepository;
    public RoleService(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Task<ApplicationRole> GetRoleByUserIdAsync(string UserId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ApplicationRole>> GetAllAsync()
    {
        return await _roleRepository.GetAll();
    }


    public async Task CreateAsync(ApplicationRole entity)
    {
        await _roleRepository.Create(entity);
    }

    public async Task UpdateAsync(ApplicationRole entity)
    {
        await _roleRepository.Edit(entity);
    }

    public async Task DeleteAsync(string id)
    {
        var role = await _roleRepository.GetRoleByUserIdAsync(id);
        await _roleRepository.Delete(role);
    }
}
public interface IRoleService
{
    Task<ApplicationRole> GetRoleByUserIdAsync(string UserId);
    Task<IEnumerable<ApplicationRole>> GetAllAsync();
    Task CreateAsync(ApplicationRole entity);
    Task UpdateAsync(ApplicationRole entity);
    Task DeleteAsync(string id);


}
