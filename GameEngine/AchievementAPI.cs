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
    public static class AchievementAPI
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

        public static async Task<AchievementDTO[]> GetAchievement(string name)
        {
            string? response = await HttpGet("https://localhost:7209/api/PlayerAchievement/getnewachievement", "playerName=" + name);
            if (response != null)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var leaders = JsonSerializer.Deserialize<AchievementDTO[]>(response, options);
                return leaders ?? Array.Empty<AchievementDTO>();
            }

            return Array.Empty<AchievementDTO>();

        }
    }
}
