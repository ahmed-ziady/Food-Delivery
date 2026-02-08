using FoodDelivery.Application.Menus.Commands.CreateMenu;
using FoodDelivery.Contracts.Menus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MenusController(IMapper _mapper, ISender _mediator) : ControllerBase
    {
        [HttpPost("{hostId}")]
        public async Task<IActionResult> CreateMenu(string hostId, [FromBody] CreateMenuRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateMenuCommand>((request, hostId));
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<MenuResponse>(result);
            return Ok(response);
        }

    }
}
