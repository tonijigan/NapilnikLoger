using System;
using System.IO;

namespace NapilnikLoger
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Message";
            Pathfinder writterInConsole = new ConsoleLogWritter();
            Pathfinder writterInFile = new FileLogWritter();
            Pathfinder writterInFileOnFridays = new SecureConsoleLogWritter();
            Pathfinder writterInConsoleOnFridays = new SecureFileLogWritter();
            Pathfinder writterInFileAndConsoleAndOnFridays = new ConsoleAndFileLogWritter();
            writterInConsole.Find(message);
            writterInFile.Find(message);
            writterInFileOnFridays.Find(message);
            writterInConsoleOnFridays.Find(message);
            writterInFileAndConsoleAndOnFridays.Find(message);
        }
    }

    public abstract class Pathfinder
    {
        public void Find(string message)
        {
            if (message != null)
                WriteError(message);
        }

        protected virtual void WriteError(string message) { }

        public void WritterConsole(string message) { Console.WriteLine(message); }

        public void WritterFile(string message) { File.WriteAllText("log.txt", message); }

    }

    class ConsoleLogWritter : Pathfinder
    {
        protected override void WriteError(string message)
        {
            WritterConsole(message);
        }
    }

    class FileLogWritter : Pathfinder
    {
        protected override void WriteError(string message)
        {
            WritterFile(message);
        }
    }

    class SecureConsoleLogWritter : Pathfinder
    {
        protected override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                WritterConsole(message);
            }
        }
    }

    class SecureFileLogWritter : Pathfinder
    {
        protected override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                WritterFile(message);
            }
        }
    }

    class ConsoleAndFileLogWritter : Pathfinder
    {
        protected override void WriteError(string message)
        {
            WritterConsole(message);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                WritterFile(message);
        }
    }
}