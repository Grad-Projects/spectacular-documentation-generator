using Microsoft.AspNetCore.Mvc;
using DocumentGeneration.BFF.Core.Interfaces;
using System.IO.Compression;

namespace Api.Endpoints
{
    public static class SendZipFileEndpoint
    {
        public static IEndpointRouteBuilder AddZipFileEndpoint(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost("generate/folder_documentation", (
                   [FromBody] ZipArchive zipArchive,
                   [FromServices] IGenerateDocumentationUsecase _generateDocumentation
                    )
               => _generateDocumentation.AnalyzeFolder(zipArchive)
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
