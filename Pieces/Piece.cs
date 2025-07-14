using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class Piece
    {
        public char Notation { get; }
        public int Value { get; }
        public char Shape { get; }
        public int[,] MoveSet { get; }
        public PlayerSide Side { get; }
        public Point Position { get; set; }

        protected Piece(char _notation, char _shape,
                Point _position, int[,] _moveSet,
                PlayerSide _side, int _value)
        {
            Notation = _notation;
            Shape = _shape;
            MoveSet = _moveSet;
            Side = _side;
            Value = _value;
            Position = _position;
        }

        protected bool InsideBounds(int _x, int _y)
        {
            return _x >= 0 && _y >= 0 && _x <= 7 && _y <= 7;
        }

        public virtual List<Move> MoveRange(Board position)
        {
            var moveRange = new List<Move>();
            Tile initialTile = position.Tile(Position.x, Position.y);

            for (int i = 0; i < MoveSet.GetLength(0); ++i)
            {
                int dx = MoveSet[i, 0];
                int dy = MoveSet[i, 1];
                int x = Position.x + dx;
                int y = Position.y + dy;

                while (InsideBounds(x, y))
                {
                    Tile finalTile = position.Tile(x, y);
                    Piece? targetPiece = finalTile.TilePiece;

                    if (targetPiece == null)
                    {
                        moveRange.Add(new Move(initialTile, finalTile));
                    }
                    else
                    {
                        if (targetPiece.Side != Side)
                        {
                            moveRange.Add(new Move(initialTile, finalTile));
                        }

                        break;
                    }

                    x += dx;
                    y += dy;
                }
            }

            return moveRange;
        }

        public static Piece Create(char notation, Point position, PlayerSide side)
        {
            switch (char.ToLower(notation))
            {
                case 'n': return Knight.Create(position, side);
                case 'r': return Rook.Create(position, side);
                case 'p': return Pawn.Create(position, side);
                case 'q': return Queen.Create(position, side);
                case 'k': return King.Create(position, side);
                case 'b': return Bishop.Create(position, side);
                default:
                    throw new ArgumentException($"Invalid piece notation: {notation}");
            }
        }

        public int PieceIndex()
        {
            switch (char.ToLower(Notation))
            {
                case 'p': return 0;
                case 'n': return 1;
                case 'b': return 2;
                case 'r': return 3;
                case 'q': return 4;
                case 'k': return 5;
                default: throw new Exception($"Unknown notation: {Notation}");
            }
        }
    }
}
