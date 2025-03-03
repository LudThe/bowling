using bowling.Factories;
using bowling.Interfaces;
using bowling.Repo;
using Microsoft.Extensions.Logging;

namespace bowling.Models
{
    class BowlingGame
    {
        IMember? CurrentMember { get; set; }

        private readonly ILogger<BowlingGame> _logger;

        public BowlingGame(ILogger<BowlingGame> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            Console.WriteLine("Enter name");
            Console.Write("\r\n> ");
            string name = Console.ReadLine() ?? string.Empty;

            _logger.LogInformation("Checking name in database...");

            List<IMember> members = FileHandler.ReadFile();
            CurrentMember = members.Find(memb => memb.Name.ToLower() == name.ToLower());

            if (CurrentMember != null)
            {
                Console.WriteLine($"\r\nWelcome back {CurrentMember.GetMemberDescription()}!");
                RunMatch();
            }
            else
            {
                RegisterNewMember(name, members);
            }
        }


        private void RunMatch()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                Console.WriteLine("Run match?");
                Console.Write("\r\n> ");
                string response = Console.ReadLine() ?? string.Empty;

                int human = 0;
                int cpu = 0;

                if (response.ToLower() == "yes")
                {
                    _logger.LogInformation("Simulating match...");

                    for (int i = 1; i < 11; i++)
                    {
                        human += RunTurn(CurrentMember.GetMemberDescription(), i);
                        cpu += RunTurn("CPU", i);
                    }

                    _logger.LogInformation("Match is over");

                    Console.WriteLine("\r\n");
                    Console.WriteLine($"Final score: \r\n" +
                                    $"{CurrentMember.GetMemberDescription()}: {human} points \r\n" +
                                    $"CPU player: {cpu} points");

                    if (cpu > human)
                    {
                        Console.WriteLine($"{CurrentMember.GetMemberDescription()} lost");
                    }
                    else if (cpu == human)
                    {
                        Console.WriteLine("Game ended in a tie");
                    }
                    else
                    {
                        Console.WriteLine($"{CurrentMember.GetMemberDescription()} won!");
                    }
                    Console.WriteLine("\r\n");
                }
                else
                {
                    gameOver = true;
                }
            }

            Console.WriteLine("Bye!");
            Console.ReadLine();
        }


        private int RunTurn(string player, int turnNumber)
        {
            Random random = new Random();
            int score = random.Next(0, 31);
            _logger.LogInformation($"Turn {turnNumber}: {player} scored {score} points");
            return score;
        }


        private void RegisterNewMember(string name, List<IMember> members)
        {
            Console.WriteLine("\r\n");
            Console.WriteLine($"Is {name} a VIP?");
            Console.Write("\r\n> ");
            string response = Console.ReadLine() ?? string.Empty;

            // factory pattern used here to create a new member
            IMember newMember = MemberFactory.Create(name, response.ToLower() == "yes" ? MemberType.VIP : MemberType.Regular);
            members.Add(newMember);

            _logger.LogInformation("Saving member to database...");
            FileHandler.WriteToFile(members);

            CurrentMember = newMember;
            Console.WriteLine("\r\n");
            Console.WriteLine($"Welcome {CurrentMember.GetMemberDescription()}!");

            RunMatch();
        }
    }
}
