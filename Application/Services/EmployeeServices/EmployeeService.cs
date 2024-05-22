using Application.Interfaces.EmployeeServiceInterfaces;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services.EmployeeServices;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    //CREATE employee
    public async Task<int> CreateEmployee(Employee employee, string userId)
    {
        // Call the repository method to add an employee
        return await _employeeRepository.CreateEmployeeAsync(employee, userId);
    }

    //Check user exists before adding, if in active re-activate
    public async Task<bool> CheckUserExists(string userId)
    {
        return await _employeeRepository.CheckUserExistsAsync(userId);
    }

    //READ Employee name
    public async Task<Employee> ReadEmployeeName(string? userId)
    {
        return await _employeeRepository.ReadEmployeeNameAsync(userId);

    }
    //READ Employees
    public async Task<IEnumerable<Employee>> ReadEmployees()
    {
        return await _employeeRepository.ReadEmployeesAsync();
    }
    //UPDATE employee Plant and Role

    public async Task<bool> UpdateEmployeeRole(int employeeid, int roleid)
    {
        // Call the repository method to add an employee
        return await _employeeRepository.UpdateEmployeeRoleAsync(employeeid, roleid);
    }

    //DELETE Employees - this only de-activates the employee
    public async Task<bool> DeleteEmployee(int employeeid)
    {
        return await _employeeRepository.DeleteEmployeeAsync(employeeid);
    }

    //
    public async Task<List<Personnel>> SearchPersonnel(string searchQuery)
    {
        return await _employeeRepository.SearchPersonnel(searchQuery);
    }

}
