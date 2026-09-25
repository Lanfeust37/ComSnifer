using System.Text.Json;

namespace ComSnifer.Gui;

/// <summary>
/// Derniere configuration de la GUI, persistee en JSON
/// (%APPDATA%\ComSnifer\settings.json) — equivalent du .slsnifrc pour la CLI.
/// </summary>
public sealed class GuiSettings
{
    public string Device { get; set; } = "";
    public string App { get; set; } = "";
    public int BaudRate { get; set; } = 9600;
    public int DataBits { get; set; } = 8;
    public string Parity { get; set; } = "None";
    public string StopBits { get; set; } = "1";
    public string Handshake { get; set; } = "None";
    public bool Hex { get; set; } = true;
    public bool Ascii { get; set; } = true;
    public bool Timestamp { get; set; } = true;
    public string LogFile { get; set; } = "";
    public string TeeIn { get; set; } = "";
    public string TeeOut { get; set; } = "";

    private static readonly JsonSerializerOptions Json = new() { WriteIndented = true };

    public static string DefaultPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ComSnifer", "settings.json");

    public static GuiSettings Load(string path)
    {
        try
        {
            if (File.Exists(path))
                return JsonSerializer.Deserialize<GuiSettings>(File.ReadAllText(path)) ?? new();
        }
        catch { }
        return new();
    }

    public void Save(string path)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, JsonSerializer.Serialize(this, Json));
        }
        catch { }
    }
}
