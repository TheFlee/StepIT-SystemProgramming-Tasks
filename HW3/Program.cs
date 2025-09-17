
using CancellationTokenSource cts = new();
var token = cts.Token;

ThreadPool.QueueUserWorkItem(x =>
{
    DownloadBar(token);
});

while (true)
{
    Console.WriteLine("Downloading... Press ESC to cancel.");
    var key = Console.ReadKey();
    if (key.Key == ConsoleKey.Escape)
    {
        cts.Cancel();
    }
}

void DownloadBar(CancellationToken token)
{
    for (int i = 0; i <= 100; i++)
    {
        if (token.IsCancellationRequested)
        {
            Console.Clear();
            Console.WriteLine("Download canceled!");
            return;
        }

        int filled = (i * 20) / 100;
        string bar = "\r[" + "".PadRight(filled, '#') + "".PadRight(20 - filled, '.') + $"] {i}%";

        Console.Write(bar);
        Thread.Sleep(100);
    }

    Console.Clear();
    Console.WriteLine("Download complate!");
}