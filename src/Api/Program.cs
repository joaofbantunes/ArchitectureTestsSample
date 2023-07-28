using Api.Features.Hellos.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IHelloRepository, IHelloRepository>();

var app = builder.Build();

app.MapHellos();

app.Run();

public sealed partial class Program
{
}