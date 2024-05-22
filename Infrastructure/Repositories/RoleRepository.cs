using Dapper;
using Domain.Models;
using Infrastructure.DataAccess;
using Infrastructure.Interfaces;
using System.Data;

namespace Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ISqlDapperData _db;

    public RoleRepository(ISqlDapperData db)
    {
        _db = db;

    }

    //READ Roles
    public async Task<List<Role>> ReadRolesAsync()
    {
        var parameters = new DynamicParameters();

        //return all roles
        var roles = await _db.LoadSpData<Role, DynamicParameters>("dbo.hsp_ReadRoles", parameters);
        return roles.ToList();
    }

    public async Task<Role> ReadRoleByIdAsync(int roleId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("RoleId", roleId, DbType.Int32);

        var role = await _db.LoadSpData<Role, DynamicParameters>("dbo.hsp_ReadRoleById", parameters);
        return role.FirstOrDefault()!;
    }


}
