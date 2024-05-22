using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IEmployeeRepository
{
    //CREATE
    Task<int> CreateEmployeeAsync(Employee employee, string userId);

    //Check if user exists before adding to Employees table to prevent duplicates
    Task<bool> CheckUserExistsAsync(string? userId);

    //READ
    Task<IEnumerable<Employee>> ReadEmployeesAsync();

    //READ Employee name
    Task<Employee> ReadEmployeeNameAsync(string? userId);

    //UPDATE
    Task<bool> UpdateEmployeeRoleAsync(int employeeid, int roleid);

    //DELETE
    Task<bool> DeleteEmployeeAsync(int employeeid);

    //Search Personnel table
    Task<List<Personnel>> SearchPersonnel(string searchQuery);


}
