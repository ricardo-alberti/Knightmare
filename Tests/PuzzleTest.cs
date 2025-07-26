namespace Knightmare.Tests
{
    internal sealed class PuzzleTest
    {
        private readonly int depthLevel;
        private readonly Dictionary<string, string> puzzles;
        private readonly BoardView view;
        private readonly BoardParser boardParser;
        private readonly AlphaBeta alphaBeta;

        public PuzzleTest()
        {
            view = new BoardView();
            alphaBeta = new AlphaBeta();
            depthLevel = 5;
            boardParser = new BoardParser();
            puzzles = new Dictionary<string, string>()
            {
                {
                    "rnbqkbnr/pppppppp/8/8/2B1P3/5Q2/PPPP1PPP/RNB1K1NR w KQkq - 0 1",
                    "rnbqkbnr/pppppppp/8/8/2B1P3/5Q2/PPPP1PPP/RNB1K1NR w KQkq - 0 1"
                },
                {
                    "8/8/8/8/8/8/pk6/6KP b - - 0 1",
                    "8/8/8/8/8/8/pk6/6KP b - - 0 1"
                },
                {
                    "1k5Q/ppp4K/8/8/8/8/8/8 w - - 0 1",
                    "1k5Q/ppp4K/8/8/8/8/8/8 w - - 0 1"
                },
                {
                    "8/6PK/8/8/8/pk6/8/8 w - - 0 1",
                    "8/6PK/8/8/8/pk6/8/8 w - - 0 1"
                },
                {
                    "rnbqkb1r/1ppppppp/p6B/8/2BPP3/5Q2/PPP2PPP/RN2K1NR b - - 0 0",
                    "rnbqkb1r/1ppp1ppp/p3p2B/8/2BPP3/5Q2/PPP2PPP/RN2K1NR w - - 0 0"
                },
                {
                    "6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2rK2R w - - 0 0",
                    "6k1/b4ppp/5nb1/2p1pq2/1pN5/pP3P1N/P1P3PP/B2K3R b - - 0 0"
                },
                {
                    "rnb1kbnr/pppp1ppp/4p3/8/4P2P/2N5/PPPP1qP1/R1BQKBNR w - - 0 0",
                    "rnb1kbnr/pppp1ppp/4p3/8/4P2P/2N5/PPPP1KP1/R1BQ1BNR b - - 0 0"
                }
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

                Console.Clear();

                Board board = boardParser.CreateBoardFromFEN(puzzle);

                view.Print(board);

                List<Node> tree = new();
                int rootIndex = alphaBeta.BestTree(board, depthLevel, int.MinValue, int.MaxValue, board.WhiteToMove, tree);
                Node root = tree[rootIndex];
                board.MakeMove(root.Move);

                string result = boardParser.CreateFENFromBoard(board);

                tested++;

                if (result == solution)
                {
                    passed++;
                    continue;
                }

                Console.WriteLine($"TEST FAIL: {depthLevel}");
                Console.WriteLine($"Received: {result}");
                Console.WriteLine($"Expected: {solution}");
                failed++;

                view.Print(board);
                Console.Read();
            }

            Console.WriteLine($"TESTS PASSED: {passed}");
            Console.WriteLine($"TESTS FAILED: {failed}");
            Console.WriteLine($"TESTS EXECUTED: {tested}");
            Console.WriteLine($"Puzzle Tests Completed");
            Console.Read();
        }
    }
}
