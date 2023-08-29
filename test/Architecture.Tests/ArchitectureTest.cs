using NetArchTest.Rules;

namespace Architecture.Tests
{
    public class ArchitectureTest
    {
        private static string OrderingApiNamespace = "Ordering.Api";
        private static string OrderingApplicationNamespace = "Ordering.Application";
        private static string OrderingDomainNamespace = "Ordering.Domain";
        private static string OrderingInfrastructureNamespace = "Ordering.Infrastructure";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            //Arrange
            var assembly = typeof(Ordering.Domain.Entities.Order).Assembly;

            var otherProjects = new[]
            {
                OrderingApplicationNamespace, OrderingApiNamespace, OrderingInfrastructureNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            //Assert
            Assert.True(testResult.IsSuccessful);

        }


        [Fact]
        public void Handlers_Should_HaveDependencyOnDomain()
        {
            //Arrange
            var assembly = typeof(Ordering.Application.ApplicationServiceRegistration).Assembly;

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(OrderingDomainNamespace)
                .GetResult();

            //Assert
            Assert.True(testResult.IsSuccessful);

        }
    }
}