
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
            public int[] board =
            {
              2,3,4,5,6,4,3,2,
              1,1,1,1,1,1,1,1,
              0,0,0,0,0,0,0,0,
              0,0,0,0,0,0,0,0,
              0,0,0,0,0,0,0,0,
              0,0,0,0,0,0,0,0,
              1,1,1,1,1,1,1,1,
              2,3,4,5,6,4,3,2,
            };

            const int knight = 3;
            const int Tower = 2;
            const int Bishop = 4;
            const int King = 6;
            const int Queen = 5;
            const int Pawn = 1;
            const int EmptyTile = 0;

            public void makeMove()
            {
                string move = Console.ReadLine();

                if (move == "e4")
                {
                    board[36] = 1;
                    board[52] = 0;
                }

                printBoard();
            }

            private void printBoard()
            {
                Console.Clear();

                for (int i = 0; i < 64; i++)
                {
                    if (i % 8 == 0)
                    {
                        Console.WriteLine();
                    }

                    Console.Write(board[i]);
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
