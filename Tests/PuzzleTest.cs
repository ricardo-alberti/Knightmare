using Knightmare.Boards;
using Knightmare.Views;

namespace Knightmare.Tests
{
    internal sealed class PuzzleTest
    {
        private readonly int[] depthLevel;
        private readonly Dictionary<string, string> puzzles;
        private readonly View view;

        public PuzzleTest()
        {
            view = new View();
            depthLevel = new int[] { 4 };
            puzzles = new Dictionary<string, string>()
            {
                {"6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2rK2R w - - 0 0",
                 "6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2K3R b - - 0 0"},
                {"rnb1kbnr/pppp1ppp/4p3/8/4P2P/2N5/PPPP1qP1/R1BQKBNR w - - 0 0",
                 "rnb1kbnr/pppp1ppp/4p3/8/4P2P/2N5/PPPP1KP1/R1BQ1BNR b - - 0 0" }
            };
        }

        public void Execute()
        {
            int tested = 0;
            int passed = 0;
            int failed = 0;

            foreach (var pair in puzzles)
            {
                string puzzle = pair.Key;
                string solution = pair.Value;

                foreach (int level in depthLevel)
                {
                    Console.Clear();

                    Robot bot = new(level);
                    Board board = new();
                    board = BoardParser.Create(puzzle);

                    view.PrintBoard(board);

                    bot.Play(board);
                    string result = BoardParser.FEN(board);

                    tested++;

                    if (result == solution)
                    {
                        passed++;
                        continue;
                    }

                    Console.WriteLine($"TEST FAIL: {level}");
                    Console.WriteLine($"Received: {puzzle} -> {result}");
                    Console.WriteLine($"Expected: {puzzle} -> {solution}");
                    failed++;

                    view.PrintBoard(board);
                    Console.Read();
                }
            }

            Console.WriteLine($"TESTS PASSED: {passed}");
            Console.WriteLine($"TESTS FAILED: {failed}");
            Console.WriteLine($"TESTS EXECUTED: {tested}");
            Console.WriteLine($"Puzzle Tests Completed");
            Console.Read();
        }
    }
}
