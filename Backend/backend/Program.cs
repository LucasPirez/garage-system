using System.Reflection;
using backend.Common.Middlewares;
using backend.Database;
using backend.Modules.AuthModule;
using backend.Modules.CustomerModule;
using backend.Modules.CustomerModule.Interfaces;
using backend.Modules.RepairOrderModule;
using backend.Modules.RepairOrderModule.Interfaces;
using backend.Modules.VehicleModule;
using backend.Modules.VehicleModule.Interfaces;
using backend.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static backend.Swagger.DefaultWorksShopIdFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mechanical API", Version = "v1" });
    c.ExampleFilters();
    c.EnableAnnotations();
    c.OperationFilter<SwaggerDefaultWorkshopIdFilter>();
});

var origins = builder.Configuration.GetSection("Origins").Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader()
    );
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateCustomerDtoExample>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IRepairOrderService, RepairOrderService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API Mecánica v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }
