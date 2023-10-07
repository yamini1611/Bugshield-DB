using Bugshield.Interfaces;
using Bugshield.Models;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace Bugshield.Repository
{
    public class QueryRepository : IQueryRepository
    {
        private readonly ProjectBugshieldContext _context;
        public QueryRepository(ProjectBugshieldContext context)
        {
            _context = context;
        }
        /// <summary>
        /// To All Raised Queries
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Query>> GetQueriesAsync()
        {
            return await _context.Queries.ToListAsync();
        }
        /// <summary>
        /// To get a Specific raised Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Query> GetQueryAsync(int id)
        {
            return await _context.Queries.FindAsync(id);
        }
        /// <summary>
        /// To Create a Query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task CreateQueryAsync(Query query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            _context.Queries.Add(query);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// To Update a Raised Query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task UpdateQueryAsync(int id, Query query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            var existingQuery = await _context.Queries.FindAsync(id) ?? throw new InvalidOperationException("Query not found");
            existingQuery.QueryDetails = query.QueryDetails;
            existingQuery.SolvedTime = query.SolvedTime;
            existingQuery.RaisedTime = query.RaisedTime;
            existingQuery.UserId = query.UserId;
            existingQuery.IsSolved = query.IsSolved;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// To Delete a raised Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteQueryAsync(int id)
        {
            var query = await _context.Queries.FindAsync(id);
            if (query != null)
            {
                _context.Queries.Remove(query);
                await _context.SaveChangesAsync();
            }
        }
    }
}
