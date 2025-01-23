using static BLAZOR_PRACTIKA_1.NDK_01_0U.ApiRequest.User;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BLAZOR_PRACTIKA_1.NDK_01_0U.ApiRequest
{
    public class ApiRequestService
    {
        public readonly HttpClient _httpClient;
        public readonly ILogger<ApiRequestService> _logger;

        public ApiRequestService(HttpClient httpClient, ILogger<ApiRequestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Регистрация пользователя
        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/UsersLogins/createNewUserAndLogin", request);
            return response.IsSuccessStatusCode;
        }

        // Авторизация пользователя
        public async Task<User> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/UsersLogins/login", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        // Получить всех пользователей (для администратора)
        public async Task<User[]> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<User[]>("api/UsersLogins/getAllUsers");
        }

        // Получить профиль пользователя
        public async Task<User> GetUserProfileAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/UsersLogins/getUserProfile/{userId}");
        }

        // Удалить пользователя (для администратора)
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var response = await _httpClient.DeleteAsync($"api/UsersLogins/deleteUser/{userId}");
            return response.IsSuccessStatusCode;
        }
    }

}
