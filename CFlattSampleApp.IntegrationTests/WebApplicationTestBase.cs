using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CFlattSampleApp.IntegrationTests;

public class WebApplicationTestBase : IDisposable
{
    public CFlattSampleAppDbContext Context { get; private set; }
    public WebApplicationFactory<Program> WebApplication { get; private set; }
    public HttpClient Client { get; private set; }

    readonly IServiceScope scope;

    /// <summary>
    /// Sets connection database to CFlattSampleAppTest"
    /// </summary>
    public WebApplicationTestBase()
    {
        // "Data Source=localhost;Initial Catalog=CFlattSampleAppTest;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False"
        WebApplication = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration(config =>
                {
                    var connectionString = config.Build().GetConnectionString("CFlattSampleAppDb");
                    if (connectionString == null)
                        throw new NullReferenceException("connectionString cannot be null");
                    var parts = connectionString.Split(';', StringSplitOptions.TrimEntries);
                    connectionString = "";
                    string[] tokens = { "Initial Catalog", "Database" };

                    foreach (var part in parts)
                    {
                        bool found = false;
                        foreach (var token in tokens)
                        {
                            if (part.StartsWith(token, StringComparison.OrdinalIgnoreCase))
                            {
                                connectionString += $"{token}=CFlattSampleAppTest;";
                                found = true;
                                continue;
                            }
                        }
                        if (!found)
                            connectionString += part + ";";
                    }

                    List<KeyValuePair<string, string?>> appsettings = new()
                    {
                    new("ConnectionStrings:CFlattSampleAppDb", connectionString)
                    };
                    config.AddInMemoryCollection(appsettings);
                }
                );
            });

        Client = WebApplication.CreateClient();

        scope = WebApplication.Services.CreateScope();
        Context = scope.ServiceProvider.GetService<CFlattSampleAppDbContext>() 
            ?? throw new NullReferenceException("Service not found");
        
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
        new DatabaseSeeder().Seed(Context);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        scope.Dispose();
        GC.SuppressFinalize(this);
    }
}
