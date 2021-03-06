﻿using System;
using System.IO;

namespace UnityChildContainers
{
    /// <summary>
    /// An interface for result writing service
    /// </summary>
    public interface IResultWriter
    {
        /// <summary>
        /// Writes the given value into results
        /// </summary>
        /// <param name="value">Value to be written into result</param>
        void WriteResult(int value);
    }

    /// <summary>
    /// A default implementation which is writting results into the console
    /// </summary>
    public class ConsoleResultWriter : IResultWriter
    {
        public void WriteResult(int value)
        {
            Console.WriteLine("Result: {0}", value);
        }
    }

    public class FileResultWriter : IResultWriter
    {
        public void WriteResult(int value)
        {
            using (var output = File.AppendText("output.txt"))
            {
                output.WriteLine(value);
            }
        }
    }

}