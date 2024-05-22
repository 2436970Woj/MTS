namespace Domain.Models
{
    public class Employee
    {
        public int? EmployeeId { get; set; }
        public string? UserId { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool ActiveFlag { get; set; }
        public int FacilityId { get; set; }


    }
}
