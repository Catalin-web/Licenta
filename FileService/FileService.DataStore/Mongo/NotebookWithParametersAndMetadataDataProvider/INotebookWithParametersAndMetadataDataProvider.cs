using Fileservice.Models.Entities;
using System.Linq.Expressions;

namespace Fileservice.DataStore.Mongo.NotebookDataProvider
{
    public interface INotebookWithParametersAndMetadataDataProvider
    {
        Task InsertOrUpdateAsync(NotebookWithParametersAndMetadata notebookWithParametersAndMetadata);
        Task UpdateAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match, NotebookWithParametersAndMetadata notebookWithParametersAndMetadata);
        Task DeleteAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match);
        Task<NotebookWithParametersAndMetadata> GetAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match);
        Task<IEnumerable<NotebookWithParametersAndMetadata>> GetAllAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match);
    }
}
