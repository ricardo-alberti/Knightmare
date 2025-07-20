using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal class Move
    {
        public Tile Initial { get; }
        public Tile Final { get; }
        public Piece MovingPiece { get; }
        public Piece? CapturedPiece { get; }

        public Move(Tile _initialTile, Tile _finalTile)
        {
            Initial = _initialTile;
            Final = _finalTile;
            MovingPiece = _initialTile.TilePiece!;
            CapturedPiece = _finalTile.TilePiece;
        }

        public Tile[] Tiles() { return new Tile[2] { Initial, Final }; }

        public void Undo(Board board)
        {
            board.Terminal = false;
            board.Tiles[Initial.Position.y, Initial.Position.x].TilePiece = MovingPiece;
            board.Tiles[Final.Position.y, Final.Position.x].TilePiece = CapturedPiece;

            if (MovingPiece.Side == PlayerSide.White)
            {
                board.WhitePieces.Remove(Final.Position);
                board.WhitePieces[Initial.Position] = MovingPiece;
                if (CapturedPiece != null)
                {
                    board.BlackPieces[Final.Position] = CapturedPiece;
                }
                board.SideToMove = PlayerSide.White;
                return;
            }

            board.BlackPieces.Remove(Final.Position);
            board.BlackPieces[Initial.Position] = MovingPiece;
            if (CapturedPiece != null)
            {
                board.WhitePieces[Final.Position] = CapturedPiece;
            }
            board.SideToMove = PlayerSide.Black;
        }

        public void Execute(Board board)
        {
            if (CapturedPiece is King)
            {
                board.Terminal = true;
            }

            board.Tiles[Initial.Position.y, Initial.Position.x].TilePiece = null;
            board.Tiles[Final.Position.y, Final.Position.x].TilePiece = MovingPiece;

            if (MovingPiece.Side == PlayerSide.White)
            {
                board.SideToMove = PlayerSide.Black;
                board.WhitePieces.Remove(Initial.Position);
                board.WhitePieces[Final.Position] = MovingPiece;
                if (CapturedPiece != null) board.BlackPieces.Remove(Final.Position);
                return;
            }

            board.SideToMove = PlayerSide.White;
            board.BlackPieces.Remove(Initial.Position);
            board.BlackPieces[Final.Position] = MovingPiece;
            if (CapturedPiece != null) board.WhitePieces.Remove(Final.Position);
        }

        public static Move Create(string _inputUCI, Board _board)
        {
            string input = _inputUCI.ToLower();

            int firstX = input[0] - 'a';
            int lastX = input[2] - 'a';
            int firstY = 7 - (input[1] - '1');
            int lastY = 7 - (input[3] - '1');

            Move move = new Move(_board.Tile(firstX, firstY), _board.Tile(lastX, lastY));

            return move;
        }

        public bool IsCapture()
        {
            return CapturedPiece != null;
        }
    }
}
