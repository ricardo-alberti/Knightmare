internal class AlphaBeta 
{
    private readonly IEvaluator evaluator;
    private readonly MoveGenerator moveGenerator;

    public AlphaBeta()
    {
        evaluator = new MaterialEvaluator();
        moveGenerator = new MoveGenerator();
    }

    public int BestTree(Board position, int depth, int alpha, int beta, bool isMaximizing, List<Node> tree)
    {
        if (depth == 0)
        {
            var eval = evaluator.Execute(position);
            Node leaf = new Node
            {
                Move = 0,
                Eval = eval,
                ChildStart = -1,
                ChildCount = 0
            };
            tree.Add(leaf);
            return tree.Count - 1;
        }

        var moves = moveGenerator.GenerateMoves(position);
        int startIndex = tree.Count;
        int bestEval = isMaximizing ? int.MinValue : int.MaxValue;
        int bestMove = 0;
        int bestIndex = -1;
        int localChildCount = 0;

        foreach (int move in moves)
        {
            MoveState moveState = position.MakeMove(move);

            int childIndex = BestTree(position, depth - 1, alpha, beta, !isMaximizing, tree);
            Node child = tree[childIndex];
            localChildCount++;

            position.UndoMove(moveState, move);

            if ((isMaximizing && child.Eval > bestEval) || (!isMaximizing && child.Eval < bestEval))
            {
                bestEval = child.Eval;
                bestIndex = childIndex;
                bestMove = move;
            }

            if (isMaximizing)
            {
                alpha = Math.Max(alpha, bestEval);
                if (beta <= alpha) break;
            }
            else
            {
                beta = Math.Min(beta, bestEval);
                if (beta <= alpha) break;
            }
        }

        var node = new Node
        {
            Move = bestMove,
            Eval = bestEval,
            ChildStart = startIndex,
            ChildCount = localChildCount
        };
        tree.Add(node);
        return tree.Count - 1;
    }
}
