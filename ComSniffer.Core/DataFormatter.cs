using System.Text;

namespace ComSniffer;

/// <summary>
/// Formatage identique a la version C : "A (065) " en decimal, "A (41) " en hex.
/// Construit dans un StringBuilder borne en memoire — pas de strcat O(n^2)
/// ni de buffer fixe a risque de debordement.
/// </summary>
public static class DataFormatter
{
    private static readonly string[] AsciiNames =
    [
        "<NUL>", "<SOH>", "<STX>", "<ETX>", "<EOT>", "<ENQ>", "<ACK>", "<BEL>",
        "<BS>",  "<HT>",  "<LF>",  "<VT>",  "<FF>",  "<CR>",  "<SO>",  "<SI>",
        "<DLE>", "<DC1>", "<DC2>", "<DC3>", "<DC4>", "<NAK>", "<SYN>", "<ETB>",
        "<CAN>", "<EM>",  "<SUB>", "<ESC>", "<FS>",  "<GS>",  "<RS>",  "<US>",
        "<SPACE>"
    ];

    private const string Del = "<DEL>";

    public static string Format(ReadOnlySpan<byte> data, bool hex)
    {
        var sb = new StringBuilder(data.Length * 8);
        foreach (byte b in data)
        {
            string name = b switch
            {
                127 => Del,
                <= 32 => AsciiNames[b],
                _ => ((char)b).ToString(),
            };
            sb.Append(name).Append(' ').Append('(');
            sb.Append(hex ? b.ToString("x2") : b.ToString("D3"));
            sb.Append(')').Append(' ');
        }
        return sb.ToString();
    }
}

/// <summary>Sequences ANSI, lookup insensible a la casse (corrige str2lower).</summary>
public static class AnsiColors
{
    private static readonly Dictionary<string, string> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["black"] = "\x1b[0;30m",         ["red"] = "\x1b[0;31m",
        ["green"] = "\x1b[0;32m",         ["yellow"] = "\x1b[0;33m",
        ["blue"] = "\x1b[0;34m",          ["magenta"] = "\x1b[0;35m",
        ["cyan"] = "\x1b[0;36m",          ["white"] = "\x1b[0;37m",
        ["brightblack"] = "\x1b[1;30m",   ["brightred"] = "\x1b[1;31m",
        ["brightgreen"] = "\x1b[1;32m",   ["brightyellow"] = "\x1b[1;33m",
        ["brightblue"] = "\x1b[1;34m",    ["brightmagenta"] = "\x1b[1;35m",
        ["brightcyan"] = "\x1b[1;36m",    ["brightwhite"] = "\x1b[1;37m",
    };

    public const string Reset = "\x1b[0m";

    public static string? Get(string? name) =>
        name is not null && Map.TryGetValue(name, out string? code) ? code : null;
}
