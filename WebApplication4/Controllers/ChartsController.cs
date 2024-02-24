using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")] // /api/charts/...
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DbcoursesContext context;

        public ChartsController(DbcoursesContext context)
        {
            this.context = context;
        }

        [HttpGet("countByDifLvl")]
        public async Task <IActionResult> GetCountByDifLvlAsync() 
        {

            {
                var result = await context.Courses
                    .GroupBy(category => category.DifficultyLevel.DifLevel)
                    .Select(categoryGroup => new drawСountByDifLvlChart(categoryGroup.Key.ToString(), categoryGroup.Count()))
                    .ToListAsync();
                return new JsonResult(result);

            };
        }
        [HttpGet("countByCategory")]
        public async Task<IActionResult> GetCountByCategoryAsync()
        {
            var result = await context.Courses
                    .GroupBy(course => course.Category.Category1)
                    .Select(categoryGroup => new drawСountCategoryChart( categoryGroup.Key.ToString(), categoryGroup.Count() ))
                    .ToListAsync();
            return new JsonResult(result);
        }

    }
}
