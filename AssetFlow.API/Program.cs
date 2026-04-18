using System.Security.Cryptography.X509Certificates;
using AssetFlow.API.Data;
using AssetFlow.API.Repositories;
using AssetFlow.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetService, AssetService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
db.Database.Migrate();
app.Run();