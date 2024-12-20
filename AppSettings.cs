using System.Text.Json;

public class AppSettings
{
    public static AppSettings? Instance { get; set; }

    public bool Debug { get; set; }
    public bool GUI { get; set; }
    public string? InitialPosition { get; set; }
    public int WhiteLevel { get; set; }
    public int BlackLevel { get; set; }

    public static void Create(string configPath)
    {
        if (Instance == null)
        {
            try
            {
                string json = File.ReadAllText(configPath);
                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(json);

                if (settings != null)
                {
                    Instance = settings;
                }
                else
                {
                    Console.WriteLine("Error: Failed to deserialize settings.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings from file: {ex.Message}");
            }
        }
    }
}
