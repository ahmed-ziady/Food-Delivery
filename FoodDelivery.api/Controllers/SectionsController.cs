using FoodDelivery.Application.Menus.Commands.Sections.RemoveSection;
using FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName;
using FoodDelivery.Application.Menus.Queries.Section;
using FoodDelivery.Application.Sections.Commands.Sections.AddSection;
using FoodDelivery.Contracts.Sections;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.api.Controllers
{

    [Authorize]
    [Route("api/sections")]
    [ApiController]
    public class SectionsController(IMapper _mapper, ISender _mediator) : ControllerBase
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
        public async Task<IActionResult> AddAsync(AddMenuSectionRequest request, CancellationToken cancellationToken)
        {
            var onwerId = GetUserId();
            var command = _mapper.Map<AddMenuSectionCommand>((request, onwerId));
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPatch("{sectionId}")]
        public async Task<IActionResult> UpdateNameAsync(Guid sectionId, UpdateSectionNameRequest request, CancellationToken cancellationToken)
        {
            var restaurantId = GetUserId();
            var command = new UpdateSectionNameCommand( restaurantId, sectionId,  request.Name
            );
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{sectionId}")]
        public async Task<IActionResult> DeleteAsync(Guid sectionId, CancellationToken cancellationToken)
        {
            var restaurantId = GetUserId();

            var command = new RemoveSectionCommand(restaurantId, sectionId);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var rstuarantId = GetUserId();
            var query = new GetAllSectionsQuery(rstuarantId);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
