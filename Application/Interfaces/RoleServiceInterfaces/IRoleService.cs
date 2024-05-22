using Domain.Models;
using System.Data;

namespace Application.Interfaces.RoleServiceInterfaces
{
    public interface IRoleService
    {
        Task<List<Role>> ReadRoles();

        Task<Role> ReadRoleById(int roleId);
    }
}