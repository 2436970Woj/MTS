using Application.Interfaces.RoleServiceInterfaces;

namespace Application.Services.RoleServices;

//Service is contacted by Index component when the role dropdown option is changed
public class RoleChangedService : IRoleChangedService
{
    private bool isRoleChanged;
    public event Action? OnChange;
    public bool IsRoleChanged
    {
        get { return isRoleChanged; }
        set
        {
            if (isRoleChanged != value)
            {
                isRoleChanged = value;
                NotifyStateChanged();

            }
        }
    }
    private void NotifyStateChanged() => OnChange?.Invoke();

}
