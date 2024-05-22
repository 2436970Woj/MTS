namespace Application.Interfaces.RoleServiceInterfaces
{
    public interface IRoleChangedService
    {
        bool IsRoleChanged { get; set; }

        event Action? OnChange;
    }
}