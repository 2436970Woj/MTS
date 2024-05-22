using Domain.Models;

namespace Application.Interfaces.EmployeeServiceInterfaces
{
    public interface IEmployeeService
    {
        Task<bool> CheckUserExists(string userId);
        Task<int> CreateEmployee(Employee employee, string userId);
        Task<Employee> ReadEmployeeName(string? userId);
        Task<IEnumerable<Employee>> ReadEmployees();
        Task<bool> DeleteEmployee(int employeeid);
        Task<bool> UpdateEmployeeRole(int employeeid, int roleid);
        Task<List<Personnel>> SearchPersonnel(string searchQuery);

    }
}
