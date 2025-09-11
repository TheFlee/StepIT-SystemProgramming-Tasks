using System;
using System.Diagnostics;

var blackList = new List<string>();

while (true)
{
    Console.Clear();
    Console.WriteLine("======= Task Manager =======");
    Console.WriteLine("1. All Processes");
    Console.WriteLine("2. Start process by Name");
    Console.WriteLine("3. Kill process by Id");
    Console.WriteLine("4. Kill processes by Name");
    Console.WriteLine("5. Add Black list");
    Console.WriteLine("6. Remove from Black list");
    Console.WriteLine("7. Exit");

    Console.Write("Select option: ");
    string choice = Console.ReadLine()!;
    Console.Clear();

    switch (choice)
    {
        case "1":
            ListAllProcesses();
            break;
        case "2":
            StartProcessByName();
            break;
        case "3":
            KillProcessById();
            break;
        case "4":
            KillProcessByName();
            break;
        case "5":
            AddToBlacklist();
            break;
        case "6":
            RemoveFromBlacklist();
            break;
        case "7":
            return;
        default:
            Console.WriteLine("Invalid option!");
            break;
    }
    KillBlacklisted();
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();

}

void ListAllProcesses()
{
    var processes = Process.GetProcesses();
    foreach (var process in processes)
    {
        Console.WriteLine($"{process.Id}. {process.ProcessName} -> Threads: {process.Threads.Count}");
    }
}

void StartProcessByName()
{
    Console.Write("Enter process name: ");
    string procName = Console.ReadLine()!;
    try
    {
        Process.Start(procName);
    }
    catch (Exception)
    {
        Console.WriteLine("Can not start the proccess!");
    }
}

void KillProcessById()
{
    Console.Write("Enter process Id: ");
    int.TryParse(Console.ReadLine(), out int procId);
    try
    {
        var process = Process.GetProcessById(procId);
        process.Kill();
        Console.WriteLine("Process killed!");
    }
    catch (Exception)
    {
        Console.WriteLine("Can not kill the proccess!");
    }
}

void KillProcessByName()
{
    Console.Write("Enter process Name: ");
    var procName = Console.ReadLine()!;
    try
    {
        var processes = Process.GetProcessesByName(procName);
        foreach (var process in processes)
        {
            process.Kill();
            Console.WriteLine($"Killed {process.Id}. {process.ProcessName}");
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Can not kill the proccesses!");
    }
}

void AddToBlacklist()
{
    Console.Write("Enter process name to blacklist: ");
    string procName = Console.ReadLine()!;
    if (!blackList.Contains(procName))
    {
        blackList.Add(procName);
        Console.WriteLine($"{procName} added to blacklist!");
    }
    else
    {
        Console.WriteLine($"{procName} already in blacklist!");
    }
}

void RemoveFromBlacklist()
{
    Console.Write("Enter process name to remove: ");
    string procName = Console.ReadLine()!;
    if (blackList.Contains(procName))
    {
        blackList.Remove(procName);
        Console.WriteLine($"{procName} removed from blacklist!");
    }
    else
    {
        Console.WriteLine($"There is no proccess called {procName} in blacklist!");
    }
}

void KillBlacklisted()
{
    var processes = Process.GetProcesses();
    foreach (var proc in processes)
    {
        if (blackList.Contains(proc.ProcessName))
        {
            proc.Kill();
        }
    }
}