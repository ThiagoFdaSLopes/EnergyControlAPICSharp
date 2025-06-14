using EnergyControlAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EnergyControlAPI.Test
{
    public class EquipmentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EquipmentControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsHttpStatusCode200()
        {
            // Arrange
            var request = "/api/Equipment";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica se o status code é 200
        }

    }


    public class SectorsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public SectorsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetAll_ReturnsHttpStatusCode200()
        {
            // Arrange
            var request = "/api/Sectors";
            // Act
            var response = await _client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode(); // Verifica se o status code é 200
        }

    }
}
