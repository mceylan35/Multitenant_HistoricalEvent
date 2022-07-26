using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenant.Api.Filter
{
    public class MyHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null && !descriptor.ControllerName.StartsWith("Weather"))
            {
                 
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "tenant",
                    In = ParameterLocation.Header,
                    Description = "tenant",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "lang",
                    In = ParameterLocation.Header,
                    Description = "lang",
                    Required = false
                }); 
 
            }
        }
    }
}
