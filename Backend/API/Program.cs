using backend.Modules.VehicleModule.Interfaces;
using backend.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static backend.Swagger.DefaultWorksShopIdFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

if (builder.Environment.EnvironmentName == "Development")
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(7027);
    });
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mechanical API", Version = "v1" });
    c.ExampleFilters();
    c.EnableAnnotations();
    c.OperationFilter<SwaggerDefaultWorkshopIdFilter>();
});

var origins =
    Environment.GetEnvironmentVariable("Origins")?.Split(",")
    ?? builder.Configuration.GetSection("Origins").Get<string[]>()
    ?? [];

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
builder.Services.AddDbContextFactory<AppDbContext>(
    options => options.UseNpgsql(connection),
    ServiceLifetime.Scoped
);

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//builder.Services.AddScoped<IVehicleService, VehicleService>();
//builder.Services.AddScoped<IRepairOrderService, RepairOrderService>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();
//builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API Mec?nica v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }
