using Arise.Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arise.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("by-userId/{userId}")]
        public async Task<IActionResult> GetEmployeeByUserId(Guid userId)
        {
            var query = new GetEmployeeByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
