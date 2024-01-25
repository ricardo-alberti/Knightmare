
namespace ChessRobot
{

    class Program
    {
        static void Main()
        {
            Game chess = new Game();
            chess.makeMove();
        }

        public class Game
        {
            const string Nb = "\u2658";
            const string Tb = "\u2656";
            const string Bb = "\u2657";
            const string Kb = "\u2654";
            const string Qb = "\u2655";
            const string Pb = "\u2659";

            const string Nw = "\u265E";
            const string Tw = "\u265C";
            const string Bw = "\u265D";
            const string Kw = "\u265A";
            const string Qw = "\u265B";
            const string Pw = "\u265F";

            const string E = "#";

            public string[,] board =

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

            public void makeMove()
            {

                //computerThink();
                printBoard();

                Console.Write("Piece start point: ");
                string? pieceFrom = Console.ReadLine();
                Console.Write("Piece end point: ");
                string? pieceTo = Console.ReadLine();

                int initialX = getPoint(pieceFrom[0]);
                int initialY = getPoint(pieceFrom[1]);
                int endX = getPoint(pieceTo[0]);
                int endY = getPoint(pieceTo[1]);

                string tmp = board[initialY, initialX];
                board[initialY, initialX] = board[endY, endX];
                board[endY, endX] = tmp;

                makeMove();
            }

            public void computerThink()
            {
                int initialX = 0;
                int initialY = 0;
                int endX = 0;
                int endY = 0;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == Pw)
                        {
                            for (int k = 0; k < pawnMoves.Length; k++)
                            {
                                if (true)
                                {
                                    initialX = 1;
                                    initialY = 1;
                                    endX = 3;
                                    endY = 3;
                                }
                            }
                        }
                    }
                }

                string tmp = board[initialY, initialX];
                board[initialY, initialX] = board[endY, endX];
                board[endY, endX] = tmp;
            }

            public int getPoint(char XY)
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

            public void printBoard()
            {
                Console.Clear();

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; ++j)
                    {
                        Console.Write(board[i, j]);
                    }

                    Console.WriteLine();
                }
            }

            int[,] knightMoves =
            {
                { 3, 1 },
                { 3, -1 },

                { -3, 1 },
                { -3, -1 },

                { 1, 3 },
                { -1, 3 },

                { 1, -3 },
                {-1, -3 },
            };

            int[,] pawnMoves =
            {
                { 1, 0 },
                { 1, 1 },
                { -1, 1 },
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
                { 1, 1 },
                { -1, 1 },
                { -1, -1 },
                { 1, -1 },
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 },
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
                { -1, 0 },
                { 0, -1 },
            };
        }
    }
}
