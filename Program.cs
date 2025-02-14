using Knightmare.Tests;

class Program
{
    static void Main(string[] args)
    {
        bool runTests = args.Length > 0 && args[0] == "test";

        if (runTests)
        {
            Console.WriteLine("Running tests...");
            PuzzleTest puzzleTest = new();
            MoveCountTest moveCountTest = new();

            puzzleTest.Execute();
            moveCountTest.Execute();

            Console.WriteLine("Tests completed.\n");
        }

        Game chess = new Game();
        chess.Start();
    }
}
