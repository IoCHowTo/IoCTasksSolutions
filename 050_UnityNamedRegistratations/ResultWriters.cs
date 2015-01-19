using System;
using System.IO;
using Microsoft.Practices.Unity;

namespace UnityNamedRegistratations
{
    public interface IResultWriter
    {
        void WriteResult(int value);
    }

    public class ConsoleResultWriter : IResultWriter
    {
        public void WriteResult(int value)
        {
            Console.WriteLine(value);
        }
    }

    public class FileResultWriter : IResultWriter, IDisposable
    {
        private readonly StreamWriter _output;

        public FileResultWriter()
        {
            _output = File.CreateText("output.txt");
        }

        public void WriteResult(int value)
        {
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

    /// <summary>
    /// Purpose of this class is to use both result writers
    /// to write the result into the console as well as file
    /// </summary>
    public class ConsolidatedResultWriter : IResultWriter
    {
        [Dependency("ConsoleResultWriter")]
        public IResultWriter ConsoleResultWriter { get; set; }

        [Dependency("FileResultWriter")]
        public IResultWriter FileResultWriter { get; set; }

        public void WriteResult(int value)
        {
            ConsoleResultWriter.WriteResult(value);
            FileResultWriter.WriteResult(value);
        }
    }
}