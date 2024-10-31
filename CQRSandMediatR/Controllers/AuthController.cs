using CQRSandMediatR.Commands.Login;
using CQRSandMediatR.Commands.Register;
using CQRSandMediatR.Commands.SendEmail;
using CQRSandMediatR.Commands.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSandMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
        {
            var response = await _mediator.Send(command);
            try
            {
                if (response)
                    return Ok("Email verified successfully.");
                else
                    return BadRequest("Invalid or expired verification token.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPost("send-verification-email")]
        public async Task<IActionResult> SendVerificationMail(SendVerificationEmailCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

    }
}
