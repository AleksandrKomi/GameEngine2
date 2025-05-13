using GameEngine.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class LeaderAPI
    {
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

        public static async Task PostLeader(string name, int score)
        {
            var dto = new LeaderDTO()
            {
                Name = name,
                Score = score
            };

            string jsonContent = JsonSerializer.Serialize(dto);

            string? response = await HttpPost("https://localhost:7209/api/Leaderboard", jsonContent);
        }

        public static async Task PostStatistics(string name, int score)
        {
            var dto = new StatisticsDTO()
            {
               // DateTime = dateTime,
                Name = name,
                Score = score
            };

            string jsonContent = JsonSerializer.Serialize(dto);

            string? response = await HttpPost("https://localhost:7209/api/StatisticsControllers", jsonContent);
        }

        public static async Task<LeaderDTO[]> GetLeaders()
        {
            string? response = await HttpGet("https://localhost:7209/api/Leaderboard");
            if (response != null)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var leaders = JsonSerializer.Deserialize<LeaderDTO[]>(response, options);
                return leaders ?? Array.Empty<LeaderDTO>();
            }

            return Array.Empty<LeaderDTO>();
        }
    }

}
