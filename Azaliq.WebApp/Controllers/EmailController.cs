using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels._2fa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IOrderService _orderService;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmailController(
            IEmailSender emailSender,
            IOrderService orderService,
            IPdfService pdfService,
            IEmailService emailService,
            UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _orderService = orderService;
            _pdfService = pdfService;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost("send")]
        [ValidateAntiForgeryToken]
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
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("invoice")]
        [Authorize] // Ensure only logged-in users can request their invoice
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailInvoicePdf([FromQuery] int orderId)
        {
            var order = await _context
                .Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            
            if (order == null)
                return NotFound("Order not found.");

            // Get current logged-in user ID from claims
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return BadRequest("User is not logged in.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("User or email not found.");

            var pdfBytes = await _pdfService.GenerateOrderPdfWithQrAsync(order);

            await _emailService.SendEmailWithAttachmentAsync(
                user.Email,
                $"Your Azaliq Order Invoice - #{order.Id}",
                "<p>Thank you for your order from Azaliq! Your invoice is attached below.</p>",
                pdfBytes,
                $"Azaliq_Invoice_{order.Id}.pdf"
            );

            return Ok("Invoice emailed successfully.");
        }

    }
}