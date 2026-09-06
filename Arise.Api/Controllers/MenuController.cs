using Arise.Api.Extensions;
using Arise.Application.Features.Menus.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arise.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems([FromQuery] GetMenuItemsQuery request, CancellationToken ct)
        {
            var result = await _mediator.Send(request, ct);
            return result.ToActionResult();
        }
    }
}
