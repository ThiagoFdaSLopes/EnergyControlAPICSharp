using System.Net;
using System.Net.Http.Json;
using Xunit;
using EnergyControlAPI.Helpers;
using EnergyControlAPI.DTOs;

namespace EnergyControlAPI.Tests
{
    public class UserControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UserControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_Returns200AndValidStructure()
        {
            // Arrange
            var url = "/api/User?pageNumber=1&pageSize=5";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var paged = await response.Content.ReadFromJsonAsync<PagedResponse<UserDto>>();
            Assert.NotNull(paged);
            Assert.Equal(1, paged.PageNumber);
            Assert.Equal(5, paged.PageSize);
        }
    }
}
