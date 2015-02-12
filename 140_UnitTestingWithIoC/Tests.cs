using System;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace UnitTestingWithIoC
{
    [TestFixture]
    public class CalculatorTests
    {
        private const int Value1 = 10;

        [Test]
        public void EvaluateShouldReturnConstantIfOnlyConstantIsSpecified()
        {
            var factory = new Mock<IOperationFactory>();
            var calculator = new Calculator(factory.Object);

            var expression = new[] { Value1.ToString() };

            Assert.AreEqual(Value1, calculator.Evaluate(expression));
        }
        
        [Test]
        public void EvaluateShouldThrowTheExpectedExceptionIfOnlyOperandIsSpecified()
        {
            var factory = new Mock<IOperationFactory>();
            var calculator = new Calculator(factory.Object);

            var expression = new[] { "+" };

            Assert.Throws<Exception>(() => calculator.Evaluate(expression));
        }

        [Test]
        [Ignore("This is just an implementation simplification")]
        public void EvaluateShouldThrowTheExpectedExceptionIfTwoConstantsAreSpecified()
        {
            var factory = new Mock<IOperationFactory>();
            var calculator = new Calculator(factory.Object);

            var expression = new[] { "10", "10" };

            Assert.Throws<Exception>(() => calculator.Evaluate(expression));
        }

        [Test]
        public void EvaluateShouldThrowTheExpectedExceptionIfTwoOperandsAreSpecifiedAfterConstant()
        {
            var factory = new Mock<IOperationFactory>();
            var calculator = new Calculator(factory.Object);

            var expression = new[] { "10", "+", "+" };

            Assert.Throws<Exception>(() => calculator.Evaluate(expression));
        }

        [Test]
        public void EvaluateShouldWorkCorrectly()
        {
            const int expectedResult1 = 30;
            const int expectedResult = 90;
            const string token1 = "+";
            const string token2 = "*";

            // Here you can test the internal algorithm and expected behavior
            // up to the way that methods should be called in the expected order
            // on mocks.
            // Though - be very careful. As you can see the test setup may grow
            // very quickly which implies that future maintenance may be very difficult.
            var sequence = new MockSequence();

            var factory = new Mock<IOperationFactory>(MockBehavior.Strict);
            var operation1 = new Mock<IOperation>(MockBehavior.Strict);
            var operation2 = new Mock<IOperation>(MockBehavior.Strict);
            
            factory.InSequence(sequence).Setup(f => f.Create(token2)).Returns(operation2.Object).Verifiable();
            factory.InSequence(sequence).Setup(f => f.Create(token1)).Returns(operation1.Object).Verifiable();

            operation1.InSequence(sequence).Setup(o => o.Evaluate(10, 10)).Returns(expectedResult1).Verifiable();
            operation2.InSequence(sequence).Setup(o => o.Evaluate(expectedResult1, 30)).Returns(expectedResult).Verifiable();

            var calculator = new Calculator(factory.Object);

            var expression = new[] { "10", token1, "10", token2, "30" };
            
            Assert.AreEqual(expectedResult, calculator.Evaluate(expression));

            factory.VerifyAll();
            operation1.VerifyAll();
            operation2.VerifyAll();
        }

    }

    [TestFixture]
    public class PlusTests
    {
        [Test]
        public void EvaluateShouldWorkProperly()
        {
            var operand = new Plus();

            Assert.AreEqual(10, operand.Evaluate(5, 5));
        }
    }

    [TestFixture]
    public class MinusTests
    {
        [Test]
        public void EvaluateShouldWorkProperly()
        {
            var operand = new Minus();

            Assert.AreEqual(5, operand.Evaluate(10, 5));
        }
    }

    [TestFixture]
    public class DivideTests
    {
        [Test]
        public void EvaluateShouldWorkProperly()
        {
            var operand = new Divide();

            Assert.AreEqual(10, operand.Evaluate(20, 2));
        }
    }

    [TestFixture]
    public class MultiplyTests
    {
        [Test]
        public void EvaluateShouldWorkProperly()
        {
            var operand = new Multiply();

            Assert.AreEqual(25, operand.Evaluate(5, 5));
        }
    }

    [TestFixture]
    public class OperationFactoryTests
    {
        [TestCase(typeof(IOperationPlus), "+")]
        [TestCase(typeof(IOperationMinus), "-")]
        [TestCase(typeof(IOperationMultiply), "*")]
        [TestCase(typeof(IOperationDivide), "/")]
        public void CreateShouldWork(Type expectedType, string operand)
        {
            var unity = new UnityContainer();
            Bootstrapper.SetupContainer(unity);

            var factory = new OperationFactory(unity);

            var result = factory.Create(operand);
            Assert.That(result, Is.AssignableTo(expectedType));
        }

        [Test]
        public void CreateShouldThrowNotImplementedExceptionForUnknownOperand()
        {
            var unity = new UnityContainer();
            Bootstrapper.SetupContainer(unity);

            var factory = new OperationFactory(unity);
            Assert.Throws<NotImplementedException>(() => factory.Create("%"));
        }
    }
}
