using Knightmare.Tests;

class Program
{
    static void Main()
    {
        AppSettings.Create("./appsettings.json");

        PuzzleTest puzzleTest = new();
        MoveCountTest moveCountTest = new();

        Human human = new(PlayerSide.Black);
        Robot robot = new(PlayerSide.White, AppSettings.Instance.WhiteLevel);
        string initialPosition = AppSettings.Instance.InitialPosition;

        if (AppSettings.Instance.Debug)
        {
            puzzleTest.Execute();
            moveCountTest.Execute();
        }

        Game chess = new Game(robot, human, initialPosition);
        chess.Start();
    }
}

