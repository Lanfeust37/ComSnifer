using ComSnifer;

public class DataFormatterTests
{
    [Fact]
    public void Decimal_PrintableChar()
    {
        Assert.Equal("A (065) ", DataFormatter.Format("A"u8.ToArray(), hex: false));
    }

    [Fact]
    public void Hex_PrintableChar()
    {
        Assert.Equal("A (41) ", DataFormatter.Format("A"u8.ToArray(), hex: true));
    }

    [Fact]
    public void ControlChars_UseAsciiNames()
    {
        Assert.Equal("<NUL> (000) <LF> (010) ", DataFormatter.Format([0, 10], hex: false));
        Assert.Equal("<ESC> (1b) ", DataFormatter.Format([27], hex: true));
    }

    [Fact]
    public void Space_And_Del_AreNamed()
    {
        Assert.Equal("<SPACE> (032) <DEL> (127) ", DataFormatter.Format([32, 127], hex: false));
        Assert.Equal("<DEL> (7f) ", DataFormatter.Format([127], hex: true));
    }

    [Fact]
    public void HighBytes_AreFormatted()
    {
        string s = DataFormatter.Format([200], hex: true);
        Assert.EndsWith(" (c8) ", s);
    }

    [Fact]
    public void EmptyInput_EmptyOutput()
    {
        Assert.Equal("", DataFormatter.Format([], hex: false));
    }
}

public class AnsiColorsTests
{
    [Theory]
    [InlineData("red")]
    [InlineData("Red")]       // insensible a la casse (corrige str2lower no-op)
    [InlineData("BRIGHTCYAN")]
    public void KnownColors_ReturnCode(string name)
    {
        Assert.NotNull(AnsiColors.Get(name));
    }

    [Theory]
    [InlineData("bleu")]
    [InlineData("")]
    [InlineData(null)]
    public void UnknownColors_ReturnNull(string? name)
    {
        Assert.Null(AnsiColors.Get(name));
    }
}
