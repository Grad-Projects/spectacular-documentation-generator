using DocumentGeneration.BFF.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class CheckUserEndpoint
    {

        public static IEndpointRouteBuilder AddCheckUserEndPoint(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPut("checkUser", (
                   [FromHeader] string Username,
                   [FromServices] IDatabaseQueries _checkuser
                    )
               => _checkuser.checkUser(Username)
            )
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status500InternalServerError)
           .Produces(StatusCodes.Status400BadRequest)
           .Produces(StatusCodes.Status403Forbidden)
           .WithTags("Documents");


            return endpoints;
        }
    }
}
