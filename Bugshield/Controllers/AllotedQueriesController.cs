using Microsoft.AspNetCore.Mvc;
using Bugshield.Models;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;
using Bugshield.Interfaces;

namespace Bugshield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AllotedQueriesController : ControllerBase
    {
        private readonly IAllotedQueryRepository _repository;

        public AllotedQueriesController(IAllotedQueryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/AllotedQueries
        /// <summary>
        /// GET method for receiving all Alloted Queries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin , SA Team")]
        public async Task<ActionResult<IEnumerable<AllotedQuery>>> GetAllotedQueries()
        {
            try
            {
                var allotedQueries = await _repository.GetAllotedQueriesAsync();
                return Ok(allotedQueries);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/AllotedQueries/5
        /// <summary>
        ///  GET method for receiving that particular Alloted Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Admin, SA Team, Users")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AllotedQuery>> GetAllotedQuery(int id)
        {
            var allotedQuery = await _repository.GetSingleAllotedQueryAsync(id);
          
            try
            {
                if (allotedQuery == null)
                {
                    return NotFound("Query Raised by admin has not been alloted to the specified user.");
                }
                return Ok(allotedQuery);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updating Alloted Query 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="allotedQuery"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SA Team")]
        public async Task<IActionResult> PutAllotedQuery(int? id, [FromBody] AllotedQuery updatedQuery)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("ID value is null");
                }

                await _repository.UpdateAllotedQueryAsync(id.Value, updatedQuery);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Method to post Alloted query
        /// </summary>
        /// <param name="allotedQuery"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin , SA Team")]
        public async Task<ActionResult<AllotedQuery>> PostAllotedQuery(AllotedQuery allotedQuery)
        {
            try
            {
                if(allotedQuery ==null)
                {
                    return BadRequest("value cannot be null");

                }
                var createdQuery = await _repository.CreateAllotedQueryAsync(allotedQuery);
                return Ok(createdQuery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Method to delete a Alloted Query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SA Team")]
        public async Task<IActionResult> DeleteAllotedQuery(int id)
        {
            try
            {
                if( id == 0)
                {
                    return BadRequest("id cannot be null");
                }
                await _repository.DeleteAllotedQueryAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Email To be Sent to User by SA Team after accepting Ticket
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest emailRequest)
        {
            if (ModelState.IsValid)
            {

                if (emailRequest == null)
                {
                    return BadRequest("Invalid request");
                }

                try
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(emailRequest.FromEmail));
                    message.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                    message.Subject = "Subject of the Email";

                    var text = new TextPart("plain")
                    {
                        Text = $"Hi there, we will start working on the issue: {emailRequest.Query} and possibly resolved with {emailRequest.SolvedTime} ",
                    };

                    var multipart = new Multipart("mixed")
                    {
                    text
                    };
                    message.Body = multipart;

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync(emailRequest.FromEmail, emailRequest.Password);
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    return Ok("Email sent successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return Content("Send Email method executed");
        }
        /// <summary>
        /// Email To be Send by Admin to SA Team
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        [HttpPost("Email")]
        public async Task<IActionResult> EmailAsync([FromBody] EmailToSA emailRequest)
        {
            if (emailRequest == null)
            {
                return BadRequest("Invalid request");
            }
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(emailRequest.FromEmail));
                message.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                message.Subject = "Query Allocation Notification";
                var text = new TextPart("plain")
                {
                    Text = $"Hi SA Team,\n\n"
                           + "I wanted to inform you that a new query has been allocated to you by the admin."
                           + "Please review the details and take appropriate action as soon as possible.\n\n"
                           + "Thank you for your prompt attention to this matter.\n\n"
                           + "Best regards,\n"
                           + "Yours Admin",
                };
                var multipart = new Multipart("mixed")
                {

                    text
                };
                message.Body = multipart;
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync(emailRequest.FromEmail, "Yamini@1611");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// Email to be Send to User from SA Team
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        [HttpPost("EmailToUser")]

        public async Task<IActionResult> EmailToUserAsync([FromBody] EmailtoUser emailRequest)
        {
            if (emailRequest == null)
            {
                return BadRequest("Invalid request");
            }
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(emailRequest.FromEmail));
                message.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                message.Subject = "Ticket Update";

                var text = new TextPart("plain")
                {
                    Text = $"Dear User,\n\n" +
                               $"We have an important update regarding the raised issue:\n\n" +
                               $"Issue: {emailRequest.Query}\n" +
                               $"Status: {emailRequest.Progress}\n" +
                               $"Resolved Time: {emailRequest.SolvedTime}\n\n" +
                               $"Please review this information and take any necessary actions.\n\n" +
                               $"Best regards,\n" +
                               $"The SA Team"
                };

                var multipart = new Multipart("mixed")
                {
                    text
                };
                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync(emailRequest.FromEmail, emailRequest.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Email to be Send to Admin from SA Team
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        [HttpPost("EmailToAdmin")]
        public async Task<IActionResult> EmailToAdminAsync([FromBody] EmailtoAdmin emailRequest)
        {
            if (emailRequest == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(emailRequest.FromEmail));
                message.To.Add(MailboxAddress.Parse("20bsca151yaminipriyaj@skacas.ac.in"));
                message.Subject = "Ticket Update";

                var text = new TextPart("plain")
                {
                    Text = $"Dear Admin,\n\n" +
                               $"I have an important update regarding the alloted issue:\n\n" +
                               $"Issue: {emailRequest.Query}\n" +
                               $"Status: {emailRequest.Progress}\n" +
                               $"Resolved Time: {emailRequest.SolvedTime}\n\n" +
                               $"Please review this information and take any necessary actions.\n\n" +
                               $"Best regards,\n" +
                               $"The SA Team"
                };

                var multipart = new Multipart("mixed")
                {
                    text
                };

                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync(emailRequest.FromEmail, emailRequest.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }


}
