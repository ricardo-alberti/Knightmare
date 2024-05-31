public class Board
{
    private readonly Dictionary<Point, ChessPiece> whitePieces;
    private readonly Dictionary<Point, ChessPiece> blackPieces;
    private readonly Tile[,] tiles;
    private readonly Dictionary<bool, string> gameOver;

    public Board() : this(defaultPieces(1), defaultPieces(0), new Tile[8, 8], new Dictionary<bool, string> {[false] = ""}) { }

    public Board(Dictionary<Point, ChessPiece> _whitePieces, Dictionary<Point, ChessPiece> _blackPieces, Tile[,] _tiles, Dictionary<bool, string> _gameOver)
    {
        whitePieces = _whitePieces;
        blackPieces = _blackPieces;
        tiles = _tiles;
        gameOver = _gameOver;
    }

    public bool GameOver()
    {
        return gameOver.First().Key;
    }

    public Board UpdateGameOverProperty(string result)
    {
        return new Board(whitePieces, blackPieces, tiles, new Dictionary<bool, string> {[true] = result});
    }

    public Dictionary<bool, string> GameOverResult()
    {
        return gameOver;
    }

    public int Evaluate()
    {
        int whiteQuality = 0;
        int blackQuality = 0;

        foreach (var pair in whitePieces)
        {
            whiteQuality += pair.Value.Value();
        }

        foreach (var pair in blackPieces)
        {
            blackQuality += pair.Value.Value();
        }

        return (whiteQuality) - (blackQuality);
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

            if (blackPiecesUpdated.ContainsKey(newTiles[1].Position())) {
                if (blackPiecesUpdated[newTiles[1].Position()].Notation() == 'K')
                {
                    return new Board(whitePiecesUpdated, blackPiecesUpdated, updatedTiles, new Dictionary<bool, string> {[true] = "white"});
                }

                blackPiecesUpdated.Remove(newTiles[1].Position());
            }
        }
        else
        {
            blackPiecesUpdated.Remove(newTiles[0].Position());
            blackPiecesUpdated[newTiles[1].Position()] = pieceUpdated;

            if (whitePiecesUpdated.ContainsKey(newTiles[1].Position())) {
                if (whitePiecesUpdated[newTiles[1].Position()].Notation() == 'K')
                {
                    return new Board(whitePiecesUpdated, blackPiecesUpdated, updatedTiles, new Dictionary<bool, string> {[true] = "black"});
                }
                
                whitePiecesUpdated.Remove(newTiles[1].Position());
            }
        }

        return new Board(whitePiecesUpdated, blackPiecesUpdated, updatedTiles, gameOver);
    }

    public Board Copy()
    {
        Dictionary<Point, ChessPiece> whitepiecesCopy = new Dictionary<Point, ChessPiece>();
        Dictionary<Point, ChessPiece> blackpiecesCopy = new Dictionary<Point, ChessPiece>();
        Tile[,] tilesCopy = new Tile[8,8];

        foreach (var pair in whitePieces)
        {
            whitepiecesCopy[pair.Key] = pair.Value;
        }

        foreach (var pair in blackPieces)
        {
            blackpiecesCopy[pair.Key] = pair.Value;
        }

        foreach (Tile tile in tiles)
        {
            tilesCopy[tile.Position().y, tile.Position().x] = tile;
        }

        Board board = new Board(whitepiecesCopy, blackpiecesCopy, tilesCopy, gameOver);
        board.SetPieces();
        return board;
    }

    public Tile Tile(int x, int y)
    {
        return tiles[y, x];
    }

    public void Print(Move move)
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

        move.Print();
        Console.Read();
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
                    if (whitePieces.Count - 1 >= k && whitePieces.ElementAt(k).Value.Position().y == i
                            && whitePieces.ElementAt(k).Value.Position().x == j)
                    {
                        tiles[i, j] = new Tile(whitePieces.ElementAt(k).Value, new Point(j, i));
                        break;
                    }

                    if (blackPieces.Count - 1 >= k && blackPieces.ElementAt(k).Value.Position().y == i
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
        Dictionary<Point, ChessPiece> pieces = new Dictionary<Point, ChessPiece>();

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
            ChessPiece piece = ChessPiece.CreatePiece(notations[i], position[(_side * 16) + i], _side, (id++) + (16 * _side));
            pieces.Add(piece.Position(), piece);
        }

        return pieces;
    }
}
