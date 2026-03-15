class Program
{
    static void Main(string[] args)
    {
        UCIHandler uciHandler = new UCIHandler();

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
