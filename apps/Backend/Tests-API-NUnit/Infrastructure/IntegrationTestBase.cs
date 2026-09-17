using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API_NUnit_Tests.Infrastructure;

public abstract class IntegrationTestBase : IDisposable
{
    protected CustomApiFactory Factory;
    protected HttpClient Client;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Factory = new CustomApiFactory();
        await Factory.DbContainer.StartAsync();
        
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    [SetUp]
    public void SetUp()
    {
        Client = Factory.CreateClient();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Factory.DbContainer.StopAsync();
        await Factory.DbContainer.DisposeAsync();
        Dispose();
    }

    public void Dispose()
    {
        Factory.Dispose();
        Client.Dispose();
    }
}