using System;

namespace IoCWithoutUnity
{
    public interface IOperationFactory
    {
        IOperation Create(string token);
    }

    public class OperationFactory : IOperationFactory
    {
        private readonly IContainer m_Container;

        public OperationFactory(IContainer container)
        {
            m_Container = container;
        }

        public IOperation Create(string token)
        {
            switch (token)
            {
                case "+":
                    return (IOperation)m_Container.Create("IOperationPlus");
                case "-":
                    return (IOperation)m_Container.Create("IOperationMinus");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}