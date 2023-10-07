using Bugshield.Models;

namespace Bugshield.Interfaces
{
    public interface IAllotedQueryRepository
    {
        Task<IEnumerable<AllotedQuery>> GetAllotedQueriesAsync();
        Task<AllotedQuery> GetSingleAllotedQueryAsync(int id);
        Task UpdateAllotedQueryAsync(int id, AllotedQuery updatedQuery);
        Task<AllotedQuery> CreateAllotedQueryAsync(AllotedQuery allotedQuery);
        Task DeleteAllotedQueryAsync(int id);
    }

}
