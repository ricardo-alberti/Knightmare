using Knightmare.Boards;
using System.Diagnostics;
using Knightmare.Views;

namespace Knightmare.Tests
{
    internal sealed class PerformanceTest
    {
        private readonly Robot white;
        private readonly Robot black;
        private readonly View view;
        private Board position;

        public PerformanceTest()
        {
            position = BoardParser.Create();
            white = new Robot(2);
            black = new Robot(2);
            view = new View();
        }

        public void Execute()
        {
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 100; ++i)
            {
                for (int j = 0; j < 20; ++j)
                {
                    white.Play(position);
                    black.Play(position);
                }

                position = BoardParser.Create();
            }
            watch.Stop();

            view.PrintBoard(position);
            Console.WriteLine($"Time Elapsed: {watch.ElapsedMilliseconds}");
            Console.Read();
        }
    }
}
