using Dapper;
using Infrastructure.DataAccess;
using System.Data;
using Domain.Models;
using Infrastructure.Interfaces;
using System.Data.SqlClient;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    //This repo contains all the Employee CRUD operations using Dapper and Stored Procedures

    private readonly ISqlDapperData _db;

    List<Employee>? employee;
    public EmployeeRepository(ISqlDapperData db)
    {
        _db = db;

    }

    //CREATE employee - this adds a new employee to the Employees table using UserId H******
    // This uses the generic SaveSpDate method
    public async Task<int> CreateEmployeeAsync(Employee employee, string userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId, DbType.String);
        parameters.Add("NewId", employee.EmployeeId, DbType.Int32, ParameterDirection.Output);
        await _db.SaveSpDate("dbo.hsp_CreateEmployee", parameters);
        return parameters.Get<int>("NewId");
    }

    //check if user exists before adding
    public async Task<bool> CheckUserExistsAsync(string? userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId, DbType.String);

        var result = await _db.LoadSpData<int, DynamicParameters>("dbo.hsp_CheckUserExists", parameters);

        // Check if the list contains the value 1 return true > user already exists
        return result.Any(r => r == 1);
    }

    public async Task<Employee> ReadEmployeeNameAsync(string? userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId, DbType.String);

        // Return employee name
        employee = await _db.LoadSpData<Employee, DynamicParameters>("dbo.hsp_ReadEmployeeName", parameters);
        return employee.FirstOrDefault()!;
    }


    //READ employees from Employees table using stored procedure
    public async Task<IEnumerable<Employee>> ReadEmployeesAsync()
    {
        IEnumerable<Employee> employees;

        var parameters = new DynamicParameters();

        //return all employees
        employees = await _db.LoadSpData<Employee, DynamicParameters>("hsp_ReadEmployees", parameters);
        return employees;
    }

    //UPDATE employee in Employees table using stored procedure (Role or Plant)
    public async Task<bool> UpdateEmployeeRoleAsync(int employeeid, int roleid)
    {
        var parameters = new DynamicParameters();
        parameters.Add("RoleId", roleid, DbType.Int32);
        parameters.Add("Employeeid", employeeid, DbType.Int32);
        await _db.SaveSpDate("dbo.hsp_UpdateEmployeeRole", parameters);
        return true;
    }



    //DELETE employee from Employees table
    // this stored procedure just changes IsActive to fales and doesn't remove the record
    public async Task<bool> DeleteEmployeeAsync(int employeeid)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Employeeid", employeeid, DbType.Int32);

        await _db.SaveSpDate("dbo.hsp_DeleteEmployee", parameters);

        return true;
    }

    public async Task<List<Personnel>> SearchPersonnel(string searchQuery)
    {
        var parameters = new DynamicParameters();
        parameters.Add("SearchQuery", searchQuery, DbType.String);

        // Call the stored procedure using Dapper
        var results = await _db.LoadSpData<Personnel, DynamicParameters>("hsp_SearchPersonnel", parameters);

        return results;
    }

}
