using System;
using System.IO;

namespace UnityResolutionOverrides
{
    public interface IConsoleResultWriter
    {
        void WriteResult(int value);
    }

    public interface IConsoleAndFileResultWriter
    {
        void WriteResult(int value);
    }

    public class ConsoleResultWriters : IConsoleResultWriter
    {
        public void WriteResult(int value)
        {
            Console.WriteLine(value);
        }
    }

    public class ConsoleAndFileResultWriter : IConsoleAndFileResultWriter, IDisposable
    {
        private readonly string _context;
        private readonly IConsoleResultWriter _consoleResultWriter;
        private readonly StreamWriter _output;

        public ConsoleAndFileResultWriter(string fileName, string context, IConsoleResultWriter consoleResultWriter)
        {
            _context = context;
            _consoleResultWriter = consoleResultWriter;
            _output = File.CreateText(fileName);
        }

        public void WriteResult(int value)
        {
            _consoleResultWriter.WriteResult(value);
            _output.WriteLine(_context + ":" + value);
        }

        public void Dispose()
        {
            if (_output != null)
            {
                _output.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }

}