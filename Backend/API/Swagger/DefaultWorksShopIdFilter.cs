using Infraestructure.Context;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Swagger
{
    public class DefaultWorksShopIdFilter
    {
        public class SwaggerDefaultWorkshopIdFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                foreach (var param in operation.Parameters)
                {
                    if (param.Name == "workshopId")
                    {
                        param.Schema.Default = new OpenApiString(SeedData.workshopAId);
                    }
                }
            }
        }
    }
}
