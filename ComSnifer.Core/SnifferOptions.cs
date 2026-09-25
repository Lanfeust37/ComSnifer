using System.IO.Ports;

namespace ComSnifer;

public sealed class OptionException : Exception
{
    public OptionException(string message) : base(message) { }
}

/// <summary>
/// Configuration du sniffer. Equivalent des options de slsnif, avec parsing
/// borne (pas de sscanf/%s non limite) et comparaisons insensibles a la casse
/// (corrige le bug des str2upper/str2lower no-op de la version C).
/// </summary>
public sealed class SnifferOptions
{
    public string? DevicePort { get; set; }
    public string? AppPort { get; set; }
    public int BaudRate { get; set; } = 9600;
    public int DataBits { get; set; } = 8;
    public Parity Parity { get; set; } = Parity.None;
    public StopBits StopBits { get; set; } = StopBits.One;
    public Handshake Handshake { get; set; } = Handshake.None;
    public bool ShowBytes { get; set; }
    public bool ShowTimestamp { get; set; }
    public bool Hex { get; set; }
    public bool ShowHelp { get; set; }
    public bool ListPorts { get; set; }
    public string? LogFile { get; set; }
    public List<string> InTeeFiles { get; } = new();
    public List<string> OutTeeFiles { get; } = new();
    public string Color { get; set; } = "white";
    public string TimeColor { get; set; } = "cyan";
    public string BytesColor { get; set; } = "yellow";

    /// <summary>
    /// Parse le rc-file puis les arguments (la CLI a priorite).
    /// <paramref name="rcPath"/> remplace l'emplacement par defaut (tests).
    /// </summary>
    public static SnifferOptions Parse(string[] args, string? rcPath = null)
    {
        var opts = new SnifferOptions();
        opts.LoadRcFile(rcPath ?? DefaultRcPath());
        opts.ApplyArgs(args);
        return opts;
    }

    private static string DefaultRcPath()
    {
        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(home, ".slsnifrc");
    }

    private void ApplyArgs(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            string? inline = null;
            int eq = arg.IndexOf('=');
            if (eq >= 0 && arg.StartsWith("--"))
            {
                inline = arg[(eq + 1)..];
                arg = arg[..eq];
            }

            string Next()
            {
                if (inline is not null) return inline;
                if (++i >= args.Length)
                    throw new OptionException($"L'option {arg} requiert un argument.");
                return args[i];
            }

            switch (arg)
            {
                case "-h": case "--help": ShowHelp = true; break;
                case "-l": case "--list": ListPorts = true; break;
                case "-b": case "--bytes": ShowBytes = true; break;
                case "-t": case "--timestamp": ShowTimestamp = true; break;
                case "-x": case "--hex": Hex = true; break;
                case "-d": case "--device": DevicePort = Next(); break;
                case "-a": case "--appport": AppPort = Next(); break;
                case "-s": case "--speed": BaudRate = ParseInt(Next(), arg); break;
                case "--databits": DataBits = ParseInt(Next(), arg); break;
                case "--parity": Parity = ParseEnum<Parity>(Next(), arg); break;
                case "--stopbits": StopBits = ParseStopBits(Next()); break;
                case "--handshake": Handshake = ParseEnum<Handshake>(Next(), arg); break;
                case "--log": LogFile = Next(); break;
                case "-i": case "--in-tee": InTeeFiles.Add(Next()); break;
                case "-o": case "--out-tee": OutTeeFiles.Add(Next()); break;
                case "--color": Color = Next(); break;
                case "--timecolor": TimeColor = Next(); break;
                case "--bytescolor": BytesColor = Next(); break;
                default:
                    if (arg.StartsWith('-'))
                        throw new OptionException($"Option inconnue : {arg}");
                    if (DevicePort is null) DevicePort = arg;
                    else if (AppPort is null) AppPort = arg;
                    else throw new OptionException($"Argument inattendu : {arg}");
                    break;
            }
        }
    }

    /// <summary>
    /// Lit %USERPROFILE%\.slsnifrc : paires "CLE valeur" (casse indifferente,
    /// lignes '#' ignorees). Toute ligne mal formee est ignoree — aucun buffer
    /// non borne n'est utilise, contrairement au sscanf de la version C.
    /// </summary>
    private void LoadRcFile(string path)
    {
        if (path.Length == 0 || !File.Exists(path)) return;

        foreach (string raw in File.ReadLines(path))
        {
            string line = raw.Trim();
            if (line.Length == 0 || line.StartsWith('#')) continue;
            int sp = line.IndexOfAny([' ', '\t']);
            if (sp < 0) continue;
            string key = line[..sp];
            string value = line[(sp + 1)..].Trim();

            bool On() => value.Equals("ON", StringComparison.OrdinalIgnoreCase);
            switch (key.ToUpperInvariant())
            {
                case "SPEED": if (int.TryParse(value, out int s)) BaudRate = s; break;
                case "DATABITS": if (int.TryParse(value, out int d)) DataBits = d; break;
                case "PARITY": if (Enum.TryParse<Parity>(value, true, out var p)) Parity = p; break;
                case "STOPBITS": try { StopBits = ParseStopBits(value); } catch (OptionException) { } break;
                case "HANDSHAKE": if (Enum.TryParse<Handshake>(value, true, out var h)) Handshake = h; break;
                case "TOTALBYTES": ShowBytes = On(); break;
                case "TIMESTAMP": ShowTimestamp = On(); break;
                case "DISPLAYHEX": Hex = On(); break;
                case "COLOR": Color = value; break;
                case "TIMECOLOR": TimeColor = value; break;
                case "BYTESCOLOR": BytesColor = value; break;
                case "LOG": LogFile = value; break;
                case "INTEE": InTeeFiles.Add(value); break;
                case "OUTTEE": OutTeeFiles.Add(value); break;
            }
        }
    }

    private static int ParseInt(string v, string opt) =>
        int.TryParse(v, out int n)
            ? n
            : throw new OptionException($"Valeur invalide pour {opt} : '{v}'");

    private static T ParseEnum<T>(string v, string opt) where T : struct =>
        Enum.TryParse<T>(v, true, out T e)
            ? e
            : throw new OptionException($"Valeur invalide pour {opt} : '{v}'");

    private static StopBits ParseStopBits(string v) => v switch
    {
        "1" => StopBits.One,
        "1.5" => StopBits.OnePointFive,
        "2" => StopBits.Two,
        _ => throw new OptionException($"Valeur invalide pour --stopbits : '{v}' (1, 1.5 ou 2)"),
    };

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(DevicePort))
            throw new OptionException("Endpoint du peripherique requis (--device COMx | tcp:hote:port).");
        if (string.IsNullOrWhiteSpace(AppPort))
            throw new OptionException("Endpoint cote application requis (--appport COMx | tcp:... | listen:...).");
        if (DevicePort.Equals(AppPort, StringComparison.OrdinalIgnoreCase))
            throw new OptionException("Les deux endpoints doivent etre differents.");
        // leve OptionException si les specs sont mal formees
        _ = EndpointSpec.Parse(DevicePort);
        _ = EndpointSpec.Parse(AppPort);
        if (BaudRate <= 0)
            throw new OptionException("Baudrate invalide.");
        if (DataBits is < 5 or > 8)
            throw new OptionException("DataBits doit etre entre 5 et 8.");
        if (AnsiColors.Get(Color) is null)
            throw new OptionException($"Couleur inconnue : '{Color}'");
        if (AnsiColors.Get(TimeColor) is null)
            throw new OptionException($"Couleur inconnue : '{TimeColor}'");
        if (AnsiColors.Get(BytesColor) is null)
            throw new OptionException($"Couleur inconnue : '{BytesColor}'");
    }
}
