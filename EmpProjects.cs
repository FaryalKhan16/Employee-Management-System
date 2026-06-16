namespace UniversityManagementSystem.Models
{
    public class EmpProjects
    {
        public int p_id { get; set; }

        public int? e_id { get; set; }

        public string? p_name { get; set; }

        public DateOnly? duedate { get; set; }

        public DateOnly? subdate { get; set; }

        public string? mark { get; set; }

        public string? p_status { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
