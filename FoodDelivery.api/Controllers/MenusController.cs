using FoodDelivery.Application.Menus.Commands.Sections.AddSection;
using FoodDelivery.Application.Menus.Commands.Sections.RemoveSection;
using FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName;
using FoodDelivery.Contracts.Menus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.api.Controllers
{

    [Authorize]
    [Route("api/menus/sections")]
    [ApiController]
    public class MenusController(IMapper _mapper, ISender _mediator) : ControllerBase
    {
        private Guid GetUserId()
        {
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim switch
            {
                null => throw new UnauthorizedAccessException("User ID claim not found."),
                _ => Guid.Parse(userIdClaim.Value)
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddSection(AddMenuSectionRequest request, CancellationToken cancellationToken)
        {
            var onwerId = GetUserId();
            var command = _mapper.Map<AddMenuSectionCommand>((request, onwerId));
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPatch("{sectionId}")]
        public async Task<IActionResult> UpdateSectionName(Guid sectionId, UpdateSectionNameRequest request, CancellationToken cancellationToken)
        {
            var restaurantId = GetUserId();
            var command = new UpdateSectionNameCommand(
                restaurantId,
                sectionId,
                request.Name
            );
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{sectionId}")]
        public async Task<IActionResult> DeleteSection( Guid sectionId, CancellationToken cancellationToken)
        {
            var restaurantId = GetUserId();

            var command = new RemoveSectionCommand(restaurantId, sectionId);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
