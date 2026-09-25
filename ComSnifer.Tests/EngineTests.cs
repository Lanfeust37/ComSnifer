using System.Net;
using System.Net.Sockets;
using ComSnifer;

public class EngineTests
{
    private static readonly string NoRc = Path.Combine(Path.GetTempPath(), "slsnif_no_such_rc_file");

    private static int FreePort()
    {
        var l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        return port;
    }

    /// <summary>
    /// Test end-to-end : faux peripherique = serveur echo TCP,
    /// fausse application = client TCP. Verifie le forwarding dans les 2 sens.
    /// </summary>
    [Fact]
    public async Task TcpToTcp_ForwardsBothWays()
    {
        int devPort = FreePort();
        int appPort = FreePort();

        var echo = new TcpListener(IPAddress.Loopback, devPort);
        echo.Start();
        Task echoTask = Task.Run(async () =>
        {
            using TcpClient c = await echo.AcceptTcpClientAsync();
            NetworkStream s = c.GetStream();
            var buf = new byte[1024];
            int n = await s.ReadAsync(buf);
            await s.WriteAsync(buf.AsMemory(0, n));
            echo.Stop();
        });

        SnifferOptions opts = SnifferOptions.Parse(
            ["-d", $"tcp:127.0.0.1:{devPort}", "-a", $"listen:{appPort}"], NoRc);
        opts.Validate();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
        await using var engine = new SnifferEngine(opts);
        Task runTask = engine.RunAsync(cts.Token);

        using var app = new TcpClient();
        await app.ConnectAsync("127.0.0.1", appPort, cts.Token);
        NetworkStream stream = app.GetStream();

        byte[] payload = "PING"u8.ToArray();
        await stream.WriteAsync(payload, cts.Token);
        var reply = new byte[payload.Length];
        await stream.ReadExactlyAsync(reply, cts.Token);

        Assert.Equal(payload, reply);   // aller + retour a travers le sniffer

        cts.Cancel();
        await runTask;
        await echoTask;
    }
}
