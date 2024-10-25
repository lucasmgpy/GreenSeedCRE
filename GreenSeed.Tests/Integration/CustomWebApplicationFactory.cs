// GreenSeed.Tests/Integration/CustomWebApplicationFactory.cs
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GreenSeed.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.IO;

namespace GreenSeed.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Define o ambiente como "Testing"
            builder.UseEnvironment("Testing");

            // Define o ContentRoot para o diretório do projeto web
            builder.UseContentRoot(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../GreenSeed")));

            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                // Adiciona configurações em memória para testes
                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "InMemoryDbForTesting" },
                    // As configurações de BlobStorage são ignoradas para evitar erros
                });
            });

            builder.ConfigureServices(services =>
            {
                // Remove a configuração existente do DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adiciona o DbContext usando uma base de dados em memória
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Configura a autenticação de teste
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
            });
        }
    }
}
