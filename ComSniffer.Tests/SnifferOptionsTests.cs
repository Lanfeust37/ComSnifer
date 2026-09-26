using System.IO.Ports;
using ComSniffer;

public class SnifferOptionsTests
{
    // chemin inexistant -> aucun rc-file n'est lu pendant les tests
    private static readonly string NoRc = Path.Combine(Path.GetTempPath(), "slsnif_no_such_rc_file");

    private static SnifferOptions Parse(params string[] args) =>
        SnifferOptions.Parse(args, NoRc);

    [Fact]
    public void Defaults_Are9600_8N1()
    {
        SnifferOptions o = Parse("-d", "COM1", "-a", "COM2");
        Assert.Equal(9600, o.BaudRate);
        Assert.Equal(8, o.DataBits);
        Assert.Equal(Parity.None, o.Parity);
        Assert.Equal(StopBits.One, o.StopBits);
        Assert.Equal(Handshake.None, o.Handshake);
        Assert.False(o.Hex);
        Assert.False(o.ShowBytes);
        Assert.False(o.ShowTimestamp);
    }

    [Fact]
    public void FullArgs_AreParsed()
    {
        SnifferOptions o = Parse("-d", "COM1", "-a", "COM11", "-s", "115200",
                                 "-x", "-t", "-b", "--parity", "even", "--stopbits", "2");
        Assert.Equal("COM1", o.DevicePort);
        Assert.Equal("COM11", o.AppPort);
        Assert.Equal(115200, o.BaudRate);
        Assert.True(o.Hex);
        Assert.True(o.ShowTimestamp);
        Assert.True(o.ShowBytes);
        Assert.Equal(Parity.Even, o.Parity);
        Assert.Equal(StopBits.Two, o.StopBits);
    }

    [Fact]
    public void InlineEqualsForm_Works()
    {
        SnifferOptions o = Parse("--device=COM3", "--appport=COM4", "--speed=4800");
        Assert.Equal("COM3", o.DevicePort);
        Assert.Equal("COM4", o.AppPort);
        Assert.Equal(4800, o.BaudRate);
    }

    [Fact]
    public void PositionalPorts_Work()
    {
        SnifferOptions o = Parse("COM1", "COM2");
        Assert.Equal("COM1", o.DevicePort);
        Assert.Equal("COM2", o.AppPort);
    }

    [Fact]
    public void RepeatableTeeFiles_Accumulate()
    {
        SnifferOptions o = Parse("-d", "COM1", "-a", "COM2",
                                 "-i", "a.bin", "--in-tee", "b.bin", "-o", "out.bin");
        Assert.Equal(["a.bin", "b.bin"], o.InTeeFiles);
        Assert.Equal(["out.bin"], o.OutTeeFiles);
    }

    [Fact]
    public void MissingPorts_FailValidation()
    {
        Assert.Throws<OptionException>(() => Parse("-a", "COM2").Validate());
        Assert.Throws<OptionException>(() => Parse("-d", "COM1").Validate());
    }

    [Fact]
    public void SamePort_Twice_FailsValidation()
    {
        Assert.Throws<OptionException>(() => Parse("COM1", "com1").Validate());
    }

    [Theory]
    [InlineData("--bogus")]
    [InlineData("-Z")]
    public void UnknownOption_Throws(string opt)
    {
        Assert.Throws<OptionException>(() => Parse("-d", "COM1", "-a", "COM2", opt));
    }

    [Fact]
    public void MissingOptionArgument_Throws()
    {
        Assert.Throws<OptionException>(() => Parse("-d", "COM1", "-a", "COM2", "-s"));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("9600x")]
    public void InvalidSpeed_Throws(string v)
    {
        Assert.Throws<OptionException>(() => Parse("-d", "COM1", "-a", "COM2", "-s", v));
    }

    [Fact]
    public void InvalidStopBits_Throws()
    {
        Assert.Throws<OptionException>(() =>
            Parse("-d", "COM1", "-a", "COM2", "--stopbits", "3"));
    }

    [Fact]
    public void InvalidColor_FailsValidation()
    {
        Assert.Throws<OptionException>(() =>
            Parse("-d", "COM1", "-a", "COM2", "--color", "bleu").Validate());
    }

    [Fact]
    public void RcFile_IsApplied()
    {
        string rc = Path.Combine(Path.GetTempPath(), $"slsnifrc_{Guid.NewGuid():N}");
        File.WriteAllText(rc, """
            # commentaire
            SPEED 19200
            TIMESTAMP on
            DISPLAYHEX ON
            COLOR green
            PARITY even
            ligne_sans_valeur
            """);
        try
        {
            // 'on' minuscule fonctionne : corrige le bug str2upper no-op du C
            SnifferOptions o = SnifferOptions.Parse(["-d", "COM1", "-a", "COM2"], rc);
            Assert.Equal(19200, o.BaudRate);
            Assert.True(o.ShowTimestamp);
            Assert.True(o.Hex);
            Assert.Equal("green", o.Color);
            Assert.Equal(Parity.Even, o.Parity);
        }
        finally { File.Delete(rc); }
    }

    [Fact]
    public void CliArgs_OverrideRcFile()
    {
        string rc = Path.Combine(Path.GetTempPath(), $"slsnifrc_{Guid.NewGuid():N}");
        File.WriteAllText(rc, "SPEED 19200\nTIMESTAMP OFF\n");
        try
        {
            SnifferOptions o = SnifferOptions.Parse(
                ["-d", "COM1", "-a", "COM2", "-s", "4800", "-t"], rc);
            Assert.Equal(4800, o.BaudRate);
            Assert.True(o.ShowTimestamp);
        }
        finally { File.Delete(rc); }
    }
}
