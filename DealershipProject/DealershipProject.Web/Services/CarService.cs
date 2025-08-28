using DealershipProject.Shared.Models;
using DealershipProject.Shared.Services;
using System.Text.Json;

namespace DealershipProject.Web.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        //private readonly IAuthService _authService;
        private const string BaseUrl = "https://handsonlabapi20250807134636-gjfahzdkfug4b9cs.indonesiacentral-01.azurewebsites.net/";

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_authService = authService;
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            try
            {
                var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJ1ZGlAZ21haWwuY29tIiwicm9sZSI6WyJzYWxlcyIsImFkbWluIl0sIm5iZiI6MTc1NjM2NzQzOSwiZXhwIjoxNzU2MzcxMDM5LCJpYXQiOjE3NTYzNjc0Mzl9.HZ6P8LcsbFW7y_liLbrFA1yyUXyrTyTWqzQJNHUF9_I";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetStringAsync($"{BaseUrl}api/cars");
                var cars = JsonSerializer.Deserialize<List<Car>>(response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return cars ?? new List<Car>();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                System.Diagnostics.Debug.WriteLine($"Error fetching cars: {ex.Message}");
                return new List<Car>();
            }
        }
    }
}