
namespace ChessRobot
{

    class Program
    {
        static void Main()
        {
            Game.start();
        }

        public class Game
        {
            private const string Nb = " \u2658";
            private const string Tb = " \u2656";
            private const string Bb = " \u2657";
            private const string Kb = " \u2654";
            private const string Qb = " \u2655";
            private const string Pb = " \u2659";

            private const string Nw = " \u265E";
            private const string Tw = " \u265C";
            private const string Bw = " \u265D";
            private const string Kw = " \u265A";
            private const string Qw = " \u265B";
            private const string Pw = " \u265F";

            private const string E = " \u2022";

            private string[,] board =
            {
                {Tw, Nw, Bw, Qw, Kw, Bw, Nw, Tw},
                {Pw, Pw, Pw, Pw, Pw, Pw, Pw, Pw},
                { E,  E,  E,  E,  E,  E,  E,  E},
                { E,  E,  E,  E,  E,  E,  E,  E},
                { E,  E,  E,  E,  E,  E,  E,  E},
                { E,  E,  E,  E,  E,  E,  E,  E},
                {Pb, Pb, Pb, Pb, Pb, Pb, Pb, Pb},
                {Tb, Nb, Bb, Qb, Kb, Bb, Nb, Tb},
            };

            public class Point
            {
                public int x;
                public int y;
            }

            int[,] pieceMoveSet = { };

            public bool computerMegaBrainThink(bool[,] seen, Stack<Point> path, int movesAhead, Point pieceToMove, List<Point> myPieces, List<Point> enemyPieces)
            {
                //basecase
                if (pieceToMove.x > 7 || pieceToMove.y > 7 || pieceToMove.x < 0 || pieceToMove.y < 0)
                {
                    return false;
                }

                if (seen[pieceToMove.y, pieceToMove.x] == true)
                {
                    return false;
                }

                if (path.Count > 1)
                {
                    Point[] arrayPath = path.ToArray();
                    Array.Reverse(arrayPath);

                    for (int i = 0; i < myPieces.Count; ++i)
                    {
                        if (arrayPath[1].x == myPieces[i].x && arrayPath[1].y == myPieces[i].y)
                        {
                            return false;
                        }
                    }
                }

                if (movesAhead == 1)
                {
                    Point urr = new Point() { x = pieceToMove.x, y = pieceToMove.y };
                    path.Push(urr);

                    return true;
                }

                //preorder
                seen[pieceToMove.y, pieceToMove.x] = true;
                Point curr = new Point() { x = pieceToMove.x, y = pieceToMove.y };

                path.Push(curr);

                switch (board[pieceToMove.y, pieceToMove.x])
                {
                    case Pb:
                        pieceMoveSet = blackPawnMoves;
                        break;
                    case Nb:
                        pieceMoveSet = knightMoves;
                        break;
                    case Kb:
                        pieceMoveSet = kingMoves;
                        break;
                    case Qb:
                        pieceMoveSet = QueenMoves;
                        break;
                    case Bb:
                        pieceMoveSet = bishopMoves;
                        break;
                    case Tb:
                        pieceMoveSet = towerMoves;
                        break;
                    case Pw:
                        pieceMoveSet = whitePawnMoves;
                        break;
                    case Nw:
                        pieceMoveSet = knightMoves;
                        break;
                    case Kw:
                        pieceMoveSet = kingMoves;
                        break;
                    case Qw:
                        pieceMoveSet = QueenMoves;
                        break;
                    case Bw:
                        pieceMoveSet = bishopMoves;
                        break;
                    case Tw:
                        pieceMoveSet = towerMoves;
                        break;
                }
                //order

                for (int i = 0; i < pieceMoveSet.GetLength(0); ++i)
                {
                    int y = pieceMoveSet[i, 0], x = pieceMoveSet[i, 1];

                    Point pieceToMoveUpdated = new Point() { x = pieceToMove.x + x, y = pieceToMove.y + y };
                    int movesAheadUpdated = movesAhead + 1;

                    for (int j = 0; j < myPieces.Count; ++j)
                    {
                        if (pieceToMoveUpdated.x == myPieces[j].x && pieceToMoveUpdated.y == myPieces[j].y)
                        {
                            return false;
                        }
                    }

                    if (computerMegaBrainThink(seen, path, movesAheadUpdated, pieceToMoveUpdated, myPieces, enemyPieces))
                    {
                        return true;
                    }
                }

                //postOrder
                path.Pop();
                return false;
            }

            Random rnd = new Random();

            public void computerTurn(List<Point> myPieces, List<Point> enemyPieces)
            {
                bool[,] seen = new bool[8, 8];

                Console.WriteLine($"number of pieces: {myPieces.Count}");
                for (int i = 0; i < myPieces.Count; ++i)
                {
                    Console.WriteLine($"x:{myPieces[i].x}, y:{myPieces[i].y}");
                }

                Console.WriteLine();

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        seen[i, j] = false;
                    }
                }

                int movesAhead = 0;
                Stack<Point> path = new Stack<Point>();


                getPath();

                void getPath()
                {
                    if (myPieces.Count == 0)
                    {
                    }

                    for (int i = 0; i < myPieces.Count; ++i)
                    {
                        path.Clear();
                        int num = rnd.Next(i, myPieces.Count);

                        if (computerMegaBrainThink(seen, path, movesAhead, myPieces[num], myPieces, enemyPieces))
                        {
                            Point[] arrayPath = path.ToArray();
                            Array.Reverse(arrayPath);
                            Point start = arrayPath[0];
                            Point end = arrayPath[1];

                            for (int j = 0; j < enemyPieces.Count; ++j)
                            {
                                if (end.x == enemyPieces[j].x && end.y == enemyPieces[j].y)
                                {
                                    enemyPieces.RemoveAt(j);
                                }
                            }

                            myPieces[num] = new Point() { x = end.x, y = end.y };

                            for (int k = 0; k < path.Count - 1; ++k)
                            {
                                Console.WriteLine($"{k} x:{arrayPath[k].x} y:{arrayPath[k + 1].y} => x:{arrayPath[k].x} y:{arrayPath[k + 1].y}");
                            }

                            move(start.x, start.y, end.x, end.y);
                            break;
                        }
                    }
                }
            }

            public void humanTurn()
            {
                Console.Write("  Piece start point: ");
                string? pieceFrom = Console.ReadLine();
                Console.Write("  Piece end point: ");
                string? pieceTo = Console.ReadLine();

                int initialX = getPoint(pieceFrom[0]);
                int initialY = getPoint(pieceFrom[1]);
                int endX = getPoint(pieceTo[0]);
                int endY = getPoint(pieceTo[1]);

                if (initialY > 8 || initialX > 8 || endX > 8 || endY > 8 || initialX < 0 || initialY < 0 || endX < 0 || endY < 0)
                {
                    printBoard();
                    Console.WriteLine("  Entrada inválida");
                    humanTurn();
                }

                move(initialX, initialY, endX, endY);

                int getPoint(char XY)
                {
                    char[] collumns = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                    char[] rows = { '1', '2', '3', '4', '5', '6', '7', '8' };

                    for (int i = 0; i < 8; ++i)
                    {
                        if (collumns[i] == XY)
                        {
                            return i;
                        }

                        if (rows[i] == XY)
                        {
                            return i;
                        }
                    }

                    return -1;
                }
            }

            public void move(int initialX, int initialY, int endX, int endY)
            {

                string tmp = board[initialY, initialX];

                if (board[endY, endX] == E)
                {
                    board[initialY, initialX] = board[endY, endX];
                }
                else
                {
                    board[initialY, initialX] = E;
                }

                Console.WriteLine($"{tmp} x{board[endY, endX]}");
                Console.Read();

                if (tmp == Pw && endY == 7)
                {
                    tmp = Qw;
                }

                if (tmp == Pb && endY == 0)
                {
                    tmp = Qb;
                }

                board[endY, endX] = tmp;
            }

            public void printBoard()
            {
                Console.Clear();


                Console.WriteLine("     A B C D E F G H");
                Console.WriteLine("    -----------------");

                for (int i = 0; i < 8; i++)
                {
                    Console.Write($" {i + 1} |");

                    for (int j = 0; j < 8; ++j)
                    {
                        Console.Write(board[i, j]);
                    }

                    Console.WriteLine(" |");
                }
                Console.WriteLine("    -----------------");
            }

            public static void start()
            {
                Game chess = new Game();
                chess.turnsCycle();
            }

            private void turnsCycle()
            {
                if (whitePiecesPosition.Count == 0 || blackPiecesPosition.Count == 0) {
                    Console.Write("ACABOU CHEGA");
                    return;
                }

                computerTurn(whitePiecesPosition, blackPiecesPosition);
                printBoard();

                computerTurn(blackPiecesPosition, whitePiecesPosition);
                printBoard();

                turnsCycle();
            }

            int[,] knightMoves =
            {
                //THIS is how the knight moves
                { 2, 1 },
                { 2, -1 },

                { -2, 1 },
                { -2, -1 },

                { 1, 2 },
                { -1, 2 },

                { 1, -2 },
                {-1, -2 },
            };

            int[,] whitePawnMoves =
            {
                { 1, 0 },
                { 1, 1 },
                { 1, -1 },
            };

            int[,] blackPawnMoves =
            {
                { -1, 0 },
                { -1, 1 },
                { -1, -1 },
            };

            int[,] bishopMoves =
            {
                { 1, 1 },
                { -1, 1 },
                { -1, -1 },
                { 1, -1 },
            };

            int[,] QueenMoves =
            {
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 },
                { 1, 1 },
                { -1, 1 },
                { -1, -1 },
                { 1, -1 },
            };

            int[,] kingMoves =
            {
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 },
            };

            int[,] towerMoves =
            {
                { 1, 0 },
                { 0, 1 },
                { 0, -1 },
                { -1, 0 },
            };

            List<Point> blackPiecesPosition = new List<Point>
            {
                new Point() { x = 0, y = 7 },
                new Point() { x = 1, y = 7 },
                new Point() { x = 2, y = 7 },
                new Point() { x = 3, y = 7 },
                new Point() { x = 4, y = 7 },
                new Point() { x = 5, y = 7 },
                new Point() { x = 6, y = 7 },
                new Point() { x = 7, y = 7 },
                new Point() { x = 0, y = 6 },
                new Point() { x = 1, y = 6 },
                new Point() { x = 2, y = 6 },
                new Point() { x = 3, y = 6 },
                new Point() { x = 4, y = 6 },
                new Point() { x = 5, y = 6 },
                new Point() { x = 6, y = 6 },
                new Point() { x = 7, y = 6 },
            };

            List<Point> whitePiecesPosition = new List<Point>
            {
                new Point() { x = 0, y = 0 },
                new Point() { x = 1, y = 0 },
                new Point() { x = 2, y = 0 },
                new Point() { x = 3, y = 0 },
                new Point() { x = 4, y = 0 },
                new Point() { x = 5, y = 0 },
                new Point() { x = 6, y = 0 },
                new Point() { x = 7, y = 0 },
                new Point() { x = 0, y = 1 },
                new Point() { x = 1, y = 1 },
                new Point() { x = 2, y = 1 },
                new Point() { x = 3, y = 1 },
                new Point() { x = 4, y = 1 },
                new Point() { x = 5, y = 1 },
                new Point() { x = 6, y = 1 },
                new Point() { x = 7, y = 1 },
            };
        }
    }
}
