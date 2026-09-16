using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Claims.Tests
{
    public class CoversControllerTests
    {
        [Fact]
        public async Task ComputePremium()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ =>
                { });

            var client = application.CreateClient();

            var startDate = new DateTime(2026, 1, 1).ToString("yyyy-MM-dd");
            var endDate = new DateTime(2026, 1, 31).ToString("yyyy-MM-dd");
            var coverType = 1;

            var response = await client.PostAsync($"/Covers/compute?startDate={startDate}&endDate={endDate}&coverType={coverType}", null, TestContext.Current.CancellationToken);
        
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var premium = await response.Content.ReadFromJsonAsync<decimal>(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(41250m, premium);
        }
    }
}
