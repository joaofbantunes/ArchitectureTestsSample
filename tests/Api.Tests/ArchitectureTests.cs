using NetArchTest.Rules;
using static Api.Tests.NetArchTestExtensions;

namespace Api.Tests;

public class ArchitectureTests
{
    [Fact]
    public void When_Developing_Domain_Types_They_Should_Not_Reference_Application_Types()
        => ApiTypes()
            .That()
            .ResideInDomainNamespaces()
            .ShouldNot()
            .HaveDependencyOnAny(
                ApiTypes()
                    .ApplicationNamespaces())
            .Evaluate();

    [Fact]
    public void When_Developing_Domain_Types_They_Should_Not_Reference_Infrastructure_Types()
        => ApiTypes()
            .That()
            .ResideInDomainNamespaces()
            .ShouldNot()
            .HaveDependencyOnAny(
                ApiTypes()
                    .InfrastructureNamespaces())
            .Evaluate();
    
    [Fact]
    public void When_Developing_Application_Types_They_Should_Not_Reference_Infrastructure_Types()
        => ApiTypes()
            .That()
            .ResideInApplicationNamespaces()
            .ShouldNot()
            .HaveDependencyOnAny(
                ApiTypes()
                    .InfrastructureNamespaces())
            .Evaluate();

    // I think this is a reasonable thing to enforce, but if I'm forgetting some valid situation, we can revisit
    [Fact]
    public void When_Developing_Non_Abstract_Or_Static_Types_Then_They_Should_Be_Sealed()
        => ApiTypes()
            .That()
            .AreClasses()
            .And()
            .AreNotAbstract() // static classes are considered abstract, so this suffices
            .Should()
            .BeSealed()
            .Evaluate();
}

file static class NetArchTestExtensions
{
    public static Types ApiTypes()
        => Types.InAssembly(typeof(Program).Assembly);

    public static PredicateList ResideInDomainNamespaces(this Predicates predicates)
        => predicates.ResideInNamespaceMatching( /*language=regexp*/".*\\.Domain.*");

    public static PredicateList ResideInApplicationNamespaces(this Predicates predicates)
        => predicates.ResideInNamespaceMatching( /*language=regexp*/".*\\.Application.*");

    public static PredicateList ResideInInfrastructureNamespaces(this Predicates predicates)
        => predicates.ResideInNamespaceMatching( /*language=regexp*/".*\\.Infrastructure.*");

    public static string[] ApplicationNamespaces(this Types types)
        => types
            .That()
            .ResideInApplicationNamespaces()
            .GetNamespaces();
    
    public static string[] InfrastructureNamespaces(this Types types)
        => types
            .That()
            .ResideInInfrastructureNamespaces()
            .GetNamespaces();

    public static void Evaluate(this ConditionList conditionList)
    {
        var failingTypeNames = conditionList.GetResult().FailingTypeNames ?? Array.Empty<string>();
        failingTypeNames.Should().BeEmpty();
    }

    private static string[] GetNamespaces(this PredicateList predicateList)
        => predicateList.GetTypes()
            .Where(t => t.Namespace is not null)
            .Select(t => t.Namespace!)
            .Distinct()
            .ToArray();
}