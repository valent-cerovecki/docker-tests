using Docker_Tests.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Docker_tests.Tests;

public abstract class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient Client { get; }
    public ApplicationContext DbContext { get; }

    protected IntegrationTestBase(WebApplicationFactory<Program> factory)
    {
        factory = factory
            .WithWebHostBuilder(builder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                integrationConfig.GetSection("ConnectionStrings")["DefaultConnection"] =
                    ContainerInitializer.ConnectionString;

                builder.UseConfiguration(integrationConfig);
            });

        Client = factory.CreateClient();

        DbContext = factory.Services.CreateScope()
            .ServiceProvider.GetService<ApplicationContext>()!;
        DbContext!.Database.EnsureDeleted();
        DbContext!.Database.EnsureCreated();
    }

    protected async Task InsertData(IEnumerable<Person> people)
    {
        await DbContext.People.AddRangeAsync(people);
        await DbContext.SaveChangesAsync();
    }
}
