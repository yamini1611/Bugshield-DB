using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bugshield.Models;
using Bugshield.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bugshield.Controllers
{
    [Authorize(Roles = "SA Team,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorLogsController : ControllerBase
    {
        private readonly IErrorLogRepository _repository;

        public ErrorLogsController(IErrorLogRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// To Get All Error Logs
        /// </summary>
        /// <returns></returns>
        // GET: api/ErrorLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorLog>>> GetErrorLogs()
        {
            try
            {
                var errorLogs = await _repository.GetAllErrorLogsAsync();
                return Ok(errorLogs);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// To Get a Specific Error Log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ErrorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorLog>> GetErrorLog(int id)
        {
            if (id== 0)
            {
                return Content("ID value is 0");
            }
            try
            {
                var errorLog = await _repository.GetErrorLogByIdAsync(id);
                return Ok(errorLog);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// To Update a Error Log
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        // PUT: api/ErrorLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrorLog(int id, ErrorLog errorLog)
        {
            if (id != errorLog.ErrorLogId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateErrorLogAsync(errorLog);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

            return Content("Error Log Updation is executed");
        }
        /// <summary>
        /// To post a Error Log
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        // POST: api/ErrorLogs
        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostErrorLog(ErrorLog errorLog)
        {
            try
            {
                await _repository.CreateErrorLogAsync(errorLog);
                return Ok(CreatedAtAction("GetErrorLog", new { id = errorLog.ErrorLogId }, errorLog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// To Delete a Error Log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ErrorLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrorLog(int id)
        {
            if (!await _repository.ErrorLogExistsAsync(id))
            {
                return Content("ID does not exists");
            }
            try
            {
                await _repository.DeleteErrorLogAsync(id);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Content("Error log Delete Executed");
        }
    }
}
