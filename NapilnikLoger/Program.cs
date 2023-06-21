using System;
using System.IO;

namespace NapilnikLoger
{
    interface ILogger
    {
        void WriteError(string message);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new FileLogWritter());
            pathfinder.WriteError("s");
        }
    }

    class ConsoleLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureConsoleLogWritter : ConsoleLogWritter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class Pathfinder : ILogger
    {
        protected ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }
        public void WriteError(string message)
        {

        }
    }
}