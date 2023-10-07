using Microsoft.AspNetCore.Mvc;
using Bugshield.Interfaces;
using Bugshield.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bugshield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryRepository _queryRepository;
        public QueriesController(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }
        /// <summary>
        /// To Get All Queries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "SA Team,Admin")]
        public async Task<ActionResult<IEnumerable<Query>>> GetQueries()
        {
            var queries = await _queryRepository.GetQueriesAsync();
            return Ok(queries);
        }
        /// <summary>
        /// To get Specific Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "SA Team,Admin")]
        public async Task<ActionResult<Query>> GetQuery(int id)
        {
            if(id == 0)
            {
                return BadRequest("id cannot be 0");
            }
            var query = await _queryRepository.GetQueryAsync(id);

            if (query == null)
            {
                return BadRequest(" Requested Query not found");

            }
            return query;
        }
        /// <summary>
        /// To Update a Specfic Query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "SA Team,Admin")]

        public async Task<IActionResult> PutQuery(int id, Query query)
        {
            if (id != query.QueryId)
            {
                return BadRequest();
            }
            try
            {
                await _queryRepository.UpdateQueryAsync(id, query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
        /// <summary>
        /// To Post a Query
        /// </summary>
        /// <param name="queryPosted"></param>
        /// <returns></returns>
        [HttpPost("PostQueries")]
        [Authorize(Roles = "SA Team,Admin,Users")]
        public async Task<ActionResult<Query>> PostQuery([FromBody] QueryPosted queryPosted)
        {
            try
            {
                var query = new Query
                {
                    QueryDetails = queryPosted.QueryDetails,
                    SolvedTime = queryPosted.SolvedTime,
                    RaisedTime = queryPosted.RaisedTime,
                    UserId = queryPosted.UserId,
                    IsSolved = queryPosted.IsSolved
                };

                await _queryRepository.CreateQueryAsync(query);
                return CreatedAtAction("GetQuery", new { id = query.QueryId }, query);
            }
            catch
            {
                return BadRequest("Error posting details");
            }
        }
        /// <summary>
        /// To delete a Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SA Team")]

        public async Task<IActionResult> DeleteQuery(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                await _queryRepository.DeleteQueryAsync(id);
            }   
            catch(Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            return Content("Delete method executed");
        }
    }
}
