using Knightmare.Tests;

class Program
{
    static void Main(string[] args)
    {
        Magic.InitializeRookAttacks();
        Magic.InitializeBishopAttacks();

        UCIHandler uciHandler = new UCIHandler();
        bool runTests = args.Length > 0 && args[0] == "test";

        if (runTests)
        {
            Console.WriteLine("Running tests...");
            PuzzleTest puzzleTest = new();
            MoveCountTest moveCountTest = new();
            PerformanceTest performanceTest = new();

            performanceTest.Execute();
            puzzleTest.Execute();
            //moveCountTest.Execute();

            Console.WriteLine("Tests completed.\n");
        }

        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null) continue;

            uciHandler.ProcessCommand(input);
        }
    }
}
