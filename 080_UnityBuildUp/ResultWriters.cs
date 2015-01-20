using System;
using System.IO;
using Microsoft.Practices.Unity;

namespace UnityBuildUp
{
    public interface IConsoleResultWriter
    {
        void WriteResult(int value);
    }
    
    /// <summary>
    /// </summary>
    public class ResultWriters : IConsoleResultWriter
    {
        public void WriteResult(int value)
        {
            Console.WriteLine(value);
        }
    }

    public class ConsoleAndFileResultWriter : IDisposable
    {
        private IConsoleResultWriter _consoleResultWriter;
        private readonly StreamWriter _output;

        public ConsoleAndFileResultWriter(string fileName)
        {
            _output = File.CreateText(fileName);
        }

        [Dependency]
        public IConsoleResultWriter ConsoleResultWriter
        {
            get { return _consoleResultWriter; }
            set { _consoleResultWriter = value; }
        }


        public void WriteResult(int value)
        {
            _consoleResultWriter.WriteResult(value);
            _output.WriteLine(value);
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