using Knightmare.Tests;

class Program
{
    static void Main()
    {
        PuzzleTest test = new();
        test.Execute();

        MoveCountTest test2 = new();
        test2.Execute();

        Game chess = new Game();
        chess.Start();
    }
}

