using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class ChessPiece
    {
        private readonly int id;
        private readonly char notation;
        private readonly int value;
        private readonly string shape;
        private readonly Point position;
        private readonly int[,] moveSet;
        private readonly PlayerSide side;

        protected ChessPiece(int _id, char _notation, string _shape, Point _position, int[,] _moveSet, PlayerSide _side, int _value)
        {
            id = _id;
            notation = _notation;
            shape = _shape;
            position = _position;
            moveSet = _moveSet;
            side = _side;
            value = _value;
        }

        public ChessPiece UpdatePosition(Point _position)
        {
            return Create(notation, _position, side, id);
        }

        public string Shape()
        {
            return shape;
        }

        public int Value()
        {
            return value;
        }

        public char Notation()
        {
            return notation;
        }

        public int Id()
        {
            return id;
        }

        public PlayerSide Side()
        {
            return side;
        }

        public ChessPiece Promote(char notation)
        {
            return Create(notation, position, side, id);
        }

        public int[,] MoveSet()
        {
            return moveSet;
        }

        public Point Position()
        {
            return position;
        }

        public ChessPiece Copy()
        {
            return Create(notation, position, side, id);
        }

        public virtual List<Move> MoveRange(Board _position)
        {
            Board position = _position.Copy();
            List<Move> moveRange = new List<Move>();

            Tile initialTile = position.Tile(Position().x, Position().y);
            ChessPiece piece = initialTile.Piece();
            int[,] moveSet = piece.MoveSet();

            for (int i = 0; i < moveSet.GetLength(0); ++i)
            {
                int moveset_x = moveSet[i, 0];
                int moveset_y = moveSet[i, 1];

                int finaltile_x = initialTile.Position().x + moveset_x;
                int finaltile_y = initialTile.Position().y + moveset_y;

                while (finaltile_x >= 0 && finaltile_y >= 0 && finaltile_x <= 7 && finaltile_y <= 7)
                {
                    Tile finalTile = position.Tile(finaltile_x, finaltile_y);
                    ChessPiece finalPiece = finalTile.Piece();

                    if (finalPiece != null)
                    {
                        if (finalPiece.Side() == piece.Side())
                            break;

                        if (finalPiece.Side() != piece.Side() && finalPiece.Side() != PlayerSide.None)
                        {
                            moveRange.Add(new Move(initialTile, finalTile)); // Capture move
                            break;
                        }
                    }

                    moveRange.Add(new Move(initialTile, finalTile)); // Valid move to empty tile

                    finaltile_x += moveset_x;
                    finaltile_y += moveset_y;
                }
            }

            return moveRange;
        }

        public static ChessPiece Create(char notation, Point position, PlayerSide side, int id)
        {
            char lowerNotation = char.ToLower(notation);

            switch (lowerNotation)
            {
                case 'n':
                    return Knight.Create(position, side, id);
                case 'r':
                    return Rook.Create(position, side, id);
                case 'p':
                    return Pawn.Create(position, side, id);
                case 'q':
                    return Queen.Create(position, side, id);
                case 'k':
                    return King.Create(position, side, id);
                case 'b':
                    return Bishop.Create(position, side, id);
                default:
                    return new Piece();
            }
        }
    }
}