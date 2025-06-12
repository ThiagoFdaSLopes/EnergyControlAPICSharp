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
        
        [Fact]
        public async Task GetById_ReturnsHttpStatusCode200()
        {
            // Arrange
            var request = "/api/Sectors/1"; // Supondo que o ID 1 exista
            // Act
            var response = await _client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode(); // Verifica se o status code é 200
        }

        [Fact]
        public async Task Create_ReturnsHttpStatusCode201()
        {
            // Arrange
            var request = "/api/Sectors";
            var content = new StringContent("{\"name\":\"Test Sector\",\"floorNumber\":1,\"description\":\"Test Description\"}", System.Text.Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync(request, content);
            // Assert
            Assert.Equal(201, (int)response.StatusCode); // Verifica se o status code é 201 Created
        }
    }
}
