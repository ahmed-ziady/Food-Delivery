using FoodDelivery.Application.Menus.Commands.Items.AddItem;
using FoodDelivery.Application.Menus.Commands.Items.AddItemPictures;
using FoodDelivery.Application.Menus.Commands.Items.DeleteItem;
using FoodDelivery.Application.Menus.Commands.Items.DeleteItemPicture;
using FoodDelivery.Application.Sections.Commands.Items.AddIngredientsToItem;
using FoodDelivery.Application.Sections.Commands.Items.AddItem;
using FoodDelivery.Application.Sections.Commands.Items.UpdateItem;
using FoodDelivery.Application.Sections.Commands.Items.UpdateItemIngredients;
using FoodDelivery.Application.Sections.Queries.Items;
using FoodDelivery.Contracts.Sections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.api.Controllers
{
    [Route("api/sections/{sectionId}/items")]
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

        [HttpGet("{itemId}", Name = "GetSectionItem")]
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
                request.Price,
                request.DeliveryType
            );

            var item = await mediator.Send(command);

            return CreatedAtRoute("GetSectionItem", new { sectionId, itemId = item.Id }, item);
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
        [HttpPost("{itemId}/pictures")]
        public async Task<IActionResult> UploadAsync(Guid sectionId, Guid itemId, IFormFileCollection formFiles)

        {
            var restaurantId = GetRestaurantId();
            var command = new AddItemPicturesCommand(restaurantId, sectionId, itemId, formFiles);
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{itemId}/pictures")]
        public async Task<IActionResult> DeleteAsync(Guid sectionId, Guid itemId, [FromQuery] string url)
        {
            var restaurantId = GetRestaurantId();

            if (string.IsNullOrWhiteSpace(url))
                return BadRequest("Picture URL is required.");
            var command = new DeleteItemPictureCommand(restaurantId, sectionId, itemId, url);
            await mediator.Send(command);

            return NoContent();
        }
        [HttpPost("{itemId}/ingredients")]
        public async Task<IActionResult> AddAsync(Guid sectionId, Guid itemId, AddIngredientsToITemRequest request)
        {
            var restuarantId = GetRestaurantId();
            await mediator.Send(new AddIngredientsToItemCommand(restuarantId, sectionId, itemId, request.IngredientIds));
            return NoContent();
        }
        [HttpPut("{itemId}/ingredients")]
        public async Task<IActionResult> UpdateAsync(Guid restuarantId, Guid sectionId, Guid itemId, UpdateItemIngredientsRequest request)
        {
            await mediator.Send(new UpdateItemIngredientsCommand(restuarantId, sectionId, itemId, request.IngredientIds));
            return NoContent();
        }
    }


}