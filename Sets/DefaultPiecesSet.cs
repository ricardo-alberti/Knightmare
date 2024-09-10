using Knightmare.Pieces;

namespace Knightmare.Sets
{
    internal class DefaultPiecesSet
    {
        private readonly PieceCollection white;
        private readonly PieceCollection black;

        public DefaultPiecesSet()
        {
            white = new PieceCollection(
                new List<ChessPiece>() {
                        ChessPiece.Create('R', new Point(0, 0), PlayerSide.White, 17),
                        ChessPiece.Create('R', new Point(7, 0), PlayerSide.White, 18),
                        ChessPiece.Create('N', new Point(1, 0), PlayerSide.White, 19),
                        ChessPiece.Create('N', new Point(6, 0), PlayerSide.White, 20),
                        ChessPiece.Create('B', new Point(2, 0), PlayerSide.White, 21),
                        ChessPiece.Create('B', new Point(5, 0), PlayerSide.White, 22),
                        ChessPiece.Create('K', new Point(4, 0), PlayerSide.White, 23),
                        ChessPiece.Create('Q', new Point(3, 0), PlayerSide.White, 24),

                        ChessPiece.Create('P', new Point(0, 1), PlayerSide.White, 25),
                        ChessPiece.Create('P', new Point(7, 1), PlayerSide.White, 26),
                        ChessPiece.Create('P', new Point(1, 1), PlayerSide.White, 27),
                        ChessPiece.Create('P', new Point(6, 1), PlayerSide.White, 28),
                        ChessPiece.Create('P', new Point(2, 1), PlayerSide.White, 29),
                        ChessPiece.Create('P', new Point(5, 1), PlayerSide.White, 30),
                        ChessPiece.Create('P', new Point(3, 1), PlayerSide.White, 31),
                        ChessPiece.Create('P', new Point(4, 1), PlayerSide.White, 32)
                    }
            );

            black = new PieceCollection(
                new List<ChessPiece>() {
                        ChessPiece.Create('R', new Point(0, 7), PlayerSide.Black, 1),
                        ChessPiece.Create('R', new Point(7, 7), PlayerSide.Black, 2),
                        ChessPiece.Create('N', new Point(1, 7), PlayerSide.Black, 3),
                        ChessPiece.Create('N', new Point(6, 7), PlayerSide.Black, 4),
                        ChessPiece.Create('B', new Point(2, 7), PlayerSide.Black, 5),
                        ChessPiece.Create('B', new Point(5, 7), PlayerSide.Black, 6),
                        ChessPiece.Create('K', new Point(4, 7), PlayerSide.Black, 7),
                        ChessPiece.Create('Q', new Point(3, 7), PlayerSide.Black, 8),

                        ChessPiece.Create('P', new Point(0, 6), PlayerSide.Black, 9),
                        ChessPiece.Create('P', new Point(7, 6), PlayerSide.Black, 10),
                        ChessPiece.Create('P', new Point(1, 6), PlayerSide.Black, 11),
                        ChessPiece.Create('P', new Point(6, 6), PlayerSide.Black, 12),
                        ChessPiece.Create('P', new Point(2, 6), PlayerSide.Black, 13),
                        ChessPiece.Create('P', new Point(5, 6), PlayerSide.Black, 14),
                        ChessPiece.Create('P', new Point(3, 6), PlayerSide.Black, 15),
                        ChessPiece.Create('P', new Point(4, 6), PlayerSide.Black, 16)
                }
            );
        }

        public PieceCollection White()
        {
            return white;
        }

        public PieceCollection Black()
        {
            return black;
        }
    }
}
