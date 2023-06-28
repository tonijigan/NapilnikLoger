using NapilnikLoger;
using System;
using System.Collections.Generic;
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
            const string message = "WOTTAKWOT";
            List<Pathfinder> pathfinders = new List<Pathfinder>();
            pathfinders.Add(new Pathfinder(new ConsoleLogWritter()));
            pathfinders.Add(new Pathfinder(new FileLogWritter()));
            pathfinders.Add(new Pathfinder(new SecureConsoleLogWritter()));
            pathfinders.Add(new Pathfinder(new SecureFileLogWritter()));
            pathfinders.Add(new Pathfinder(new ConsoleAndFileLogWritter
                           (new ConsoleLogWritter(), new FileLogWritter())));

            foreach (var pathfinder in pathfinders)
                pathfinder.WriteError(message);
        }
    }

    class Pathfinder : ILogger
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            WriteError(message);
        }

        public void WriteError(string message)
        {
            if (_logger != null)
                _logger.WriteError(message);
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
                base.WriteError(message);
        }
    }

    class SecureFileLogWritter : FileLogWritter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                base.WriteError(message);
        }
    }
}

class ConsoleAndFileLogWritter : ILogger
{
    private List<ILogger> _loggers = new List<ILogger>();

    public ConsoleAndFileLogWritter(ILogger consoleWritter, ILogger fileWritter)
    {
        _loggers.Add(consoleWritter);
        _loggers.Add(fileWritter);
    }

    public void WriteError(string message)
    {
        WriterMessage(message);

        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            WriterMessage(message);
    }

    private void WriterMessage(string message)
    {
        if (_loggers == null)
            return;

        foreach (ILogger logger in _loggers)
            logger.WriteError(message);
    }
}