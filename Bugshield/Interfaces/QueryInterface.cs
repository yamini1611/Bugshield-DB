using Bugshield.Models;

namespace Bugshield.Interfaces
{
    public interface IQueryRepository
    {
        Task<IEnumerable<Query>> GetQueriesAsync();
        Task<Query> GetQueryAsync(int id);
        Task CreateQueryAsync(Query query);
        Task UpdateQueryAsync(int id, Query query);
        Task DeleteQueryAsync(int id);
    }
}
