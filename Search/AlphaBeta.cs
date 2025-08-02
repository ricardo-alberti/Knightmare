internal class AlphaBeta 
{
    private readonly IEvaluator evaluator;

    public AlphaBeta()
    {
        evaluator = new MaterialEvaluator();
    }

    public int BestTree(Board position, int depth, int alpha, int beta, bool isMaximizing, List<Node> tree)
    {
        int alphaOrig = alpha;

        ulong key = position.ZobristKey; 
        if (TranspositionTable.TryGet(key, out var entry) && entry.Depth >= depth)
        {
            switch (entry.Bound)
            {
                case BoundType.Exact:
                    return AddLeafNode(tree, entry.Eval);
                case BoundType.LowerBound:
                    if (entry.Eval >= beta)
                        return AddLeafNode(tree, entry.Eval);
                    alpha = Math.Max(alpha, entry.Eval);
                    break;
                case BoundType.UpperBound:
                    if (entry.Eval <= alpha)
                        return AddLeafNode(tree, entry.Eval);
                    beta = Math.Min(beta, entry.Eval);
                    break;
            }
        }

        if (depth == 0)
        {
            var eval = evaluator.Execute(position);
            return AddLeafNode(tree, eval);
        }


        var moves = MoveGenerator.GenerateMoves(position);
        int startIndex = tree.Count;
        int bestEval = isMaximizing ? int.MinValue : int.MaxValue;
        int bestMove = -1;
        int bestIndex = -1;
        int localChildCount = 0;

        foreach (int move in moves)
        {
            MoveState moveState = position.MakeMove(move);

            if (position.IsInCheck(!position.WhiteToMove))
            {
                position.UndoMove(moveState, move);
                continue;
            }

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

        BoundType bound;
        if (bestEval <= alphaOrig) bound = BoundType.UpperBound;
        else if (bestEval >= beta) bound = BoundType.LowerBound;
        else bound = BoundType.Exact;

        TranspositionTable.Store(key, new TranspositionEntry(bestEval, depth, bound, bestMove));

        var node = new Node
        {
            Move = bestMove,
            Eval = bestEval,
            ChildStart = startIndex,
            ChildCount = localChildCount
        };
        tree.Add(node);
        return tree.Count - 1;

        int AddLeafNode(List<Node> tree, int eval)
        {
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
    }
}
