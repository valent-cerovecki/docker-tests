using System.Runtime.CompilerServices;
using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.Hosting;
using TestContainers.Container.Database.MsSql;

namespace Docker_tests.Tests;

internal class ContainerInitializer
{
    internal static string? ConnectionString { get; private set; }
    private const string TestDatabase = "Test";


    [ModuleInitializer]
    internal static void InitializeContainer()
    {
        var container = new ContainerBuilder<MsSqlContainer>()
            .ConfigureDockerImageName("mcr.microsoft.com/mssql/server:2022-latest")
            .ConfigureDatabaseConfiguration("sa", "Password123#", TestDatabase)
            .Build();

        container.StartAsync().Wait();

        ConnectionString = container.GetConnectionString(TestDatabase) + ";TrustServerCertificate=True";
    }
}
