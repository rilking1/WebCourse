using ClosedXML.Excel;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
namespace WebApplication4.Services
{

    
        public class CategoryImportService : IImportService<Category>
        {
            private readonly DbcoursesContext _context;


            public CategoryImportService(DbcoursesContext context)
            {
                _context = context;
            }

            public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
            {
                if (!stream.CanRead)
                {
                    throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
                }

                using (XLWorkbook workBook = new XLWorkbook(stream))
                {
                    //перегляд усіх листів (в даному випадку категорій книг)
                    foreach (IXLWorksheet worksheet in workBook.Worksheets)
                    {
                        //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову

                        var catname = worksheet.Name;
                        var category = await _context.Categorys.FirstOrDefaultAsync(cat => cat.Category1 == catname, cancellationToken);
                        if (category == null)
                        {
                            category = new Category();
                            category.Category1 = catname;
                            //category.Info = "from EXCEL";
                            //додати в контекст
                            _context.Categorys.Add(category);
                        }
                        //перегляд усіх рядків                    
                        foreach (var row in worksheet.RowsUsed().Skip(1))
                        //пропустити перший рядок, бо це заголовок
                        {
                            await AddBookAsync(row, cancellationToken, category);
                        }
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
            }

            private async Task AddBookAsync(IXLRow row, CancellationToken cancellationToken, Category category)
            {
                Course course = new Course();
                course.Title = GetBookName(row);
                course.Description = GetBookInfo(row);
                course.Category = category;
                _context.Courses.Add(course);
                await GetAuthorsAsync(row, course, cancellationToken);
            }

            private static string GetBookName(IXLRow row)
            {
                return row.Cell(1).Value.ToString();
            }

            private static string GetBookInfo(IXLRow row)
            {
                return row.Cell(6).Value.ToString();
            }

            private async Task GetAuthorsAsync(IXLRow row, Course course, CancellationToken cancellationToken)
            {
                //у разі наявності автора знайти його, у разі відсутності - додати
                for (int i = 3; i < 4; i++)
                {
                    //чи є запис про автора
                    if (row.Cell(i).Value.ToString().Length > 0)
                    {
                        var autName = row.Cell(i).Value.ToString();
                        //перевірка чи є такий автор в базі
                        var author = await _context.DifficultyLevels.FirstOrDefaultAsync(aut => aut.DifLevel == autName, cancellationToken);
                        if (author is null)
                        {
                            author = new DifficultyLevel();
                            author.DifLevel = autName;
                            //author.Info = "from EXCEL";
                            _context.DifficultyLevels.Add(author);
                        }

                        //AuthorBook ab = new AuthorBook();
                        //ab.Book = book;
                        //ab.Author = author;
                        //_context.AuthorBooks.Add(ab);
                    }
                }
            }
        }
    
}

















//public class CourseImportService: IImportService<Course>
//{
//    private readonly DbcoursesContext _context;
//    // реалізація AddMovieAsync та інших методів

//    public CourseImportService(DbcoursesContext context)
//    {
//        _context = context;
//    }

//    public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
//    {
//        if (!stream.CanRead)
//        {
//            throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
//        }

//        using (XLWorkbook workBook = new XLWorkbook(stream))
//        {
//            //перегляд усіх листів (в даному випадку категорій книг)
//            foreach (IXLWorksheet worksheet in workBook.Worksheets)
//            {
//                //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову

//                var catname = worksheet.Name;
//                var course = await _context.Courses.FirstOrDefaultAsync(cat => cat.Title == catname, cancellationToken);
//                if (course == null) 
//                {
//                    course = new Course();
//                    course.Title = catname;
//                    course.Description = "from EXCEL";
//                    //додати в контекст
//                    _context.Courses.Add(course);
//                }
//                //перегляд усіх рядків                    
//                foreach (var row in worksheet.RowsUsed().Skip(1))
//                //пропустити перший рядок, бо це заголовок
//                {
//                    await AddBookAsync(row, cancellationToken, course);             
//                }
//            }
//        }
//        await _context.SaveChangesAsync(cancellationToken);
//    }

//    private async Task AddBookAsync(IXLRow row, CancellationToken cancellationToken, Course course)
//    {
//        Course course1 = new Course();
//        course1.Title = GetBookName(row);
//        course1.Description = GetBookInfo(row);
//        _context.Courses.Add(course1);
//         await GetAuthorsAsync(row, course1, cancellationToken);
//    }

//    private static string GetBookName(IXLRow row)
//    {
//        return row.Cell(1).Value.ToString();
//    }

//    private static string GetBookInfo(IXLRow row)
//    {
//        return row.Cell(6).Value.ToString();
//    }

//    private async Task GetAuthorsAsync(IXLRow row, Course course1, CancellationToken cancellationToken)
//    {

//        for (int i = 2; i <= 5; i++)
//        {

//            if (row.Cell(i).Value.ToString().Length > 0)
//            {
//                var courTitle = row.Cell(i).Value.ToString();
//                var course2 = await _context.Courses.FirstOrDefaultAsync(aut => aut.Title == courTitle, cancellationToken);                    
//                if(course2 is null)
//                {
//                    course2 = new Course();
//                    course2.Title = courTitle;
//                    course2.Description = "from EXCEL";
//                    _context.Courses.Add(course2);
//                }

//                Course ab = new Course();
//                ab.Title = courTitle;
//                ab.Title = courTitle;
//                _context.Courses.Add(ab);
//            }
//        }
//    }
//}

