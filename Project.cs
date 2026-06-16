using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.Models;

public partial class Project
{
    public int PId { get; set; }

    public int? EId { get; set; }

    public string? PName { get; set; }

    public DateOnly? Duedate { get; set; }

    public DateOnly? Subdate { get; set; }

    public string? Mark { get; set; }

    public string? PStatus { get; set; }

    public virtual Employee? EIdNavigation { get; set; }
}
