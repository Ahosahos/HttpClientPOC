// 1️⃣ BAD PRACTICE: HttpClientDirectApp/Program.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        for (int i = 0; i < 100; i++)
        {
            using var client = new HttpClient(); // ❌ Creates a new connection each time
            client.DefaultRequestHeaders.UserAgent.ParseAdd("HttpClientDirectApp");
            var response = await client.GetAsync("https://www.example.com");
            Console.WriteLine($"Request {i + 1}: {response.StatusCode}");
            await Task.Delay(100); // Add delay to increase chance of TIME_WAIT
        }
    }
}