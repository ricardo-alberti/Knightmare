using System.Diagnostics;

namespace Knightmare.Tests
{
    internal sealed class PerformanceTest
    {
        private const int movesToPlay = 30;
        private const int botLevel = 8;
        private readonly BoardView view;
        private Board position;

        public PerformanceTest()
        {
            position = new BoardParser().CreateBoardFromFEN();
            view = new BoardView();
        }

        public void Execute()
        {
            var watch = Stopwatch.StartNew();

            AlphaBeta alphaBeta = new AlphaBeta();

            for (int j = 0; j < movesToPlay; ++j)
            {
                List<Node> tree = new();
                int rootIndex = alphaBeta.BestTree(position, botLevel, int.MinValue, int.MaxValue, position.WhiteToMove, tree);
                Node root = tree[rootIndex];
                position.MakeMove(root.Move);
            }

            watch.Stop();

            view.Print(position);
            Console.WriteLine($"Time elapsed per move: {watch.ElapsedMilliseconds / movesToPlay}");
            Console.WriteLine($"Time elapsed total: {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Moves played: {movesToPlay}");
            Console.WriteLine($"Bot level: {botLevel}");
            Console.Read();
        }
    }
}
