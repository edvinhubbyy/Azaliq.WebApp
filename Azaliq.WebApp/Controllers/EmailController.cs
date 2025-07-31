using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels._2fa;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] EmailViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _emailSender.SendEmailAsync(model.ToEmail, model.Subject, model.Message);
                return Ok(new { message = "Email sent successfully." });
            }
            catch (Exception ex)
            {
                // log ex if you have logging configured
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
