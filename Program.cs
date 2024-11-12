using Knightmare.Tests;

class Program
{
    static void Main()
    {
        AppSettings.Create("./appsettings.json");

        PuzzleTest test = new();
        test.Execute();

        MoveCountTest test2 = new();
        test2.Execute();

        Game chess = new Game();
        chess.Start();
    }
}

