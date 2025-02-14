using Knightmare.Boards;
using Knightmare.DTO;
using Knightmare.Views;

namespace Knightmare.Tests
{
    internal sealed class PuzzleTest
    {
        private readonly int[] depthLevel;
        private readonly Dictionary<string, string> puzzles;
        private readonly View view;
        private readonly MoveTreeView moveTree;

        public PuzzleTest()
        {
            view = new View();
            moveTree = new MoveTreeView();
            depthLevel = new int[] { 6 };
            puzzles = new Dictionary<string, string>()
            {
                {"6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2rK2R w - - 0 0",
                 "6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2K3R b - - 0 0"},
                {"rnbqkb1r/ppp2B2/3p1p2/4p3/3PP1p1/2N2Q1P/PPP1NP1P/R1B2K1R b - - 0 0",
                 "rnbq1b1r/ppp2k2/3p1p2/4p3/3PP1p1/2N2Q1P/PPP1NP1P/R1B2K1R w - - 0 0"},
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

                    MoveStats calculation = bot.Calculate(board);
                    view.PrintBoard(board, calculation);
                    moveTree.PrintAllMoveTrees(bot.GetInitialMoveTrees(board, level));

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

                    calculation = bot.Calculate(board);
                    view.PrintBoard(board, calculation);
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
