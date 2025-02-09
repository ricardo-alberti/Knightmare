using Knightmare.Boards;
using Knightmare.DTO;

namespace Knightmare.Tests
{
    internal sealed class MoveCountTest
    {
        private readonly Dictionary<int, int> moveCount;

        public MoveCountTest()
        {
            moveCount = new Dictionary<int, int>()
            {
                {1, 20},
                {2, 400},
                //{3, 8902},
                //{4, 197281},
                //{5, 4865609}
            };
        }

        public void Execute()
        {
            int tested = 0;
            int passed = 0;
            int failed = 0;

            foreach (var pair in moveCount)
            {
                int depth = pair.Key;
                int moves = pair.Value;

                Robot bot = new(depth);
                Board board = Board.Create();
                MoveStats calculation = bot.Calculate(board);
                int result = calculation.CalculatedMoves();

                tested++;

                if (result == moves)
                {
                    passed++;
                    continue;
                }

                Console.WriteLine($"TEST FAIL: {depth}");
                Console.WriteLine($"Received: {result}");
                Console.WriteLine($"Expected: {moves}");
                failed++;
            }

            Console.WriteLine($"TESTS PASSED: {passed}");
            Console.WriteLine($"TESTS FAILED: {failed}");
            Console.WriteLine($"TESTS EXECUTED: {tested}");
            Console.Read();
        }
    }
}
