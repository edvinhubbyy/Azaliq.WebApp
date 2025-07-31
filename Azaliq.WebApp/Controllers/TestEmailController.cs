using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Azaliq.Services.Core.Contracts;

[Route("api/[controller]")]
[ApiController]
public class TestEmailController : ControllerBase
{
    private readonly ITestEmailService _testEmailService;

    public TestEmailController(ITestEmailService testEmailService)
    {
        _testEmailService = testEmailService;
    }

    [HttpGet("send")]
    public async Task<IActionResult> SendTestEmail([FromQuery] string toEmail)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            return BadRequest("toEmail query parameter is required");
        }

        try
        {
            await _testEmailService.SendTestEmailAsync(toEmail);
            return Ok("Test email sent successfully");
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Error sending email: {ex.Message}");
        }
    }
}