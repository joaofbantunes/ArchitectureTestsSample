using Api.Features.Hellos.Domain;

namespace Api.Features.Hellos.Application;

public interface IHelloRepository
{
    void Save(Hello hello);
}