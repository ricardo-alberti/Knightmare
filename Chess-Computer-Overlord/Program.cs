
public class Game
{
    string[] board = 

    { "TNBQKBCT",
      "PPPPPPPP",
      "########",
      "###00###",
      "###00###",
      "########",
      "PPPPPPPP",
      "TNBQKBCT",
    };

    const char knight = 'N';
    const char Tower = 'T';
    const char Bishop = 'B';
    const char King = 'K';
    const char Queen = 'Q';
    const char Pawn = 'P';
    const char CenterTile = '0';

    int[,] knightMoves =
    {
        //(x, y)
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
        //(x, y)
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

    //To be continue...

}
