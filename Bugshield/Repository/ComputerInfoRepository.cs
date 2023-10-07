#nullable disable
using Microsoft.EntityFrameworkCore;
using Bugshield.Models;

namespace Bugshield.Repositories
{
    public class ComputerInfoRepository : IComputerInfoRepository
    {
        private readonly ProjectBugshieldContext _context;

        public ComputerInfoRepository(ProjectBugshieldContext context)
        {
            _context = context;
        }
        /// <summary>
        /// get All computer Info
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ComputerInfo>> GetAllComputerInfosAsync()
        {
            return await _context.ComputerInfos.ToListAsync();
        }
        /// <summary>
        /// get a particular Computer Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ComputerInfo> GetComputerInfoByIdAsync(int id)
        {
            return await _context.ComputerInfos.FindAsync(id);
        }
        /// <summary>
        /// To Add a computer
        /// </summary>
        /// <param name="computerInfo"></param>
        /// <returns></returns>
        public async Task CreateComputerInfoAsync(ComputerInfo computerInfo)
        {
            _context.ComputerInfos.Add(computerInfo);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// To Update a Computer Info
        /// </summary>
        /// <param name="computerInfo"></param>
        /// <returns></returns>
        public async Task UpdateComputerInfoAsync(ComputerInfo computerInfo)
        {
            _context.Entry(computerInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Delete a Computer Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteComputerInfoAsync(int id)
        {
            var computerInfo = await _context.ComputerInfos.FindAsync(id);
            if (computerInfo != null)
            {
                _context.ComputerInfos.Remove(computerInfo);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Method to check if a passed computer  id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ComputerInfoExistsAsync(int id)
        {
            return await _context.ComputerInfos.AnyAsync(e => e.ComputerId == id);
        }
        /// <summary>
        /// To get the last assigned computer id
        /// </summary>
        /// <returns></returns>
        public async Task<ComputerInfo> GetLastAssignedComputerAsync()
        {
            return await _context.ComputerInfos
                .OrderByDescending(c => c.ComputerId) 
                .FirstOrDefaultAsync(); 
        }

        /// <summary>
        /// To To create a back up computer table
        /// </summary>
        /// <param name="computerInfo"></param>
        /// <returns></returns>
        public async Task CreateBackUpComputerInfoAsync(ComputerInfoBackup computerInfo)
        {
            try
            {
                _context.ComputerInfoBackups.Add(computerInfo);
                 await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
                throw; 
            }
        }

    }
}
