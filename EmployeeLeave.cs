using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.Models;

public partial class EmployeeLeave
{
    public int LId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public int EId { get; set; }

    public virtual Employee EIdNavigation { get; set; } = null!;
}
