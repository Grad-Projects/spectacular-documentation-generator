using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace DocumentGeneration.BFF.API.Middleware
{


    public class GithubAuthMiddleware
    {
        // XXX: HttpClient could probably be moved somewhere
        static HttpClient httpClient = new();

        private readonly RequestDelegate _next;
        public GithubAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var AuthHeader = context.Request.Headers.Authorization;
            if (AuthHeader.IsNullOrEmpty())
            {
                await WriteMessage(context, 403, "No Auth Header found, please login with GitHub");
            }
            else
            {
                if (!await GetUserAuth(AuthHeader))
                {
                    await WriteMessage(context, 403, $"Token {AuthHeader} is not valid");
                }
                else
                {
                    // Passed authentication, carry on!
                    await _next(context);
                }
            }
        }

        // Check if user is authenticated with GitHub
        public static async Task<bool> GetUserAuth(string AccessToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");

                var response = await httpClient.SendAsync(request);
                Log.Logger.Information($"Github Response for token {AccessToken}:\n{response}");
                // TODO: Why is this always a 403??
                return response.IsSuccessStatusCode;
            }
        }

        // Helper method to write a message
        public static async Task WriteMessage(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            var text = Encoding.UTF8.GetBytes(message);
            await context.Response.Body.WriteAsync(text);
        }
    }

    public static class GithubAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseGithubAuth(this IApplicationBuilder app)
        { 
            return app.UseMiddleware<GithubAuthMiddleware>();
        }
    }
}
