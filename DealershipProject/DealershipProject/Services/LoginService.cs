using DealershipProject.Shared.Models;
using DealershipProject.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DealershipProject.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private const string LoginUrl = "https://handsonlabapi20250807134636-gjfahzdkfug4b9cs.indonesiacentral-01.azurewebsites.net/";

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{LoginUrl}api/Usman/login", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        return new AuthResponse
                        {
                            Success = true,
                            Email = loginResponse.Email,
                            Token = loginResponse.Token,
                            ExpiresAt = DateTime.UtcNow.AddHours(1), // Default expiration, you can decode JWT to get actual expiration
                            Message = "Login successful"
                        };
                    }
                }
                return new AuthResponse { Success = false, Message = $"Login failed: {responseContent}" };
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Login error: {ex.Message}"
                };
            }
        }
    }
}
