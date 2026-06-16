namespace UniversityManagementSystem.Models
{
    public class EmployeeWithSalary
    {
        public int e_id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public DateOnly? Birthday { get; set; }

        public string? Gender { get; set; }

        public string? Contact { get; set; }

        public string? NId { get; set; }

        public string? Address { get; set; }

        public string? Department { get; set; }

        public string? Degree { get; set; }

        public decimal? Total { get; set; }

        public string? Pic { get; set; }

        public int? s_id { get; set; }

        //public virtual Salary? SIdNavigation { get; set; }
    }
}
