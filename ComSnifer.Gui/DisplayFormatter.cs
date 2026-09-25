using System.Text;

namespace ComSnifer.Gui;

/// <summary>
/// Format compact pour l'affichage GUI : une ligne par paquet,
/// octets en hexa ou decimal, colonne ASCII optionnelle.
///   12:01:33.412  48 65 6C 6C 6F   | Hello
/// </summary>
internal static class DisplayFormatter
{
    public static string FormatLine(ReadOnlySpan<byte> data, bool hex, bool ascii,
                                    DateTimeOffset? timestamp)
    {
        var sb = new StringBuilder(data.Length * 4 + 24);
        if (timestamp is { } t)
            sb.Append(t.ToString("HH:mm:ss.fff")).Append("  ");

        foreach (byte b in data)
            sb.Append(hex ? b.ToString("x2") : b.ToString("D3")).Append(' ');

        if (ascii)
        {
            sb.Append("| ");
            foreach (byte b in data)
                sb.Append(b is >= 32 and <= 126 ? (char)b : '.');
        }
        return sb.AppendLine().ToString();
    }

    public static string FormatRate(double bytesPerSec) =>
        bytesPerSec < 1000 ? $"{bytesPerSec:F0} o/s" : $"{bytesPerSec / 1000:F1} ko/s";
}
