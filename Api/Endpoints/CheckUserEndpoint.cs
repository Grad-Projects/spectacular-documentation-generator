using DocumentGeneration.BFF.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class CheckUserEndpoint
    {

        public static IEndpointRouteBuilder AddCheckUserEndPoint(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPut("checkUser", (
                   [FromQuery] string userName,
                   [FromServices] IDatabaseQueries _checkuser
                    )
               => _checkuser.checkUser(userName)
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
