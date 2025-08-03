using Knightmare.Tests;

class Program
{
    private static void RunTests()
    {
        Console.WriteLine("Running tests...");

        var puzzleTest = new PuzzleTest();
        var moveCountTest = new MoveCountTest();
        var performanceTest = new PerformanceTest();

        try
        {
            performanceTest.Execute();
            puzzleTest.Execute();
            //moveCountTest.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }

        Console.WriteLine("Tests completed.\n");
    }

    static void Main(string[] args)
    {
        UCIHandler uciHandler = new UCIHandler();
        bool runTests = args.Length > 0 && args[0] == "test";

        if (runTests)
        {
            RunTests();
        }

        while (true)
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            if (uciHandler.ProcessCommand(input))
            {
                return;
            }
        }
    }
}
