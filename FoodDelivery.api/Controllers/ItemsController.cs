using FoodDelivery.Application.Menus.Commands.Items.AddItem;
using FoodDelivery.Application.Menus.Commands.Items.DeleteItem;
using FoodDelivery.Application.Menus.Commands.Items.UpdateItem;
using FoodDelivery.Application.Menus.Queries.Items;
using FoodDelivery.Contracts.Menus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.api.Controllers
{
    [Route("api/menus/sections/{sectionId}/items")]
    [ApiController]
    public class ItemsController(ISender mediator) : ControllerBase
    {

        private Guid GetRestaurantId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim switch
            {
                null => throw new UnauthorizedAccessException("User ID claim not found."),
                _ => Guid.Parse(userIdClaim.Value)
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid sectionId)
        {
            var restaurantId = GetRestaurantId();
            var query = new GetAllItemsQuery(restaurantId, sectionId);
            var items = await mediator.Send(query);

            return Ok(items);
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetAsync(Guid sectionId, Guid itemId)
        {
            var restaurantId = GetRestaurantId();
            var query = new GetItemQuery(restaurantId, sectionId, itemId);
            var item = await mediator.Send(query);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Guid sectionId, AddItemRequest request)
        {
            var restaurantId = GetRestaurantId();
            var command = new AddItemCommand(
                restaurantId,
                sectionId,
                request.Name,
                request.Description,
                request.Price
            );

            var item = await mediator.Send(command);
            return CreatedAtAction(nameof(GetAsync), new { sectionId, itemId = item.Id }, item);
        }

        [HttpPatch("{itemId}")]
        public async Task<IActionResult> UpdateAsync(Guid sectionId, Guid itemId, UpdateItemDetailsRequest request)
        {
            var restaurantId = GetRestaurantId();
            var command = new UpdateItemCommand(
                restaurantId,
                sectionId,
                itemId,
                request.Name,
                request.Description,
                request.Price
            );

            var updatedItem = await mediator.Send(command);
            return Ok(updatedItem);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteAsync(Guid sectionId, Guid itemId)
        {
            var restaurantId = GetRestaurantId();
            var command = new DeleteItemCommand(restaurantId, sectionId, itemId);

            await mediator.Send(command);
            return NoContent();
        }
    }
}