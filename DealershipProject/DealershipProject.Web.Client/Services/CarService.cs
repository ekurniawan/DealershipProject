using DealershipProject.Shared.Models;
using DealershipProject.Shared.Services;
using System.Text.Json;

namespace DealershipProject.Web.Client.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://handsonlabapi20250807134636-gjfahzdkfug4b9cs.indonesiacentral-01.azurewebsites.net/";

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            try
            {
                var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJ1ZGlAZ21haWwuY29tIiwicm9sZSI6WyJzYWxlcyIsImFkbWluIl0sIm5iZiI6MTc1NjQzMjk2MCwiZXhwIjoxNzU2NDM2NTYwLCJpYXQiOjE3NTY0MzI5NjB9.mxPIUurlniUvZwlDzBDxkoK1Ks_ehlehXoE4HuSWKI4";
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetStringAsync($"{BaseUrl}api/cars");
                var cars = JsonSerializer.Deserialize<List<Car>>(response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return cars ?? new List<Car>();
            }
            catch (HttpRequestException httpEx)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP error fetching cars: {httpEx.Message}");
                return new List<Car>();
            }
            catch (JsonException jsonEx)
            {
                System.Diagnostics.Debug.WriteLine($"JSON parsing error: {jsonEx.Message}");
                return new List<Car>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected error fetching cars: {ex.Message}");
                return new List<Car>();
            }
        }
    }
}