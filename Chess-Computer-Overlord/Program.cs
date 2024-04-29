namespace Chess
{
    class Program
    {
        static void Main()
        {
            Game chess = Game.Instance();
            chess.Start();
        }
    }

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

        public abstract class ChessPiece
        {
            protected readonly char notation;
            private readonly int id;
            private readonly string shape;
            private readonly Point position;
            private readonly int[,] moveSet;
            private readonly int side;

            protected ChessPiece(int _id, char _notation, string _shape, Point _position, int[,] _moveSet, int _side)
            {
                id = _id;
                notation = _notation;
                shape = _shape;
                position = _position;
                moveSet = _moveSet;
                side = _side;
            }

            public ChessPiece UpdatePosition(Point _position)
            {
                return CreatePiece(this.notation, _position, this.side, id);
            }

            public string Shape()
            {
                return shape;
            }

            public int Id()
            {
                return id;
            }

            public int Side()
            {
                return side;
            }

            public virtual Move[] MoveRange(Board _boardPosition)
            {
                Move[] moveRange = new Move[1];

                Tile initialTile = _boardPosition.Tile(Position().x, Position().y);
                ChessPiece piece = initialTile.Piece();
                int[,] moveSet = piece.MoveSet();

                int moveset_x;
                int moveset_y;
                int finaltile_x;
                int finaltile_y;

                for (int i = 0; i < moveSet.GetLength(0); ++i)
                {
                    moveset_x = moveSet[i, 0];
                    moveset_y = moveSet[i, 1];

                    finaltile_x = initialTile.Position().x + moveset_x;
                    finaltile_y = initialTile.Position().y + moveset_y;

                    if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) {
                        continue;
                    }

                    Tile finalTile = _boardPosition.Tile(finaltile_x, finaltile_y);

                    if (finalTile.Piece().Side() == initialTile.Piece().Side()) {
                        continue;
                    }

                    Move move = new Move(initialTile, finalTile);
                    moveRange[0] = move;
                    break;
                }

                return moveRange;
            }

            public ChessPiece Promote(char notation)
            {
                return CreatePiece(notation, position, side, id);
            }

            public int[,] MoveSet()
            {
                return moveSet;
            }

            public Point Position()
            {
                return position;
            }
        }

        public static ChessPiece CreatePiece(char notation, Point position, int side, int id)
        {
            switch (notation)
            {
                case 'N':
                    return new Knight(position, side, id);
                case 'T':
                    return new Tower(position, side, id);
                case 'P':
                    return new Pawn(position, side, id);
                case 'Q':
                    return new Queen(position, side, id);
                case 'K':
                    return new King(position, side, id);
                case 'B':
                    return new Bishop(position, side, id);
                default:
                    return new Piece();
            }
        }

        private class Knight : ChessPiece
        {
            public Knight(Point position, int side, int id)
                    : base(id, 'N', side == 1 ? " \u2658" : " \u265E", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class Queen : ChessPiece
        {
            public Queen(Point position, int side, int id)
                : base(id, 'Q', side == 1 ? " \u2655" : " \u265B", position, new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, 1 } }, side)
            {

            }
        }

        private class King : ChessPiece
        {
            public King(Point position, int side, int id)
                : base(id, 'K', side == 1 ? " \u2654" : " \u265A", position, new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, -1 } }, side)
            {

            }
        }

        private class Tower : ChessPiece
        {
            public Tower(Point position, int side, int id)
                : base(id, 'T', side == 1 ? " \u2656" : " \u265C", position, new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } }, side)
            {

            }
        }

        private class Pawn : ChessPiece
        {
            public Pawn(Point position, int side, int id)
                : base(id, 'P', side == 1 ? " \u2659" : " \u265F", position, side == 0 ? new int[,] { { -1, -1 }, { -1, 1 }, { -1, 0 } } : new int[,] { { 1, 1 }, { 1, -1 }, { 1, 0 } }, side)
            {

            }

            public override Move[] MoveRange(Board _boardPosition)
            {
                Move[] moveRange = new Move[30];

                Tile initialTile = _boardPosition.Tile(Position().x, Position().y);
                ChessPiece piece = initialTile.Piece();
                int[,] moveSet = piece.MoveSet();

                for (int i = 0; i < moveSet.GetLength(0); ++i)
                {
                    int moveset_x = moveSet[i, 0];
                    int moveset_y = moveSet[i, 1];

                    int finaltile_x = initialTile.Position().x + moveset_x;
                    int finaltile_y = initialTile.Position().y + moveset_y;

                    if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

                    Tile finalTile = _boardPosition.Tile(finaltile_x, finaltile_y);
                    if (finalTile.Piece().Side() == initialTile.Piece().Side()) continue;

                    Move move = new Move(initialTile, finalTile);
                    moveRange[0] = move;
                    break;
                }

                return moveRange;
            }
        }

        private class Bishop : ChessPiece
        {
            public Bishop(Point position, int side, int id)
                : base(id, 'B', side == 1 ? " \u2657" : " \u265D", position, new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } }, side)
            {

            }
        }

        private class Piece : ChessPiece
        {
            public Piece()
                : base(0, 'F', " \u2022", new Point(), new int[,] { { 0, 0 } }, 2)
            {

            }
        }

        public class Point
        {
            public int x { get; }
            public int y { get; }

            public Point() : this(0, 0) { }

            public Point(int X, int Y)
            {
                x = X;
                y = Y;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + x.GetHashCode();
                    hash = hash * 23 + y.GetHashCode();
                    return hash;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                Point other = (Point)obj;
                return x == other.x && y == other.y;
            }

            public void Print()
            {
                Console.WriteLine($"Point: ({x}, {y})");
                Console.Read();
            }
        }

        public class Tile
        {
            private readonly Point position;
            private readonly ChessPiece piece;

            public Tile(Point _point) : this(new Piece(), _point)
            {

            }

            public Tile(ChessPiece _piece, Point _point)
            {
                position = _point;
                piece = _piece;
            }

            public ChessPiece Piece()
            {
                return piece;
            }

            public Tile SetPiece(ChessPiece _piece)
            {
                return new Tile(_piece, this.position);
            }

            public Point Position()
            {
                return position;
            }

            public string Print()
            {
                return piece.Shape();
            }
        }

        public class Board
        {
            private readonly Dictionary<Point, ChessPiece> whitePieces;
            private readonly Dictionary<Point, ChessPiece> blackPieces;
            private readonly Tile[,] tiles;

            public Board() : this(defaultPieces(1), defaultPieces(0), new Tile[8, 8]) { }

            public Board(Dictionary<Point, ChessPiece> _whitePieces, Dictionary<Point, ChessPiece> _blackPieces, Tile[,] _tiles)
            {
                whitePieces = _whitePieces;
                blackPieces = _blackPieces;
                tiles = _tiles;
            }

            public Board Update(Move _move)
            {
                Tile[] newTiles = _move.Tiles();
                Tile[,] updatedTiles = tiles;

                ChessPiece pieceUpdated = _move.Tiles()[1].Piece();

                for (int i = 0; i < newTiles.Length; ++i)
                {
                    int x = newTiles[i].Position().x;
                    int y = newTiles[i].Position().y;

                    updatedTiles[y, x] = newTiles[i];
                }

                Dictionary<Point, ChessPiece> whitePiecesUpdated = whitePieces;
                Dictionary<Point, ChessPiece> blackPiecesUpdated = blackPieces;

                if (pieceUpdated.Side() == 1)
                {
                    whitePiecesUpdated.Remove(newTiles[0].Position());
                    whitePiecesUpdated[newTiles[1].Position()] = pieceUpdated;
                }
                else
                {
                    blackPiecesUpdated.Remove(newTiles[0].Position());
                    blackPiecesUpdated[newTiles[1].Position()] = pieceUpdated;
                }

                Board updated = new Board(whitePiecesUpdated, blackPiecesUpdated, updatedTiles);

                return updated;
            }

            public Tile Tile(int x, int y)
            {
                return tiles[y, x];
            }

            public void Print()
            {
                Console.Clear();

                Console.WriteLine("     A B C D E F G H");
                Console.WriteLine("    -----------------");

                for (int i = 0; i < 8; i++)
                {
                    Console.Write($" {i + 1} |");

                    for (int j = 0; j < 8; ++j)
                    {
                        Console.Write(tiles[i, j].Print());
                    }

                    Console.WriteLine(" |");
                }

                Console.WriteLine("    -----------------");
                Console.ReadLine();
            }

            public Dictionary<Point, ChessPiece> SidePieces(int _side)
            {
                if (_side == 1)
                {
                    return whitePieces;
                }

                return blackPieces;
            }

            public void SetPieces()
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Tile tile = new Tile(new Point(j, i));
                        tiles[i, j] = tile;

                        for (int k = 0; k < 16; k++)
                        {
                            if (whitePieces.ElementAt(k).Value.Position().y == i
                                    && whitePieces.ElementAt(k).Value.Position().x == j)
                            {
                                tiles[i, j] = new Tile(whitePieces.ElementAt(k).Value, new Point(j, i));
                                break;
                            }

                            if (blackPieces.ElementAt(k).Value.Position().y == i
                                    && blackPieces.ElementAt(k).Value.Position().x == j)
                            {
                                tiles[i, j] = new Tile(blackPieces.ElementAt(k).Value, new Point(j, i));
                                break;
                            }
                        }
                    }
                }
            }

            private static Dictionary<Point, ChessPiece> defaultPieces(int _side)
            {
                Dictionary<Point, ChessPiece> pieces = new Dictionary<Point, ChessPiece> { };
                int id = 1;

                char[] notations = {
                    'T','T','N','N','B','B','K','Q',
                    'P','P','P','P','P','P','P','P',
                };

                Point[] position = {
                    new Point(0, 7), new Point(7, 7), new Point(1, 7), new Point(6, 7), new Point(2, 7), new Point(5, 7), new Point(4, 7), new Point(3, 7),
                    new Point(0, 6), new Point(1, 6), new Point(2, 6), new Point(3, 6), new Point(4, 6), new Point(5, 6), new Point(6, 6), new Point(7, 6),
                    new Point(0, 0), new Point(7, 0), new Point(1, 0), new Point(6, 0), new Point(2, 0), new Point(5, 0), new Point(4, 0), new Point(3, 0),
                    new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(5, 1), new Point(6, 1), new Point(7, 1)
                };

                for (int i = 0; i < 16; i++)
                {
                    ChessPiece piece = CreatePiece(notations[i], position[(_side * 16) + i], _side, (id++) + (16 * _side) );
                    pieces.Add(piece.Position(), piece);
                }

                return pieces;
            }
        }

        public abstract class Player
        {
            private readonly int side;

            public Player(int _side, Board _chessBoard)
            {
                side = _side;
            }

            public Move AskMove(Board chessBoard)
            {
                Console.WriteLine("Tile1: ");
                string tile1 = Console.ReadLine();
                int x1 = tile1[0] - '0', y1 = tile1[0] - '0';

                if (tile1 == "") AskMove(chessBoard);

                Console.WriteLine("Tile2: ");
                string tile2 = Console.ReadLine();
                int x2 = tile2[0] - '0', y2 = tile2[1] - '0';

                if (tile2 == "") AskMove(chessBoard);

                Move move = new Move(chessBoard.Tile(x1, y1), chessBoard.Tile(x2, y2));

                return move;
            }

            protected Boolean ValidMove(Move _move)
            {
                return true;
            }

            public Point findPointByPiece(Dictionary<Point, ChessPiece> pieces, ChessPiece piece)
            {
                foreach (var pair in pieces)
                {
                    if (pair.Value == piece)
                    {
                        return pair.Key;
                    }
                }

                throw new KeyNotFoundException("The specified value was not found in the dictionary.");
            }

            public Board Play(Move _move, Board _position)
            {
                Board newPosition = _position.Update(_move);

                return newPosition;
            }
        }

        public class Robot : Player
        {
            private readonly int side;
            private readonly Board board;
            private readonly MoveTree possibleMoves;

            public Robot Update(MoveTree _possibleMoves)
            {
                return new Robot(side, board, _possibleMoves);
            }

            public Robot(int _side, Board _chessBoard, MoveTree _movetree) : base(_side, _chessBoard)
            {
                side = _side;
                possibleMoves = _movetree;
                board = _chessBoard;
            }

            private MoveTree Moves()
            {
                return possibleMoves;
            }

            public ChessPiece Piece(Dictionary<Point, ChessPiece> pieces, int id)
            {

                ChessPiece piece = new Piece();

                foreach(var pair in pieces)
                {
                    if (pair.Value.Id() == id)
                    {
                        return pair.Value;
                    }
                }
                 throw new Exception("NOT PIECE WITH THIS ID");
            }

            public Move MoveToPlay(Board chessBoard, int id)
            {
                Dictionary<Point, ChessPiece> pieces = chessBoard.SidePieces(side);
                ChessPiece piece = Piece(pieces, id);

                Move[] moverange = piece.MoveRange(chessBoard);

                return moverange[0];
            }
        }

        public class Mortal : Player
        {
            public Mortal(int _side, Board _board) : base(_side, _board)
            {

            }
        }

        public sealed class MoveTree
        {
            private readonly Move head;
            private readonly Move left;
            private readonly Move right;

            public MoveTree()
            {
                head = new Move();
                left = new Move();
                right = new Move();
            }

            public void findMoves(Board position, int side)
            {

            }

            public void AddMove()
            {

            }

            public void RemoveMove()
            {

            }
        }

        public sealed class Move
        {
            private readonly ChessPiece piece;
            private readonly Tile initialTile;
            private readonly Tile finalTile;

            public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

            public Move(Tile _initialTile, Tile _finalTile)
            {
                piece = _initialTile.Piece();
                initialTile = _initialTile.SetPiece(new Piece()); ;
                finalTile = _finalTile.SetPiece(piece.UpdatePosition(_finalTile.Position()));
            }

            public Tile[] Tiles()
            {
                return new Tile[] { initialTile, finalTile };
            }

            public void Print()
            {
                Console.WriteLine($"{piece.Shape()} from ({initialTile.Position().x}, {initialTile.Position().y}) to ({finalTile.Position().x}, {finalTile.Position().y})");
                Console.Read();
            }
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
}
