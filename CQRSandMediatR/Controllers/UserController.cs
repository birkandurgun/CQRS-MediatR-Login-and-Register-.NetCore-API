using CQRSandMediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSandMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUserByIdQuery() { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

    }
}
