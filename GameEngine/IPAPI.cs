using GameEngine.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameEngine
{
    public class IPAPI
    {
        public static async Task <string?> GetIP()
        {
            string? ip = await HttpGet("https://api.ipify.org");
          
            return ip;
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

        


    }
}
