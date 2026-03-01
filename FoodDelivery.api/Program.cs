using FoodDelivery.api;
using FoodDelivery.Api.Middleware;
using FoodDelivery.Application;
using FoodDelivery.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


}
var app = builder.Build();
{
    
        app.UseSwagger();
        app.UseSwaggerUI();



    app.UseStaticFiles(); 

    var uploadsRoot = Path.Combine(app.Environment.ContentRootPath, "uploads");
    Directory.CreateDirectory(uploadsRoot);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(uploadsRoot),
        RequestPath = "/uploads"
    });
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.MapControllers();
    app.Run();
}