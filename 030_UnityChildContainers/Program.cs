using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace UnityChildContainers
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new[] { "10", "+", "20", "+", "30", "-", "12" };
            }

            using (var container = new UnityContainer())
            {
                Bootstrapper.SetupContainer(container);

                try
                {
                    PerformCalculation(args, container);
                }
                catch (Exception exception)
                {
                    // Since the Minus class is registered only in the child container
                    // the default expression cannot be evaluated on the root container
                    Console.WriteLine(exception);
                }

                using (var childContainer = container.CreateChildContainer())
                {
                    Bootstrapper.SetupChildContainer(childContainer);

                    PerformCalculation(args, childContainer);
                }
            }
        }

        private static void PerformCalculation(IEnumerable<string> args, IUnityContainer container)
        {
            var calculator = container.Resolve<ICalculator>();
            int result = calculator.Evaluate(args);

            var resultWriter = container.Resolve<IResultWriter>();
            resultWriter.WriteResult(result);
        }
    }
}
