using Knightmare.Tests;

class Program
{
    static void Main()
    {
        PuzzleTest puzzleTest = new();
        MoveCountTest moveCountTest = new();

        if (false)
        {
            puzzleTest.Execute();
            moveCountTest.Execute();
        }

        Game chess = new Game();
        chess.Start();
    }
}

