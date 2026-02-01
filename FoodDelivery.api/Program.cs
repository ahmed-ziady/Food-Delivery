using FoodDelivery.api;
using FoodDelivery.Api.Middleware;
using FoodDelivery.Application;
using FoodDelivery.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


}
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.MapControllers();
    app.Run();
}