/**
 * @fileoverview Unit tests for the Calculator Demo application
 * @module CalculatorDemo.Tests
 */

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Windows;
using Xunit;

namespace CalculatorDemo.Tests
{
    /// <summary>
    /// Unit tests for calculator functionality
    /// </summary>
    public class CalculatorTests
    {
        private readonly ILogger<MainWindow> _logger;

        /// <summary>
        /// Initializes a new instance of the CalculatorTests class
        /// </summary>
        public CalculatorTests()
        {
            _logger = NullLogger<MainWindow>.Instance;
        }

        /// <summary>
        /// Tests that the calculator can be instantiated
        /// </summary>
        [Fact]
        public void Calculator_CanBeInstantiated()
        {
            // Arrange & Act
            var calculator = new CalculatorLogic();

            // Assert
            Assert.NotNull(calculator);
        }

        /// <summary>
        /// Tests basic arithmetic operations
        /// </summary>
        [Theory]
        [InlineData(2, 3, "+", 5)]
        [InlineData(5, 4, "-", 1)]
        [InlineData(3, 5, "*", 15)]
        [InlineData(10, 5, "/", 2)]
        public void BasicArithmeticOperations_ShouldCalculateCorrectly(double a, double b, string operation, double expected)
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act
            double result = calculator.PerformOperation(a, b, operation);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests division by zero handling
        /// </summary>
        [Fact]
        public void DivisionByZero_ShouldThrowException()
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculator.PerformOperation(5, 0, "/"));
        }

        /// <summary>
        /// Tests advanced mathematical functions
        /// </summary>
        [Theory]
        [InlineData(5, 25)] // Square
        [InlineData(4, 16)]
        public void Square_ShouldCalculateCorrectly(double input, double expected)
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act
            double result = calculator.Square(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests square root calculation
        /// </summary>
        [Theory]
        [InlineData(16, 4)]
        [InlineData(25, 5)]
        public void SquareRoot_ShouldCalculateCorrectly(double input, double expected)
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act
            double result = calculator.SquareRoot(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests percentage calculation
        /// </summary>
        [Theory]
        [InlineData(50, 0.5)]
        [InlineData(25, 0.25)]
        public void Percentage_ShouldCalculateCorrectly(double input, double expected)
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act
            double result = calculator.Percentage(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests inverse calculation
        /// </summary>
        [Theory]
        [InlineData(2, 0.5)]
        [InlineData(4, 0.25)]
        public void Inverse_ShouldCalculateCorrectly(double input, double expected)
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act
            double result = calculator.Inverse(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests invalid operation handling
        /// </summary>
        [Fact]
        public void InvalidOperation_ShouldThrowException()
        {
            // Arrange
            var calculator = new CalculatorLogic();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.PerformOperation(1, 2, "invalid"));
        }
    }

    /// <summary>
    /// Simple calculator logic class for testing
    /// </summary>
    public class CalculatorLogic
    {
        /// <summary>
        /// Performs a mathematical operation on two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <param name="operation">Operation to perform</param>
        /// <returns>Result of the operation</returns>
        public double PerformOperation(double a, double b, string operation)
        {
            return operation switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b == 0 ? throw new DivideByZeroException("Cannot divide by zero") : a / b,
                _ => throw new ArgumentException($"Unknown operation: {operation}")
            };
        }

        /// <summary>
        /// Calculates the square of a number
        /// </summary>
        /// <param name="number">The number to square</param>
        /// <returns>The square of the number</returns>
        public double Square(double number)
        {
            return number * number;
        }

        /// <summary>
        /// Calculates the square root of a number
        /// </summary>
        /// <param name="number">The number to find the square root of</param>
        /// <returns>The square root of the number</returns>
        public double SquareRoot(double number)
        {
            if (number < 0)
                throw new ArgumentException("Cannot calculate square root of negative number");
            return Math.Sqrt(number);
        }

        /// <summary>
        /// Calculates the percentage of a number
        /// </summary>
        /// <param name="number">The number to convert to percentage</param>
        /// <returns>The percentage value (number / 100)</returns>
        public double Percentage(double number)
        {
            return number / 100.0;
        }

        /// <summary>
        /// Calculates the inverse of a number (1/x)
        /// </summary>
        /// <param name="number">The number to find the inverse of</param>
        /// <returns>The inverse of the number</returns>
        public double Inverse(double number)
        {
            if (number == 0)
                throw new DivideByZeroException("Cannot calculate inverse of zero");
            return 1.0 / number;
        }
    }
} 