using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpClient("MyClient", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("HttpClientFactoryApp");
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new SocketsHttpHandler
            {
                MaxConnectionsPerServer = 10, // Limit concurrent connections
                PooledConnectionLifetime = TimeSpan.FromMinutes(2) // Ensure reuse
            };
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

        for (int i = 0; i < 100; i++)
        {
            var client = httpClientFactory.CreateClient("MyClient");
            var response = await client.GetAsync("https://www.example.com");
            Console.WriteLine($"Request {i + 1}: {response.StatusCode}");
            await Task.Delay(100); // Simulate real-world delays
        }
    }
}
