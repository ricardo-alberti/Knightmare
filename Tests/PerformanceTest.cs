using System.Diagnostics;
using Knightmare.Views;

namespace Knightmare.Tests
{
    internal sealed class PerformanceTest
    {
        private const int movesToPlay = 30;
        private const int botLevel = 7;
        private readonly View view;
        private Board position;

        /*
        public PerformanceTest()
        {
            position = BoardParser.Create();
            bot = new Engine(botLevel);
            view = new View();
        }

        public void Execute()
        {
            var watch = Stopwatch.StartNew();

            for (int j = 0; j < movesToPlay; ++j)
            {
                bot.Play(position);

                if (position.Terminal)
                    break;
            }

            watch.Stop();

            view.PrintBoard(position);
            Console.WriteLine($"Time elapsed per move: {watch.ElapsedMilliseconds/movesToPlay}");
            Console.WriteLine($"Time elapsed total: {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Moves played: {movesToPlay}");
            Console.WriteLine($"Bot level: {botLevel}");
            Console.Read();
        }
    }
    */
    }
}
