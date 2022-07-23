using DotnetCoreLogging.CustomLog;
using DotnetCoreLogging.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var optionsSection = builder.Configuration.GetSection("Logging")
    .GetSection("Database").GetSection("Options");
builder.Logging.AddDatabaseLogger(options =>
{
    optionsSection.Bind(options);
});

var connectionString = optionsSection.GetValue<string>("ConnectionString");
builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "Logging");
        o.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
