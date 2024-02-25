using DocumentFormat.OpenXml.Vml.Office;
using WebApplication4.Data;
namespace WebApplication4.Services
{
    public interface IImportService<TEntity>
         where TEntity : Entity
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}

