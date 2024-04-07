using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace Api.Configuration.Swagger
{
    public class EnumHelper : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(name =>
                    {
                        var member = context.Type.GetMember(name).First();
                        var descriptionAttribute = member.GetCustomAttribute<DescriptionAttribute>();
                        if (descriptionAttribute != null)
                        {
                            schema.Enum.Add(new OpenApiString(descriptionAttribute.Description));
                        }
                        else
                        {
                            schema.Enum.Add(new OpenApiString(name));
                        }
                    });
            }
        }


    }
}
