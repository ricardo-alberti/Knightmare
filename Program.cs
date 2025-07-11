using Knightmare.Tests;

class Program
{
    static void Main(string[] args)
    {
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
            moveCountTest.Execute();

            Console.WriteLine("Tests completed.\n");
        }

        Console.WriteLine("id name Knightmare");
        Console.WriteLine("id author Ricardo Alberti");
        Console.WriteLine("uciok");
        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null) continue;

            uciHandler.ProcessCommand(input);
        }
    }
}
