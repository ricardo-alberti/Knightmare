
namespace ChessRobot
{

    class Program
    {
        static void Main()
        {
            Game chess = new Game();
            chess.startMatch();
        }

        public class Game
        {
            //computer settings  

            public int[,] path = new int[12, 12];
            public int[] start;
            public bool[,] seen;
            public int score = 0;

            const string Nb = " \u2658";
            const string Tb = " \u2656";
            const string Bb = " \u2657";
            const string Kb = " \u2654";
            const string Qb = " \u2655";
            const string Pb = " \u2659";

            const string Nw = " \u265E";
            const string Tw = " \u265C";
            const string Bw = " \u265D";
            const string Kw = " \u265A";
            const string Qw = " \u265B";
            const string Pw = " \u265F";

            const string E = " #";

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

            public void startMatch()
            {
                makeMove();
            }

            public void makeMove()
            {

                //computerMegaBrainThink();
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

                if (board[endY, endX] == E)
                {
                    board[initialY, initialX] = board[endY, endX];
                }
                else
                {
                    board[initialY, initialY] = E;
                }

                board[endY, endX] = tmp;

                makeMove();
            }

            public bool computerMegaBrainThink(int[,] path, int[] curr, bool[] seen, int score)
            {
                //base case
                if (curr[0] < 8 || curr[1] < 8 || curr[0] < 0 || curr[1] < 0)
                {
                    return false;
                }

                if (seen[0] == true)
                {
                    return false;
                }

                if (score == 10)
                {
                    return true;
                }

                for (int i = 0; i < pawnMoves.Length; ++i)
                {
                    int x = pawnMoves[i, 0];
                    int y = pawnMoves[i, 1];

                    if (computerMegaBrainThink(path, [0 + x, 1 + y], seen, score))
                    {
                        return true;
                    }
                }
                //recursive
                //pre
                
                //seen.push

                //order
                computerMegaBrainThink(path, curr, seen, score);
                //post
                
                //seen.pop
                return false;
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

                Console.WriteLine("     ₍ᐢ•(ܫ)•ᐢ₎ ");
                Console.WriteLine(" _________________");

                for (int i = 0; i < 8; i++)
                {
                    Console.Write("|");

                    for (int j = 0; j < 8; ++j)
                    {
                        Console.Write(board[i, j]);
                    }

                    Console.WriteLine(" |");
                }
                Console.WriteLine(" -----------------");
            }

            int[,] knightMoves =
            {
                //this is how the knight moves
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
