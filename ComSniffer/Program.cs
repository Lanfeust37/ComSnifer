using System.IO.Ports;
using ComSnifer;

static void Usage()
{
    Console.WriteLine("""
        Serial Line Sniffer (C#/.NET 10) — version securisee de slsnif 0.4.4

        Usage : ComSnifer --device <port> --appport <port> [options]

        ENDPOINTS REQUIS (serie ou TCP) :
          -d, --device  <ep>    peripherique reel :
                                COMx | \\.\COMx | tcp:hote:port
          -a, --appport <ep>    cote application :
                                COMx | tcp:hote:port | listen:port | listen:ip:port
                                (COMx = paire virtuelle com0com ou port physique ;
                                 listen:xxx = l'application se connecte a nous)

        OPTIONS :
          -h, --help            cette aide
          -l, --list            liste les ports serie disponibles
          -s, --speed <baud>    baudrate (defaut : 9600)
          --databits <5-8>      bits de donnees (defaut : 8)
          --parity <p>          none | even | odd | mark | space
          --stopbits <s>        1 | 1.5 | 2
          --handshake <h>       none | xonxoff | rts | rtsxonxoff
          -x, --hex             affichage hexadecimal
          -t, --timestamp       horodatage (ms reelles) par paquet
          -b, --bytes           nombre d'octets par paquet
          --log <fichier>       sortie formatee vers fichier (defaut : stdout)
          -i, --in-tee <fich.>  octets bruts device -> host (repetable)
          -o, --out-tee <fich.> octets bruts host -> device (repetable)
          --color      <c>      couleur des donnees
          --timecolor  <c>      couleur des timestamps
          --bytescolor <c>      couleur du compteur d'octets

        Couleurs : black, red, green, yellow, blue, magenta, cyan, white,
                   brightblack ... brightwhite

        FICHIER RC : %USERPROFILE%\.slsnifrc — une option par ligne
                     (SPEED 9600, TIMESTAMP ON, DISPLAYHEX ON, COLOR green,
                      PARITY even, INTEE file.bin, ...)

        PAIRE VIRTUELLE : installez com0com (https://com0com.sourceforge.net),
        creez une paire (ex. COM10 <-> COM11), connectez l'application a COM10
        et passez --appport COM11.
        NOTE : le baudrate n'est PAS propage par la paire virtuelle —
        reglez-le avec -s pour correspondre a l'application.

        EXEMPLES :
          ComSnifer -d COM1 -a COM11 -s 115200 -x -t
          ComSnifer -d COM1 -a listen:5000 -t        # app -> TCP, device serie
          ComSnifer -d tcp:192.168.1.20:4001 -a COM5 # device distant (ser2net)
        """);
}

SnifferOptions opts;
try
{
    opts = SnifferOptions.Parse(args);
}
catch (OptionException ex)
{
    Console.Error.WriteLine($"Erreur : {ex.Message}");
    Usage();
    return 1;
}

if (opts.ShowHelp) { Usage(); return 0; }

if (opts.ListPorts)
{
    string[] ports = SerialPort.GetPortNames();
    Console.WriteLine(ports.Length == 0
        ? "Aucun port serie detecte."
        : "Ports disponibles : " + string.Join(", ", ports));
    return 0;
}

try { opts.Validate(); }
catch (OptionException ex)
{
    Console.Error.WriteLine($"Erreur : {ex.Message}");
    return 1;
}

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;   // on gere l'arret nous-memes, proprement
    cts.Cancel();
};

await using var engine = new SnifferEngine(opts);
return await engine.RunAsync(cts.Token);
