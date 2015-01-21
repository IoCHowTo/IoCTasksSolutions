using Microsoft.Practices.Unity;

namespace UnityResolutionOverrides
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new[] { "10", "+", "20", "+", "30" };
            }

            using (var container = new UnityContainer())
            {
                Bootstrapper.SetupContainer(container);

                var calculator = container.Resolve<ICalculator>();
                int result = calculator.Evaluate(args);

                // This is one option for fix - please note that the scope of the ParameterOverride is not limited
                // and thus may apply to resolutions of dependencies
                var resultWriter = container.Resolve<IConsoleAndFileResultWriter>(
                    new ParameterOverride("fileName", "output.txt"),
                    new ParameterOverride("context", "Context data")
                    );

                // Another option is to limit the scope to the type being built. The downside of this solution is that you are making
                // code tightly coupled with concrete implementation thus be very careful and do not spread this into multiple places
                //var resultWriter = container.Resolve<IConsoleAndFileResultWriter>(
                //    new ParameterOverride("fileName", "output.txt").OnType<ConsoleAndFileResultWriter>(),
                //    new ParameterOverride("context", "Context data").OnType<ConsoleAndFileResultWriter>()
                //    );

                // Another option is to override a type and to limit the scope to the type being built. 
                // The downside of this solution is that you are making code tightly coupled with concrete 
                // implementation thus be very careful and do not spread this into multiple places
                // Also keep in mind that this cannot be used if multiple parameters of the same type can be used (see the ConsoleAndFileResultWriter
                // constructor and also the written file)
                //var resultWriter = container.Resolve<IConsoleAndFileResultWriter>(new DependencyOverride(typeof(string), "output.txt")
                //        .OnType<ConsoleAndFileResultWriter>());

                resultWriter.WriteResult(result);
            }
        }
    }
}
