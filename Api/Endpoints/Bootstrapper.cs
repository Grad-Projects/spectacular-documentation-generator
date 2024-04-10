namespace Api.Endpoints
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddApiEndpoints(this IServiceCollection services)
        {
            return services;
        }

        public static IEndpointRouteBuilder UseApiEndpoints(this IEndpointRouteBuilder endpoints)
        {
            //add endpoints here
            var versionedApi = endpoints.NewVersionedApi();
            versionedApi.MapGroup("/api")
            .AddDocumentEndpoint()
            .AddCheckUserEndPoint();

            return endpoints;
        }
    }
}
