using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorMCP
{

    public class CalculatorService : ICalculatorService
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new ArgumentException(
                    "Division by zero is not allowed.");
            }

            return a / b;
        }

        public double Power(double number, double exponent)
        {
            return Math.Pow(number, exponent);
        }

        public double SquareRoot(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "Square root of a negative number is not allowed.");
            }

            return Math.Sqrt(value);
        }

        public double Percentage(double value, double total)
        {
            if (total == 0)
            {
                throw new ArgumentException(
                    "Total cannot be zero.");
            }

            return (value / total) * 100;
        }

        public double Average(double a, double b)
        {
            return (a + b) / 2;
        }

        public double Absolute(double value)
        {
            return Math.Abs(value);
        }

        public double Modulo(double a, double b)
        {
            if (b == 0)
            {
                throw new ArgumentException(
                    "Modulo divisor cannot be zero.");
            }

            return a % b;
        }
    }
}
