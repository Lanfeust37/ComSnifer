using ComSnifer;

public class EndpointSpecTests
{
    [Theory]
    [InlineData("COM5")]
    [InlineData(@"\\.\COM15")]
    [InlineData("com1")]
    public void SerialPorts_ParseAsSerial(string spec)
    {
        EndpointSpec ep = EndpointSpec.Parse(spec);
        Assert.Equal(EndpointKind.Serial, ep.Kind);
        Assert.Equal(spec, ep.Host);
    }

    [Fact]
    public void TcpConnect_Parses()
    {
        EndpointSpec ep = EndpointSpec.Parse("tcp:192.168.1.20:4001");
        Assert.Equal(EndpointKind.TcpConnect, ep.Kind);
        Assert.Equal("192.168.1.20", ep.Host);
        Assert.Equal(4001, ep.Port);
    }

    [Fact]
    public void TcpConnect_Hostname_Parses()
    {
        EndpointSpec ep = EndpointSpec.Parse("tcp:monserveur.local:23");
        Assert.Equal("monserveur.local", ep.Host);
        Assert.Equal(23, ep.Port);
    }

    [Fact]
    public void Listen_PortOnly()
    {
        EndpointSpec ep = EndpointSpec.Parse("listen:5000");
        Assert.Equal(EndpointKind.TcpListen, ep.Kind);
        Assert.Equal("", ep.Host);
        Assert.Equal(5000, ep.Port);
    }

    [Fact]
    public void Listen_WithAddress()
    {
        EndpointSpec ep = EndpointSpec.Parse("listen:127.0.0.1:5000");
        Assert.Equal(EndpointKind.TcpListen, ep.Kind);
        Assert.Equal("127.0.0.1", ep.Host);
        Assert.Equal(5000, ep.Port);
    }

    [Fact]
    public void Prefixes_AreCaseInsensitive()
    {
        Assert.Equal(EndpointKind.TcpConnect, EndpointSpec.Parse("TCP:host:1").Kind);
        Assert.Equal(EndpointKind.TcpListen, EndpointSpec.Parse("LISTEN:1").Kind);
    }

    [Theory]
    [InlineData("tcp:")]
    [InlineData("tcp:host")]
    [InlineData("tcp:host:")]
    [InlineData("tcp:host:abc")]
    [InlineData("tcp:host:0")]
    [InlineData("tcp:host:99999")]
    [InlineData("listen:")]
    [InlineData("listen:xyz")]
    [InlineData("listen:not.an.ip:5000")]
    [InlineData("")]
    public void InvalidSpecs_Throw(string spec)
    {
        Assert.Throws<OptionException>(() => EndpointSpec.Parse(spec));
    }

    [Fact]
    public void Describe_Formats()
    {
        Assert.Equal("COM5", EndpointSpec.Parse("COM5").Describe());
        Assert.Equal("tcp://h:9", EndpointSpec.Parse("tcp:h:9").Describe());
        Assert.Equal("listen:*:9", EndpointSpec.Parse("listen:9").Describe());
    }
}
