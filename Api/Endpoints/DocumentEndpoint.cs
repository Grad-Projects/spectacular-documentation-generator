using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class DocumentEndpoint
    {
        public static IEndpointRouteBuilder AddDocumentEndpoint(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapGet("generate/documentation", (
                   [FromBody] byte[] document
                    )
               => () => "hello world"
            )
           .Produces(StatusCodes.Status200OK, typeof(string))
           .Produces(StatusCodes.Status500InternalServerError)
           .Produces(StatusCodes.Status400BadRequest)
           .Produces(StatusCodes.Status403Forbidden)
           .WithTags("Documents");


            return endpoints;
        }
    }
}
