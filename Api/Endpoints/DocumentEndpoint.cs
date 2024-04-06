using Microsoft.AspNetCore.Mvc;
using DocumentGeneration.BFF.Core.Interfaces;

namespace Api.Endpoints
{
    public static class DocumentEndpoint
    {
        public static IEndpointRouteBuilder AddDocumentEndpoint(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost("generate/documentation", (
                   [FromBody] byte[] document,
                   [FromServices] IGenerateDocumentationUsecase _generateDocumentation
                    )
               => _generateDocumentation.Analyze(document)
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
