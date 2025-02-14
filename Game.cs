internal sealed class Game
{
    private readonly UCIHandler uciHandler;

    public Game()
    {
        uciHandler = new UCIHandler();
    }

    public void Start()
    {
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

