//using System;
//using System.Collections.Generic;
//using WebApplication4.Data;

//namespace WebApplication4.Data2;

//public partial class Course
//{
//    public int Id { get; set; }

//    public string? Title { get; set; }

//    public string? Description { get; set; }

//    public int? DifficultyLevelId { get; set; }

//    public int? CategoryId { get; set; }

//    public int TeachersId { get; set; }

//    public int? PhotoUrlId { get; set; }

//    public virtual Photo? PhotoUrl { get; set; }

//    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

//    public virtual Teacher Teachers { get; set; } = null!;
//}
