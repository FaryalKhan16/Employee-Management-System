using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.Models;

public partial class Rank
{
    public int? EId { get; set; }

    public string? Points { get; set; }

    public virtual EmployeeLeave? EIdNavigation { get; set; }
}
