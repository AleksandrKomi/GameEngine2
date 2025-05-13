using GameEngine.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameEngine
{
    public class GeoAPI
    {
        private static async Task<string?> HttpGet(string url, string args = "")
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync($"{url}?{args}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    return null;
                }
            }
        }
       
      /*  public async Task<WeatherDTO?> GetWeather(float lat, float lon)
        {
            string? response = await HttpGet("https://localhost:7209/api/Weather/weatherService", $"lat={lat}&lon={lon}");
            if (response != null)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var weather = JsonSerializer.Deserialize<WeatherDTO>(response, options);

                return weather;
            }

            return null;
        }*/

        public static async Task<WeatherDTO?> GetLocationWeather(string nameCity)
        {
            string? response = await HttpGet("https://localhost:7209/api/Weather/LocationWeather", $"nameCity={nameCity}");
            if (response != null)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var city = JsonSerializer.Deserialize<WeatherDTO>(response, options);

                return city;
            }

            return null;
        }

        public static async Task GetLocationByIP(string ip)
        {
            string? response = await HttpGet("https://localhost:7209/api/Geocoding/LocationIP", $"ip={ip}");
        }

        public static async Task<string?> GetPointLocation(string ip)
        {
            string? response = await HttpGet($"https://localhost:7209/api/Geocoding/PointLocation", $"ip={ip}");

            return response;
        }

        

        private static async Task<string?> HttpPost(string url, string jsonContent)
        {
            using (HttpClient client = new HttpClient())
            {

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    return null;
                }
            }
        }

    }
}
