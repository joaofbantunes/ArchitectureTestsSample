using Api.Features.Hellos.Application;
using Api.Features.Hellos.Domain;

namespace Api.Features.Hellos.Infrastructure;

public sealed class HelloRepository : IHelloRepository
{
    public void Save(Hello hello) => throw new NotImplementedException();
}