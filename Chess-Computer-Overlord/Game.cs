public sealed class Game
{
    private Game() { }
    private static Game _instance;
    public static Game Instance()
    {
        if (_instance == null)
        {
            _instance = new Game();
        }
        return _instance;
    }

    public void Start()
    {
        Board chessBoard = new Board();

        Robot Robot0 = new Robot(1, chessBoard, new MoveTree());
        Robot Robot1 = new Robot(0, chessBoard, new MoveTree());

        ChessPiece lastPiece = new Piece();
        Move move = new Move();
        int piece0_id = chessBoard.SidePieces(1).ElementAt(2).Value.Id();
        int piece1_id = chessBoard.SidePieces(0).ElementAt(2).Value.Id();

        chessBoard.SetPieces();
        chessBoard.Print();

        while (true)
        {
            move = Robot0.MoveToPlay(chessBoard, piece0_id);
            chessBoard = chessBoard.Update(move);
            chessBoard.Print();

            move = Robot1.MoveToPlay(chessBoard, piece1_id);
            chessBoard = chessBoard.Update(move);
            chessBoard.Print();
        }
    }
}
