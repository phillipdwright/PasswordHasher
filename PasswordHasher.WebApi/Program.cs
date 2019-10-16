using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PasswordHasher.WebApi
{
    public class Program
    {
        private static CancellationTokenSource WebHostTokenSource = new CancellationTokenSource();
        public async static Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync(WebHostTokenSource.Token);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void ShutDown() => WebHostTokenSource.Cancel();
    }
}
