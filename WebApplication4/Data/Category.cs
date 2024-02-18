using System;
using System.Collections.Generic;

namespace WebApplication4.Data;

public partial class Category
{
    public int Id { get; set; }

    public string? Category1 { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
