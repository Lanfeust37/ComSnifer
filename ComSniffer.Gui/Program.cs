namespace ComSniffer.Gui;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
#pragma warning disable SYSLIB5002  // SetColorMode : API experimentale (.NET 9/10)
        Application.SetColorMode(SystemColorMode.System);
#pragma warning restore SYSLIB5002
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
