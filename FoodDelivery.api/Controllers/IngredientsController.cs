using FoodDelivery.Application.Ingredients.Commands.AddIngredients;
using FoodDelivery.Application.Ingredients.Commands.DeleteIngredient;
using FoodDelivery.Application.Ingredients.Queries.GetAll;
using FoodDelivery.Application.Ingredients.Queries.GetById;
using FoodDelivery.Contracts.Sections;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.api.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public sealed class IngredientsController(ISender _mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddIngredientsAsync([FromForm] AddIngredientRequest request)
        {
            if (request == null)
                return BadRequest("Ingredients list cannot be empty.");
            var command = new AddIngredientCommand(request.Name, request.Picture, request.IngredientType);
            await _mediator.Send(command);
            return Accepted();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteIngredientCommand(id);
            await _mediator.Send(command);

            return NoContent(); 
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var resutl =await _mediator.Send(  new GetAllIngredientsQuery());
            return Ok(resutl);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new  GetIngredientByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}