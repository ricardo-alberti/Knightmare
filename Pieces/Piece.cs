using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class Piece
    {
        public char Notation { get; }
        public int Value { get; }
        public string Shape { get; }
        public int[,] MoveSet { get; }
        public PlayerSide Side { get; }
        public Point Position { get; set; }

        protected Piece(char _notation, string _shape,
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

        public virtual List<Move> MoveRange(Board _position)
        {
            List<Move> moveRange = new List<Move>();
            Tile initialTile = _position.Tile(Position.x, Position.y);

            for (int i = 0; i < MoveSet.GetLength(0); ++i)
            {
                int moveset_x = MoveSet[i, 0];
                int moveset_y = MoveSet[i, 1];

                int finaltile_x = initialTile.Position.x + moveset_x;
                int finaltile_y = initialTile.Position.y + moveset_y;

                while (finaltile_x >= 0 && finaltile_y >= 0
                        && finaltile_x <= 7 && finaltile_y <= 7)
                {
                    Tile finalTile = _position.Tile(finaltile_x, finaltile_y);
                    Piece? finalPiece = finalTile.TilePiece;

                    if (finalPiece != null)
                    {
                        if (finalPiece.Side == Side)
                            break;

                        if (finalPiece.Side != Side)
                        {
                            moveRange.Add(new Move(initialTile, finalTile));
                            break;
                        }
                    }

                    moveRange.Add(new Move(initialTile, finalTile));

                    finaltile_x += moveset_x;
                    finaltile_y += moveset_y;
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

        public virtual Piece Copy()
        {
            return Create(this.Notation, this.Position, this.Side);
        }

    }
}
