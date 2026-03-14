using FoodDelivery.api;
using FoodDelivery.Api.Middleware;
using FoodDelivery.Application;
using FoodDelivery.Infrastructure;
using FoodDelivery.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
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

    app.UseRateLimiter();

    app.UseStaticFiles();
    var uploadsRoot = Path.Combine(app.Environment.ContentRootPath, "uploads");
    Directory.CreateDirectory(uploadsRoot);
    using (var scope = app.Services.CreateScope())
    {
        var RoleManage = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        await RolePermissionSeeder.SeedAsync(RoleManage);
    }
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