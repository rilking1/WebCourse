using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Data;

public partial class Course
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Швидко ввів назву (ง'̀-'́)ง")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Швидко ввів опис (ง'̀-'́)ง")]
    public string? Description { get; set; }

    public int? DifficultyLevelId { get; set; }

    public int? CategoryId { get; set; }




    public int TeachersId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual DifficultyLevel? DifficultyLevel { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher? Teachers { get; set; }
}
