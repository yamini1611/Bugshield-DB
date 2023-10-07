#nullable disable
using Microsoft.EntityFrameworkCore;
using Bugshield.Models;

namespace Bugshield.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly ProjectBugshieldContext _context;
        public ErrorLogRepository(ProjectBugshieldContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all Error Logs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ErrorLog>> GetAllErrorLogsAsync()
        {
            return await _context.ErrorLogs.ToListAsync();
        }
        /// <summary>
        /// To get a Error log by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ErrorLog> GetErrorLogByIdAsync(int id)
        {
            return await _context.ErrorLogs.FindAsync(id);
        }
        /// <summary>
        /// To Create a Error log
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        public async Task CreateErrorLogAsync(ErrorLog errorLog)
        {
            _context.ErrorLogs.Add(errorLog);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// To Update Error Log 
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        public async Task UpdateErrorLogAsync(ErrorLog errorLog)
        {
            _context.Entry(errorLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// To Delete a Error Log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteErrorLogAsync(int id)
        {
            var errorLog = await _context.ErrorLogs.FindAsync(id);
            if (errorLog != null)
            {
                _context.ErrorLogs.Remove(errorLog);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Method to check whether a error log exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ErrorLogExistsAsync(int id)
        {
            return await _context.ErrorLogs.AnyAsync(e => e.ErrorLogId == id);
        }
    }
}
