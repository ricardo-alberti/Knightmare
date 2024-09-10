namespace Knightmare.Pieces
{
    internal class PieceCollection
    {
        private readonly Dictionary<Point, ChessPiece> pieces;
        private readonly List<ChessPiece> pieceList;

        public PieceCollection() : this(new List<ChessPiece>())
        {

        }

        public PieceCollection(Dictionary<Point, ChessPiece> _pieces)
        {
            pieces = _pieces;
            pieceList = new List<ChessPiece>();

            foreach (var pair in _pieces)
            {
                pieceList.Add(pair.Value);
            }
        }

        public PieceCollection(List<ChessPiece> _pieces)
        {
            pieceList = _pieces;
            pieces = new Dictionary<Point, ChessPiece>();

            foreach (ChessPiece piece in _pieces)
            {
                try
                {
                    pieces.Add(piece.Position(), piece);
                }
                catch
                {
                    Console.Write($"{piece.Position().x}, {piece.Position().y}");
                    Console.Read();
                }
            }
        }

        public Dictionary<Point, ChessPiece> List()
        {
            return pieces;
        }
    }
}
