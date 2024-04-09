using Microsoft.AspNetCore.Mvc;
using DocumentGeneration.BFF.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Api.Endpoints
{
    public static class DocumentEndpoint
    {
        public static IEndpointRouteBuilder AddDocumentEndpoint(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapGet("generate/documentation", (
                   [FromBody] List<string> document,
                   [FromHeader] string Username,
                   [FromQuery] string styeName,
                   [FromServices] IGenerateDocumentationUsecase _generateDocumentation
                    )
               => _generateDocumentation.GenDocumentation(document, styeName, Username)
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
