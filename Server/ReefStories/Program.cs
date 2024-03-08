using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReefStories.DataAccess.Identity;
using ReefStories.Domain.Models.Identity;
using ReefStories.Helpers.Extensions;
using ReefStories.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentityServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await identityContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
