using System;

namespace IoCWithoutUnity
{
    public interface IOperationFactory
    {
        IOperation Create(string token);
    }

    public class OperationFactory : IOperationFactory
    {
        private readonly IContainer _container;

        public OperationFactory(IContainer container)
        {
            _container = container;
        }

        public IOperation Create(string token)
        {
            switch (token)
            {
                case "+":
                    return (IOperation)_container.Create("IOperationPlus");
                case "-":
                    return (IOperation)_container.Create("IOperationMinus");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
