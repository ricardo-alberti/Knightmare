internal class AlphaBeta : ISearch
{
    public AlphaBeta() : this(new MaterialEvaluator()) { }
    public AlphaBeta(IEvaluator _evaluator) { }

    public Node BestTree(Board _position,
                         int depth,
                         int alpha,
                         int beta,
                         bool isMaximizing)
    {

        return new Node();
    }
}
