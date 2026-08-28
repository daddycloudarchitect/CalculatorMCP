using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorMCP
{
  

    public interface ICalculatorService
    {
        double Add(double a, double b);

        double Subtract(double a, double b);

        double Multiply(double a, double b);

        double Divide(double a, double b);

        double Power(double number, double exponent);

        double SquareRoot(double value);

        double Percentage(double value, double total);

        double Average(double a, double b);

        double Absolute(double value);

        double Modulo(double a, double b);
    }
}

