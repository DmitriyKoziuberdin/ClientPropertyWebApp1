using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ClientPropertyWebApp1.Client.Services.PropertyService
{
    public class PropertyService : IPropertyService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public PropertyService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Property> Properties { get; set; } = new List<Property>();
        public List<User> Users { get; set; } = new List<User>();           

        public async Task CreateProperty(Property property)
        {
            var result = await _http.PostAsJsonAsync("api/property", property);
            await SetProperty(result);
        }

        private async Task SetProperty(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Property>>();
            Properties = response;
            _navigationManager.NavigateTo("property");
        }

        public async Task DeleteProperty(int id)
        {
            var result = await _http.DeleteAsync($"api/property/{id}");
            await SetProperty(result);
        }

        public async Task GetProperties()
        {
            var result = await _http.GetFromJsonAsync<List<Property>>("api/property");
            if(result != null)
            {
                Properties = result;
            }
        }

        public async Task<Property> GetSingleProperty(int id)
        {
            var result = await _http.GetFromJsonAsync<Property>($"api/property/{id}");
            if (result != null)
                return result;
            throw new Exception("Property not found");
        }

        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/property/users");
            if (result != null)
            {
                Users = result;
            }
        }

        public async Task UpdateProperty(Property property)
        {
            var result = await _http.PutAsJsonAsync($"api/property/{property.Id}", property);
            await SetProperty(result);
        }
    }
}
