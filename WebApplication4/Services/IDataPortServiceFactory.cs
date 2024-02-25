using WebApplication4.Data;

namespace WebApplication4.Services
{
   public interface IDataPortServiceFactory<TEntity>
   where TEntity : Entity
    {
        IImportService<TEntity> GetImportService(string contentType);
        //IExportService<TEntity> GetExportService(string contentType);
    }

}
