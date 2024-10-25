using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using GreenSeed;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using GreenSeed.Data;
using GreenSeed.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace GreenSeed.Tests.Integration
{
    public class ProductControllerIntegrationTests : BaseIntegrationTest
    {
        private readonly HttpClient _client;

        public ProductControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }

        [Fact]
        public async Task Index_Returns_Product_List()
        {
            // Arrange
            // Seed data
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Products.AddRange(
                    new Product { Name = "Product 1", Price = 10.0M },
                    new Product { Name = "Product 2", Price = 20.0M }
                );
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/Product/Index");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Product 1", responseString);
            Assert.Contains("Product 2", responseString);
        }

        [Fact]
        public async Task AddEdit_Get_Returns_ViewResult_For_New_Product()
        {
            // Act
            var response = await _client.GetAsync("/Product/AddEdit?id=0");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Adicionar", responseString); // Verifica se a operação é "Adicionar"
        }
    }
}
