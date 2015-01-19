using Microsoft.Practices.Unity;

namespace UnityInjectionTypes
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Setup method for the unity container
        /// </summary>
        public static void SetupContainer(IUnityContainer unityContainer)
        {
            unityContainer
                .RegisterType<ICalculator, Calculator>()
                .RegisterType<IOperationPlus, Plus>()
                .RegisterType<IOperationMinus, Minus>()
                .RegisterType<IOperationDiv, Div>()
                .RegisterType<IOperationFactory, OperationFactory>(new InjectionFactory(CreateFactory))
                .RegisterType<IOperationFactory, OperationFactory>()
                .RegisterType<IResultWriter, ConsoleResultWriter>();
        }

        private static IOperationFactory CreateFactory(IUnityContainer arg)
        {
            var plus = arg.Resolve<IOperationPlus>();
            var minus = arg.Resolve<IOperationMinus>();
            var div = arg.Resolve<IOperationDiv>();

            return new OperationFactory(minus, plus, div);
        }
    }

}