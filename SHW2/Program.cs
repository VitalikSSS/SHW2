using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("1 - Перехоплення виводу з cmd");
        Console.WriteLine("2 - Запуск ping");
        Console.WriteLine("3 - Список процесів");
        Console.WriteLine("4 - Завершення notepad");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "4":
                CaptureCmdOutput();
                break;
            case "5":
                RunPing();
                break;
            case "6":
                ListProcesses();
                break;
            case "7":
                KillNotepad();
                break;
            default:
                Console.WriteLine("Невірний вибір");
                break;
        }

        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }

    static void CaptureCmdOutput()
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/C dir";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine("Вивід команди dir:");
        Console.WriteLine(output);
    }

    static void RunPing()
    {
        Process process = new Process();
        process.StartInfo.FileName = "ping";
        process.StartInfo.Arguments = "127.0.0.1 -n 2";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine("Результат ping:");
        Console.WriteLine(output);
    }

    static void ListProcesses()
    {
        Console.WriteLine("Активні процеси з ID >= 1000:");
        Console.WriteLine("ID\tНазва");
        Console.WriteLine("-------------------");

        foreach (Process process in Process.GetProcesses())
        {
            try
            {
                if (process.Id >= 1000)
                {
                    Console.WriteLine($"{process.Id}\t{process.ProcessName}");
                }
            }
            catch(Exception)
            {
               Console.WriteLine($"Не вдалося отримати інформацію про процес з ID {process.Id}");
            }
        }
    }

    static void KillNotepad()
    {
        Process[] notepadProcesses = Process.GetProcessesByName("notepad");

        Console.WriteLine($"Знайдено {notepadProcesses.Length} процесів notepad");

        foreach (Process process in notepadProcesses)
        {
            try
            {
                Console.WriteLine($"Завершую процес: {process.ProcessName} (ID: {process.Id})");
                process.Kill();
                process.WaitForExit();
                Console.WriteLine($"Процес {process.Id} успішно завершено");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завершенні процесу {process.Id}: {ex.Message}");
            }
        }

        if (notepadProcesses.Length == 0)
        {
            Console.WriteLine("Процеси notepad не знайдено");
        }
    }
}
