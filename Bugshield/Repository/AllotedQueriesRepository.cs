using Bugshield.Interfaces;
using Bugshield.Models;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace Bugshield.Repository
{
    public class AllotedQueryRepository : IAllotedQueryRepository
    {
        private readonly ProjectBugshieldContext _context;
        public AllotedQueryRepository(ProjectBugshieldContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AllotedQuery>> GetAllotedQueriesAsync()
        {
            return await _context.AllotedQueries.ToListAsync();
        }
        public async Task<AllotedQuery> GetSingleAllotedQueryAsync(int id)
        {
            return await _context.AllotedQueries.FirstOrDefaultAsync(q => q.RaisedUser == id);
        }
        public async Task UpdateAllotedQueryAsync(int id, AllotedQuery updatedQuery)
        {
            var existingQuery = await _context.AllotedQueries.FindAsync(id);
            if (existingQuery != null)
            {
                existingQuery.Sauser = updatedQuery.Sauser;
                existingQuery.Progress = updatedQuery.Progress;
                existingQuery.Remarks = updatedQuery.Remarks;
                existingQuery.SolvedTime = updatedQuery.SolvedTime;
                existingQuery.AllotedQueries = updatedQuery.AllotedQueries;
                existingQuery.RaisedUser = updatedQuery.RaisedUser;

                _context.Entry(existingQuery).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<AllotedQuery> CreateAllotedQueryAsync(AllotedQuery allotedQuery)
        {
            _context.AllotedQueries.Add(allotedQuery);
            await _context.SaveChangesAsync();
            return allotedQuery;
        }
        public async Task DeleteAllotedQueryAsync(int id)
        {
            var allotedQuery = await _context.AllotedQueries.FindAsync(id);
            if (allotedQuery != null)
            {
                _context.AllotedQueries.Remove(allotedQuery);
                await _context.SaveChangesAsync();
            }
        }
    }

}
