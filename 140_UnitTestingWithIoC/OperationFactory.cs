using System;
using Microsoft.Practices.Unity;

namespace UnitTestingWithIoC
{
    public interface IOperationFactory
    {
        IOperation Create(string token);
    }

    public class OperationFactory : IOperationFactory
    {
        private readonly IUnityContainer _container;

        public OperationFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IOperation Create(string token)
        {
            switch (token)
            {
                case "+":
                    return _container.Resolve<IOperationPlus>();
                case "-":
                    return _container.Resolve<IOperationMinus>(); 
                case "*":
                    return _container.Resolve<IOperationMultiply>(); 
                case "/":
                    return _container.Resolve<IOperationDivide>();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}