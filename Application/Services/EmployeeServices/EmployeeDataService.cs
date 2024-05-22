using Application.Interfaces.EmployeeServiceInterfaces;
using Application.Interfaces.RoleServiceInterfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;

namespace Application.Services.EmployeeServices;

public class EmployeeDataService : IEmployeeDataService
{
    public Employee? Employee { get; set; }
    public string? FullName { get; set; }

    public string? FirstName { get; set; }

    public string? RoleName { get; set; }

    public int? RoleId { get; set; }
    public int? Employeeid { get; set; }

    private Role? role;


    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IEmployeeService _employeeService;
    private readonly IRoleService _roleService;

    private bool isInitialized = false;



    public EmployeeDataService(AuthenticationStateProvider authenticationStateProvider,
        IEmployeeService employeeService,
        IRoleService roleService)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _employeeService = employeeService;
        _roleService = roleService;


    }


    public async Task EnsureInitialized()
    {
        if (!isInitialized)
        {
            await OnInitializedAsync();
            isInitialized = true;

        }
    }

    public async Task OnInitializedAsync()
    {
        Employee = await GetCurrentEmployeeName();

        await GetEmployeeRoleName();


        // Set the FullName based on the employee data
        if (Employee != null)
        {
            FullName = Employee.FirstName + " " + Employee.LastName;
            FirstName = Employee.FirstName;
            RoleId = Employee.RoleId;
            Employeeid = Employee.EmployeeId;



            if (role != null)
            {
                RoleName = role.RoleName;
            }


        }
        else
        {
            FullName = "Guest";
        }

    }

    //return the employee name and role & use UserIdHelper class to remove domain HALAMERICA\
    //MainHeader.razor uses this servive to change from HALAMERICA\UserId to Name
    public async Task<Employee?> GetCurrentEmployeeName()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authenticationState.User;

        var userId = UserIdHelperService.GetUserIdWithoutDomain(user.Identity?.Name!);
        return await _employeeService.ReadEmployeeName(userId);

    }


    //Show role name
    public async Task<string> GetEmployeeRoleName()
    {

        if (Employee != null)
        {
            // Retrieve the roleId from the employee object
            var roleId = Employee.RoleId;

            // Use the roleId to fetch the role
            role = await _roleService.ReadRoleById(roleId);

            if (role != null)
            {
                return role.RoleName!; // Assuming 'RoleName' is the property that holds the role name in the 'Role' class.
            }
        }

        return "Role Not Assigned"; // Provide a default role name or handle the case where the role isn't found.
    }

}
