internal class MoveView
{
    private readonly string[] SquareNames =
    {
            "a1","b1","c1","d1","e1","f1","g1","h1",
            "a2","b2","c2","d2","e2","f2","g2","h2",
            "a3","b3","c3","d3","e3","f3","g3","h3",
            "a4","b4","c4","d4","e4","f4","g4","h4",
            "a5","b5","c5","d5","e5","f5","g5","h5",
            "a6","b6","c6","d6","e6","f6","g6","h6",
            "a7","b7","c7","d7","e7","f7","g7","h7",
            "a8","b8","c8","d8","e8","f8","g8","h8"
    };

    private static readonly char[] PromotionPieces = { ' ', 'n', 'b', 'r', 'q' };

    public void Print(int move)
    {
        Console.WriteLine($"bestmove {ToString(move)}");
    }

    public string ToString(int move)
    {
        int from = MoveEncoder.FromSquare(move);
        int to = MoveEncoder.ToSquare(move);
        int promotion = MoveEncoder.Promotion(move);

        string moveStr = SquareNames[from] + SquareNames[to];

        return moveStr;
    }
}

