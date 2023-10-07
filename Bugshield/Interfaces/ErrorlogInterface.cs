
using Bugshield.Models;

namespace Bugshield.Repositories
{
    public interface IErrorLogRepository
    {
        Task<IEnumerable<ErrorLog>> GetAllErrorLogsAsync();
        Task<ErrorLog> GetErrorLogByIdAsync(int id);
        Task CreateErrorLogAsync(ErrorLog errorLog);
        Task UpdateErrorLogAsync(ErrorLog errorLog);
        Task DeleteErrorLogAsync(int id);
        Task<bool> ErrorLogExistsAsync(int id);
    }
}
