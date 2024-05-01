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

        Robot Robot0 = new Robot(1, chessBoard, new MoveTree(), "robot1");
        Robot Robot1 = new Robot(0, chessBoard, new MoveTree(), "robot2");

        Move move = new Move();
        MoveTree movetree = new MoveTree();

        int piece0_id = chessBoard.SidePieces(1).ElementAt(2).Value.Id();
        int piece1_id = chessBoard.SidePieces(0).ElementAt(2).Value.Id();

        chessBoard.SetPieces();
        chessBoard.Print();

        while (true)
        {
            movetree = Robot0.Calculate(chessBoard); 
            move = Robot0.MoveToPlay(movetree);
            chessBoard = chessBoard.Update(move);
            chessBoard.Print();

            movetree = Robot1.Calculate(chessBoard); 
            move = Robot1.MoveToPlay(movetree);
            chessBoard = chessBoard.Update(move);
            chessBoard.Print();
        }
    }
}
