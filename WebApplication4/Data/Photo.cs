using System;
using System.Collections.Generic;

namespace WebApplication4.Data;

public partial class Photo
{
    public int Id { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
