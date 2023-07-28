using Api.Features.Hellos.Domain;

namespace Api.Features.Hellos.Application;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapHellos(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/v1/hellos");

        group.MapPost("", CreateHello);
        
        return builder;
    }

    private static IResult CreateHello(CreateHelloDto dto, IHelloRepository repository)
    {
        var newHello = Hello.New(dto.Greeting, dto.GreeterName);
        repository.Save(newHello);
        return Results.Created();
    }
}