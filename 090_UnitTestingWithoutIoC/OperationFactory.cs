using System;

namespace UnitTestingWithoutIoC
{
    public class OperationFactory
    {
        public IOperation Create(string token)
        {
            switch (token)
            {
                case "+":
                    return new Plus();
                case "-":
                    return new Minus();
                case "*":
                    return new Multiply();
                case "/":
                    return new Divide();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}