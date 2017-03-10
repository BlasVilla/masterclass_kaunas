using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace ResultsMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseUrls(GetUseUrls())
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        private static string GetUseUrls()
        {
            string result;

            var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");

            if (!string.IsNullOrWhiteSpace(urls))
            {
                result = urls;
            }
            else
            {
                result = @"http://*:80";

                var virtualDirectory = Environment.GetEnvironmentVariable("Hosting__VirtualDirectory");

                if (!string.IsNullOrWhiteSpace(virtualDirectory))
                {
                    result += $"/{virtualDirectory}";
                }
            }

            return result;
        }
    }
}
