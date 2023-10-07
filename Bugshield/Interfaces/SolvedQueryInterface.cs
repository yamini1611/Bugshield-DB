using Bugshield.Models;

namespace Bugshield.Repositories
{
    public interface ISolvedQueryRepository
    {
        Task<IEnumerable<SolvedQuery>> GetAllSolvedQueriesAsync();
        Task<SolvedQuery> GetSolvedQueryByIdAsync(int id);
        Task CreateSolvedQueryAsync(SolvedQuery solvedQuery);
        Task UpdateSolvedQueryAsync(SolvedQuery solvedQuery);
        Task DeleteSolvedQueryAsync(int id);
        Task<bool> SolvedQueryExistsAsync(int id);
    }
}
