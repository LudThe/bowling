using bowling.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace bowling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // dependency injection used here for logging
            ServiceCollection services = new();
            services.AddLogging(builder => builder.AddConsole());
            services.AddTransient<BowlingGame>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            BowlingGame bowlingGame = serviceProvider.GetRequiredService<BowlingGame>();
            bowlingGame.Run();
        }
    }
}
