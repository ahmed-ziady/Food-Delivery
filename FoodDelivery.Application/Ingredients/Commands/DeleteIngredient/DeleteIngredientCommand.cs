using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Ingredients.Commands.DeleteIngredient
{
    public sealed record  DeleteIngredientCommand(Guid Id):IRequest;
    
}
