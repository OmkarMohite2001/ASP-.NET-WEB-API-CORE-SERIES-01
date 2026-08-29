using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(SendEmailDto dto)
        {
            await _emailService.SendEmailAsync(dto.To, dto.Subject, dto.Body);
            return Ok(new
            {
                message = "Email Sent Successfully."
            });
        }
    }
}
