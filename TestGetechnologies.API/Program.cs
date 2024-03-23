using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestGetechnologies.API.Business;
using TestGetechnologies.API.DataAccess;
using TestGetechnologies.API.DbConfig;
using TestGetechnologies.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Directory.CreateDirectory(builder.Configuration.GetDbDirectory());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetSqLiteConnectionString())
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//Repository services
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
//Business servicess
builder.Services.AddScoped<Directorio>();
builder.Services.AddScoped<Ventas>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
