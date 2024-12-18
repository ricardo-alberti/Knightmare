using Knightmare.Boards;
using Knightmare.DTO;

namespace Knightmare.Tests
{
    internal sealed class PuzzleTest
    {
        private readonly int[] depthLevel;
        private readonly Dictionary<string, string> puzzles;

        public PuzzleTest()
        {
            depthLevel = new int[] { 1, 2, 3, 4 };

            puzzles = new Dictionary<string, string>()
            {
                {"8/PPPPPPPP/k7/8/8/8/8/8", "8/P1PPPPPP/P7/8/8/8/8/8/"},
                {"8/PPPPPPPP/kn6/8/8/8/8/8", "8/P1PPPPPP/Pn6/8/8/8/8/8/"},
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
                    Robot whiteBot = new(PlayerSide.White, level);
                    Board board = Board.Create(puzzle);
                    CalculationResponse calculation = whiteBot.Calculate(board);
                    whiteBot.Play(board);
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
                }
            }

            Console.WriteLine($"TESTS PASSED: {passed}");
            Console.WriteLine($"TESTS FAILED: {failed}");
            Console.WriteLine($"TESTS EXECUTED: {tested}");
            Console.Read();
        }
    }
}
