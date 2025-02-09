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

        public PuzzleTest()
        {
            view = new View();
            depthLevel = new int[] { 4 };
            puzzles = new Dictionary<string, string>()
            {
                {"B2rK2R/P1P3PP/pP3P1N/1pN5/2p1pq2/5nb1/b4ppp/6k1 w - - 0 0",
                 "B2K3R/P1P3PP/pP3P1N/1pN5/2p1pq2/5nb1/b4ppp/6k1 b - - 0 0"}
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
                    Board board = Board.Create(puzzle);
                    MoveStats calculation = bot.Calculate(board);
                    view.PrintBoard(board, calculation);

                    bot.Play(board);
                    string result = board.FEN();

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
            Console.Read();
        }
    }
}
