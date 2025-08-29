using DealershipProject.Shared.Models;
using DealershipProject.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DealershipProject.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        private const string BaseUrl = "https://handsonlabapi20250807134636-gjfahzdkfug4b9cs.indonesiacentral-01.azurewebsites.net/";

        public CarService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            try
            {
                await SetAuthorizationHeaderAsync();
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
                System.Diagnostics.Debug.WriteLine($"Unexpected error fetching cars: {ex.Message}");
                return new List<Car>();
            }
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
        {

            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/api/cars/{carId}");
                var car = JsonSerializer.Deserialize<Car>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return car;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching car {carId}: {ex.Message}");
                return null;
            }
        }

        public async Task<Car?> CreateCarAsync(Car car)
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var json = JsonSerializer.Serialize(car);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}api/cars", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdCar = JsonSerializer.Deserialize<Car>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return createdCar;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating car: {ex.Message}");
                return null;
            }
        }

        public async Task<Car?> UpdateCarAsync(int carId, Car car)
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var json = JsonSerializer.Serialize(car);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{BaseUrl}api/cars/{carId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var updatedCar = JsonSerializer.Deserialize<Car>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return updatedCar;
                }
                return null;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating car {carId}: {ex.Message}");
                return null;
            }
        }

        public Task<bool> DeleteCarAsync(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
