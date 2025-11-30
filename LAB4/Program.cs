using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab4GithubApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("Lab4GithubApiApp");

            var url = "https://api.github.com/orgs/dotnet/repos";

            string json = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<GithubRepo> repos = JsonSerializer.Deserialize<List<GithubRepo>>(json, options) ?? new List<GithubRepo>();

            foreach (var repo in repos)
            {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Homepage: {repo.Homepage}");
                Console.WriteLine($"GitHub: {repo.HtmlUrl}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"Last push: {repo.PushedAt:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
