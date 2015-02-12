using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnitTestingWithoutIoC
{
    [TestFixture]
    public class CalculatorTests
    {
        private const int Value1 = 10;

        [Test]
        public void EvaluateShouldReturnConstantIfOnlyConstantIsSpecified()
        {
            var calculator = new Calculator();
            var expression = new[] { Value1.ToString() };

            Assert.AreEqual(Value1, calculator.Evaluate(expression));
        }
        
        [Test]
        public void EvaluateShouldThrowTheExpectedExceptionIfOnlyOperandIsSpecified()
        {
            var calculator = new Calculator();
            var expression = new[] { "+" };

            Assert.Throws<Exception>(() => calculator.Evaluate(expression));
        }

        [Test]
        [Ignore("This is just an implementation simplification")]
        public void EvaluateShouldThrowTheExpectedExceptionIfTwoConstantsAreSpecified()
        {
            var calculator = new Calculator();
            var expression = new[] { "10", "10" };

            Assert.Throws<Exception>(() => calculator.Evaluate(expression));
        }

        [Test]
        public void EvaluateShouldThrowTheExpectedExceptionIfTwoOperandsAreSpecifiedAfterConstant()
        {
            var calculator = new Calculator();
            var expression = new[] { "10", "+", "+" };

            Assert.Throws<Exception>(() => calculator.Evaluate(expression));
        }

        [Test]
        public void EvaluateShouldWorkCorrectlyForPlus()
        {
            var calculator = new Calculator();
            var expression = new[] { "10", "+", "10" };
            const int expectedResult = 20;

            Assert.AreEqual(expectedResult, calculator.Evaluate(expression));
        }

        // Tests below are necessary for every added operand support
        // and as you see it duplicates some other tests defined
        // elsewhere

        [TestCaseSource("DataSource")]
        public void EvaluateShouldWork(int expectedResult, IEnumerable<string> expression)
        {
            var calculator = new Calculator();

            Assert.AreEqual(expectedResult, calculator.Evaluate(expression));
        }

        public IEnumerable DataSource()
        {
            yield return new [] {10, (object)new[] {"20", "-", "10"}};
            yield return new [] {30, (object)new[] {"20", "+", "10"}};
            yield return new [] {200, (object)new[] {"20", "*", "10"}};
            yield return new [] {2, (object)new[] {"20", "/", "10"}};
            yield return new [] {15, (object)new[] {"20", "-", "10",  "+", "5"}};
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
        [TestCase("+")]
        [TestCase("-")]
        [TestCase("*")]
        [TestCase("/")]
        public void CreateShouldWork(string operand)
        {
            var factory = new OperationFactory();

            Assert.DoesNotThrow(() => factory.Create(operand));
        }

        [Test]
        public void CreateShouldThrowNotImplementedExceptionForUnknownOperand()
        {
            var factory = new OperationFactory();

            Assert.Throws<NotImplementedException>(() => factory.Create("%"));
        }
    }
}
