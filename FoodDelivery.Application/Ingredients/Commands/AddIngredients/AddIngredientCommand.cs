using FoodDelivery.Application.Admin.Commands.AddIngredients;
using FoodDelivery.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Application.Ingredients.Commands.AddIngredients
{
    public sealed record AddIngredientCommand(string Name,IFormFile? Picture,IngredientType IngredientType) : IRequest;

}
