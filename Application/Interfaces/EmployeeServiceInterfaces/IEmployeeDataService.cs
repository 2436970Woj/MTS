using Domain.Models;

namespace Application.Interfaces.EmployeeServiceInterfaces
{
    public interface IEmployeeDataService
    {
        int? Employeeid { get; set; }
        Employee? Employee { get; set; }
        string? FirstName { get; set; }
        string? FullName { get; set; }
        int? RoleId { get; set; }
        string? RoleName { get; set; }

        Task EnsureInitialized();
        Task<Employee?> GetCurrentEmployeeName();
        Task<string> GetEmployeeRoleName();
        Task OnInitializedAsync();
    }
}
