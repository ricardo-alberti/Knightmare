using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal class Move
    {
        private Piece? MovingPiece { get; set; }
        private Piece? CapturedPiece { get; set; }
        private Tile InitialTile { get; set; }
        private Tile FinalTile { get; set; }
        private char Promotion { get; set; }

        public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

        public Move(Tile _initialTile, Tile _finalTile, char _promotion = '\0')
        {
            MovingPiece = _initialTile.TilePiece;
            CapturedPiece = _finalTile.TilePiece;
            InitialTile = _initialTile;
            FinalTile = _finalTile;
            Promotion = _promotion;
        }

        public Tile[] Tiles() { return new Tile[2] { InitialTile, FinalTile }; }

        public void Execute(Board board)
        {
            if (MovingPiece == null) return;

            board.Tiles[InitialTile.Position.y, InitialTile.Position.x].TilePiece = null;
            board.Tiles[FinalTile.Position.y, FinalTile.Position.x].TilePiece = MovingPiece;

            if (MovingPiece.Side == PlayerSide.White)
            {
                board.SidePlayable = PlayerSide.Black;
                board.WhitePieces.Remove(InitialTile.Position);
                board.WhitePieces[FinalTile.Position] = MovingPiece;
                if (CapturedPiece != null) board.BlackPieces.Remove(FinalTile.Position);
            }
            else
            {
                board.SidePlayable = PlayerSide.White;
                board.BlackPieces.Remove(InitialTile.Position);
                board.BlackPieces[FinalTile.Position] = MovingPiece;
                if (CapturedPiece != null) board.WhitePieces.Remove(FinalTile.Position);
            }

            board.History.Push(this);
        }

        public void Undo(Board board)
        {
            if (MovingPiece == null) return;

            if (Promotion != '\0')
            {
                MovingPiece = Piece.Create('p', MovingPiece.Position, MovingPiece.Side);
            }

            board.Tiles[InitialTile.Position.y, InitialTile.Position.x].TilePiece = MovingPiece;
            board.Tiles[FinalTile.Position.y, FinalTile.Position.x].TilePiece = CapturedPiece;

            if (MovingPiece.Side == PlayerSide.White)
            {
                board.SidePlayable = PlayerSide.White;
                board.WhitePieces[InitialTile.Position] = MovingPiece;
                board.WhitePieces.Remove(FinalTile.Position);
                if (CapturedPiece != null) board.BlackPieces[FinalTile.Position] = CapturedPiece;
            }
            else
            {
                board.SidePlayable = PlayerSide.Black;
                board.BlackPieces[InitialTile.Position] = MovingPiece;
                board.BlackPieces.Remove(FinalTile.Position);
                if (CapturedPiece != null) board.WhitePieces[FinalTile.Position] = CapturedPiece;
            }
        }

        public static Move Create(string _inputUCI, Board _board)
        {
            Move move = new();
            string input = _inputUCI.ToLower();

            int firstX = input[0] - 'a';
            int lastX = input[2] - 'a';
            int firstY = 7 - (input[1] - '1');
            int lastY = 7 - (input[3] - '1');

            move = new Move(_board.Tile(firstX, firstY), _board.Tile(lastX, lastY));

            return move;
        }
    }
}
