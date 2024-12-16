using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace WorstMovieTests;

public class ProducersTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetRange()
    {
        WebApplicationServices application = new WebApplicationServices();

        using var client = application.CreateClient();

        client.BaseAddress = new Uri("https://localhost:7135");

        var response = await client.GetAsync("/producer/range-awards");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}