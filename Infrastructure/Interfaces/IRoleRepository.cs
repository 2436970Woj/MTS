using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> ReadRolesAsync();

        Task<Role> ReadRoleByIdAsync(int roleId);
    }
}
