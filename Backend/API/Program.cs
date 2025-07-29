using API.Middlewares;
using API.Swagger;
using Application;
using Infraestructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static API.Swagger.DefaultWorksShopIdFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//if (builder.Environment.EnvironmentName == "Development")
//{
//    builder.WebHost.ConfigureKestrel(serverOptions =>
//    {
//        serverOptions.ListenAnyIP(7027);
//    });
//}

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

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
