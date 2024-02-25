using WebApplication4.Services;
using WebApplication4.Data;

namespace WebApplication4.Services
{
    public class CategoryDataPortServiceFactory : IDataPortServiceFactory<Category>
    {
        private readonly DbcoursesContext _context;
        public CategoryDataPortServiceFactory(DbcoursesContext context)
        {
            _context = context;
        }
        public IImportService<Category> GetImportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new CategoryImportService(_context);
            }
            throw new NotImplementedException($"No import service implemented for movies with content type {contentType}");
        }
        //public IExportService<Category> GetExportService(string contentType)
        //{
        //    if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //    {
        //        return new CategoryExportService(_context);
        //    }
        //    throw new NotImplementedException($"No export service implemented for movies with content type {contentType}");
        //}
    }
}
