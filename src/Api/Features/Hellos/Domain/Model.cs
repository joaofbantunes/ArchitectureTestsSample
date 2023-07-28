namespace Api.Features.Hellos.Domain;

public record struct HelloId
{
    private HelloId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException(
                message: "Value should not be default",
                paramName: nameof(value));
        }

        Value = value;
    }

    public Guid Value { get; }

    public static HelloId New() => new(Guid.NewGuid());
}

public sealed class Hello
{
    private Hello(HelloId id, string greeting, string greeterName)
    {
        Id = id;
        Greeting = greeting;
        GreeterName = greeterName;
    }

    public HelloId Id { get; }

    public string Greeting { get; }

    public string GreeterName { get; }

    public static Hello New(string greeting, string greeterName)
        => new(HelloId.New(), greeting, greeterName);
}