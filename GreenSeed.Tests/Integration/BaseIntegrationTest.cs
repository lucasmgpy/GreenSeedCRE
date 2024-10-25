using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using GreenSeed;
using GreenSeed.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Authentication;
using Xunit;

namespace GreenSeed.Tests.Integration
{
    public class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly CustomWebApplicationFactory<Program> _factory;

        public BaseIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
    }
}
