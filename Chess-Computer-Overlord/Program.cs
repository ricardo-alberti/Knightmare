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
            protected readonly char code;
            private readonly string shape;
            private readonly Point position;
            private readonly int[,] moveSet;
            private readonly int side;

            protected ChessPiece(char _code, string _shape, Point _position, int[,] _moveSet, int _side)
            {
                code = _code;
                shape = _shape;
                position = _position;
                moveSet = _moveSet;
                side = _side;
            }

            public string Shape()
            {
                return shape;
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

        public static ChessPiece CreatePiece(char code, Point position, int side)
        {
            switch (code)
            {
                case 'N':
                    return new Knight(position, side);
                case 'T':
                    return new Tower(position, side);
                case 'P':
                    return new Pawn(position, side);
                case 'Q':
                    return new Queen(position, side);
                case 'K':
                    return new King(position, side);
                case 'B':
                    return new Bishop(position, side);
                default:
                    return new Piece();
            }
        }

        private class Knight : ChessPiece
        {
            public Knight(Point position, int side)
                    : base('N', side == 1 ? " \u2658" : " \u265E", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class Queen : ChessPiece
        {
            public Queen(Point position, int side)
                : base('Q', side == 1 ? " \u2655" : " \u265B", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class King : ChessPiece
        {
            public King(Point position, int side)
                : base('K', side == 1 ? " \u2654" : " \u265A", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class Tower : ChessPiece
        {
            public Tower(Point position, int side)
                : base('T', side == 1 ? " \u2656" : " \u265C", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class Pawn : ChessPiece
        {
            public Pawn(Point position, int side)
                : base('P', side == 1 ? " \u2659" : " \u265F", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class Bishop : ChessPiece
        {
            public Bishop(Point position, int side)
                : base('B', side == 1 ? " \u2657" : " \u265D", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
            {

            }
        }

        private class Piece : ChessPiece
        {
            public Piece()
                : base('F', " \u2022", new Point(), new int[,] { { 0, 0 } }, 1)
            {

            }
        }

        public class Point
        {
            private readonly int x;
            private readonly int y;

            public Point() : this(0, 0)
            {

            }

            public Point(int X, int Y)
            {
                x = X;
                y = Y;
            }

            public int X()
            {
                return x;
            }

            public int Y()
            {
                return y;
            }
        }

        public class Tile
        {
            private readonly Point position;
            private readonly ChessPiece piece;

            public Tile() : this(new Piece()) {}

            public Tile(ChessPiece _piece)
            {
                this.piece = _piece;
            }

            public ChessPiece Piece()
            {
                return piece;
            }

            public string Print()
            {
                return piece.Shape();
            }
        }

        public class Board
        {
            private readonly ChessPiece[] whitePieces;
            private readonly ChessPiece[] blackPieces;
            private readonly ChessPiece[] pieces;
            private readonly Tile[,] tiles;

            public Board() : this(defaultPieces(1), defaultPieces(0), new Tile[8, 8], Pieces(defaultPieces(1), defaultPieces(0)))
            {

            }

            public Board(ChessPiece[] _whitePieces, ChessPiece[] _blackPieces, Tile[,] _tiles, ChessPiece[] _pieces) 
            {
                whitePieces = _whitePieces;
                blackPieces = _blackPieces;
                pieces = _pieces;
                tiles = _tiles;
            }

            public Board UpdateBoard(Tile[] tiles)
            {
                Tile[,] updatedTiles = {};

                return new Board(this.whitePieces, this.blackPieces, updatedTiles, this.pieces);
            }

            public Tile Tile(Point _position)
            {
                Tile tile = tiles[_position.X(), _position.Y()];
                return tile;
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
                Console.Read();
            }

            public ChessPiece[] SidePieces(int _side)
            {
                if (_side == 1)
                {
                    return whitePieces;
                }

                return blackPieces;
            }

            public ChessPiece[] Pieces()
            {
                ChessPiece[] pieces = new ChessPiece[whitePieces.Length + blackPieces.Length];
                Array.Copy(whitePieces, 0, pieces, 0, whitePieces.Length);
                Array.Copy(blackPieces, 0, pieces, whitePieces.Length, blackPieces.Length);
                return pieces;
            }

            public static ChessPiece[] Pieces(ChessPiece[] whitePieces, ChessPiece[] blackPieces)
            {
                ChessPiece[] pieces = new ChessPiece[whitePieces.Length + blackPieces.Length];
                Array.Copy(whitePieces, 0, pieces, 0, whitePieces.Length);
                Array.Copy(blackPieces, 0, pieces, whitePieces.Length, blackPieces.Length);
                return pieces;
            }

            public void SetPieces(ChessPiece[] pieces)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Tile tile = new Tile();
                        tiles[i, j] = tile;

                        for (int k = 0; k < pieces.Length; k++)
                        {
                            if (pieces[k].Position().X() == j && pieces[k].Position().Y() == i)
                            {
                                tiles[i, j] = new Tile(pieces[k]);
                                break;
                            }
                        }
                    }
                }
            }

            private static ChessPiece[] defaultPieces(int _side)
            {
                ChessPiece[] pieces = new ChessPiece[16];

                char[] codes = {
                    'T','T','N','N','B','B','K','Q',
                    'P','P','P','P','P','P','P','P',
                };

                Point[] position = {
                    new Point(0, 0), new Point(7, 0), new Point(1, 0), new Point(6, 0), new Point(2, 0), new Point(5, 0), new Point(4, 0), new Point(3, 0),
                    new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(5, 1), new Point(6, 1), new Point(7, 1),
                    new Point(0, 7), new Point(7, 7), new Point(1, 7), new Point(6, 7), new Point(2, 7), new Point(5, 7), new Point(4, 7), new Point(3, 7),
                    new Point(0, 6), new Point(1, 6), new Point(2, 6), new Point(3, 6), new Point(4, 6), new Point(5, 6), new Point(6, 6), new Point(7, 6)
                };

                for (int i = 0; i < 16; i++)
                {
                    ChessPiece piece = CreatePiece(codes[i], position[(_side * 16) + i], _side);
                    pieces[i] = piece;
                }

                return pieces;
            }
        }

        public abstract class Player
        {
            private readonly int side;
            private readonly ChessPiece[] pieces;

            public Player(int _side, Board _chessBoard)
            {
                side = _side;
                pieces = _chessBoard.SidePieces(_side);;
            }

            protected Boolean moveValid(Tile fromTile, Tile toTile)
            {
                return true;
            }

            protected Tile[] moveToPlay()
            {
                Tile[] tiles = { new Tile() };
                return tiles;
            }

            protected void movePiece(Tile fromTile, Tile toTile)
            {
                fromTile = new Tile();
                toTile = new Tile(fromTile.Piece());
            }
        }

        public class Robot : Player
        {
            private readonly MoveTree possibleMoves;
            private readonly Board chessBoard;
            private readonly Board updatedBoard;

            public Robot(int _side, Board _chessBoard) : base(_side, _chessBoard)
            {
                possibleMoves = new MoveTree();
                updatedBoard = _chessBoard;
            }

            private MoveTree moveTree()
            {
                return possibleMoves;
            }

            private Tile[] bestMove()
            {
                Tile[] tiles = { new Tile() };
                return tiles;
            }

            public void play()
            {
                Tile tile1 = chessBoard.Tile(new Point(0, 0));
                Tile tile2 = chessBoard.Tile(new Point(7, 6));
                tile1 = new Tile(tile2.Piece());
            }

            public Board UpdatedBoard()
            {
                return updatedBoard;
            }

            private sealed class MoveTree
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
            }

            private sealed class Move
            {
                private readonly ChessPiece piece;
                private readonly Point initialPosition;
                private readonly Point finalPosition;

                public Move() : this(new Piece(), new Point())
                {

                }

                public Move(ChessPiece _piece, Point _finalPosition)
                {
                    piece = _piece;
                    initialPosition = new Point(piece.Position().X(), piece.Position().Y());
                    finalPosition = _finalPosition;
                }
            }
        }

        public class Mortal : Player
        {
            public Mortal(int _side, Board _chessBoard) : base(_side, _chessBoard)
            {

            }
        }

        public void Start()
        {
            Board chessBoard = new Board();
            chessBoard.SetPieces(chessBoard.Pieces());
            //Mortal human = new Mortal(0, chessBoard.SidePieces(0));

            chessBoard.Print();

            Robot chessRobot0 = new Robot(1, chessBoard);
            chessRobot0.play();

            chessBoard.Print();

            Robot chessRobot1 = new Robot(0, chessBoard);
            chessRobot1.play();
        }
    }
}
