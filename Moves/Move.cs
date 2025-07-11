using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal class Move
    {
        private Tile InitialTile { get; set; }
        private Tile FinalTile { get; set; }
        private Piece? CapturedPiece { get; set; } // needed for undo

        public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

        public Move(Tile _initialTile, Tile _finalTile)
        {
            InitialTile = _initialTile;
            FinalTile = _finalTile;
        }

        public void Execute(Board board)
        {
            Piece piece = InitialTile.TilePiece!;
            CapturedPiece = FinalTile.TilePiece;

            if (piece == null) return;

            board.Tiles[InitialTile.Position.y, InitialTile.Position.x].TilePiece = null;
            board.Tiles[FinalTile.Position.y, FinalTile.Position.x].TilePiece = piece;

            if (piece.Side == PlayerSide.White)
            {
                board.SidePlayable = PlayerSide.Black;
                board.WhitePieces.Remove(InitialTile.Position);
                board.WhitePieces[FinalTile.Position] = piece;
                if (CapturedPiece != null) board.BlackPieces.Remove(FinalTile.Position);
            }
            else
            {
                board.SidePlayable = PlayerSide.White;
                board.BlackPieces.Remove(InitialTile.Position);
                board.BlackPieces[FinalTile.Position] = piece;
                if (CapturedPiece != null) board.WhitePieces.Remove(FinalTile.Position);
            }

            if (CapturedPiece != null && char.ToLower(CapturedPiece.Notation) == 'k')
            {
                board.GameOver = true;
            }
        }

        public void Undo(Board board)
        {
            Piece piece = FinalTile.TilePiece!;
            if (piece == null) return;

            // Restore pieces on tiles
            board.Tiles[InitialTile.Position.y, InitialTile.Position.x].TilePiece = piece;
            board.Tiles[FinalTile.Position.y, FinalTile.Position.x].TilePiece = CapturedPiece;

            // Restore piece dictionaries
            if (piece.Side == PlayerSide.White)
            {
                board.SidePlayable = PlayerSide.White;
                board.WhitePieces.Remove(FinalTile.Position);
                board.WhitePieces[InitialTile.Position] = piece;

                if (CapturedPiece != null)
                    board.BlackPieces[FinalTile.Position] = CapturedPiece;
            }
            else
            {
                board.SidePlayable = PlayerSide.Black;
                board.BlackPieces.Remove(FinalTile.Position);
                board.BlackPieces[InitialTile.Position] = piece;

                if (CapturedPiece != null)
                    board.WhitePieces[FinalTile.Position] = CapturedPiece;
            }

            // Restore game state
            if (CapturedPiece != null && char.ToLower(CapturedPiece.Notation) == 'k')
            {
                board.GameOver = false;
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

        public Tile[] Tiles() => new Tile[] { InitialTile, FinalTile };
    }
}
