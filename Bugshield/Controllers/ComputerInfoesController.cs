using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bugshield.Models;
using Bugshield.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bugshield.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputerInfoesController : ControllerBase
    {
        private readonly IComputerInfoRepository _repository;
        public ComputerInfoesController(IComputerInfoRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// To Get all Computer Info
        /// </summary>
        /// <returns></returns>
        // GET: api/ComputerInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerInfo>>> GetComputerInfos()
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var computerInfos = await _repository.GetAllComputerInfosAsync();
                    return Ok(computerInfos);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Content("Get Computer method is executed");
        }
        ///  <summary>
        /// To Get a particular computer Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ComputerInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerInfo>> GetComputerInfo(int id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var computerInfo = await _repository.GetComputerInfoByIdAsync(id);
                    if (computerInfo == null)
                    {
                        return BadRequest("Id cannot be null");
                    }
                    return Ok(computerInfo);     
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Content("get Computer method is executed");  
        }
        /// <summary>
        /// To Update Computer Info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="computerInfo"></param>
        /// <returns></returns>
        // PUT: api/ComputerInfoes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutComputerInfo(int id, ComputerInfo computerInfo)
        {
            if (ModelState.IsValid)
            {
                if (id != computerInfo.ComputerId)
                {
                    return BadRequest();
                }
                try
                {
                    await _repository.UpdateComputerInfoAsync(computerInfo);
                    return Ok("Computer Info Updated");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Content("Computer Info update is executed");
        }
        /// <summary>
        /// To Create a new Computer in database
        /// </summary>
        /// <param name="computerInfo"></param>
        /// <returns></returns>
        // POST: api/ComputerInfoes
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ComputerInfo>> PostComputerInfo(ComputerInfo computerInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (computerInfo != null)
                    {
                        var lastAssignedComputer = await _repository.GetLastAssignedComputerAsync();

                        int nextComputerId = (lastAssignedComputer?.ComputerId ?? 0) + 1;
                        computerInfo.ComputerId = nextComputerId;

                        await _repository.CreateComputerInfoAsync(computerInfo);
                        return CreatedAtAction("GetComputerInfo", new { id = computerInfo.ComputerId }, computerInfo);
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Content("Post Computer is executed");
        }

        /// <summary>
        /// To Delete a Computer Info in Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ComputerInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerInfo(int id)
        {
            if (ModelState.IsValid)
            {
                if (!await _repository.ComputerInfoExistsAsync(id))
                {
                    return NotFound();
                }
                try
                {
                    await _repository.DeleteComputerInfoAsync(id);
                    return Ok();
                }

                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest($"Could not delete computer info" + ex);
                }
            }

            return Content("Delete computer method is executed");
        }

        [HttpPost("PostBackUp")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> BackUpComputer(ComputerInfoBackup backup)
        {
            if(ModelState.IsValid)
            {
                await _repository.CreateBackUpComputerInfoAsync(backup);
                return Ok();
            }

            return Content("Backup Computer executed");
        }
    }
}
