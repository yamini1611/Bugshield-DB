
using Bugshield.Models;

namespace Bugshield.Repositories
{
    public interface IComputerInfoRepository
    {
        Task<IEnumerable<ComputerInfo>> GetAllComputerInfosAsync();
        Task<ComputerInfo> GetComputerInfoByIdAsync(int id);
        Task CreateComputerInfoAsync(ComputerInfo computerInfo);
        Task UpdateComputerInfoAsync(ComputerInfo computerInfo);
        Task DeleteComputerInfoAsync(int id);
        Task<bool> ComputerInfoExistsAsync(int id);
        Task<ComputerInfo> GetLastAssignedComputerAsync();
        Task CreateBackUpComputerInfoAsync(ComputerInfoBackup backup);
    }
}
