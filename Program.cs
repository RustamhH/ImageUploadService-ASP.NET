using ImageUploadService.Context;
using ImageUploadService.Middlewares;
using ImageUploadService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ImageService>();
builder.Services.AddDbContext<AppDBContext>(options => {
    ConfigurationBuilder configurationBuilder = new();
    var builder = configurationBuilder.AddJsonFile("appsettings.json").Build();
    options.UseSqlServer(builder.GetConnectionString("Default"));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseStaticFiles();
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
