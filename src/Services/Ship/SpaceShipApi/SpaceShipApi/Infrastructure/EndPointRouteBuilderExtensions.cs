namespace Api.Infrastructure;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGet(
        this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", params string[] policyNames)
    {
        builder.MapGet(pattern, handler)
            .WithName(handler.Method.Name)
            .AddAuthorizationIfNeeded(policyNames);

        return builder;
    }

    public static IEndpointRouteBuilder MapPost(
        this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", params string[] policyNames)
    {
        builder.MapPost(pattern, handler)
            .WithName(handler.Method.Name)
            .AddAuthorizationIfNeeded(policyNames);

        return builder;
    }

    public static IEndpointRouteBuilder MapPut(
        this IEndpointRouteBuilder builder, Delegate handler, string pattern, params string[] policyNames)
    {
        builder.MapPut(pattern, handler)
            .WithName(handler.Method.Name)
            .AddAuthorizationIfNeeded(policyNames);

        return builder;
    }

    public static IEndpointRouteBuilder MapDelete(
        this IEndpointRouteBuilder builder, Delegate handler, string pattern, params string[] policyNames)
    {
        builder.MapDelete(pattern, handler)
            .WithName(handler.Method.Name)
            .AddAuthorizationIfNeeded(policyNames);

        return builder;
    }

    private static RouteHandlerBuilder AddAuthorizationIfNeeded(this RouteHandlerBuilder builder, params string[] policyNames)
    {
        return policyNames.Length == 0
            ? builder
            : builder.RequireAuthorization(policyNames);
    }
}