using CalculatorMCP;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CalculatorMCP.Tools;

public class CalculatorTool
{
    private readonly ILogger<CalculatorTool> _logger;
    private readonly ICalculatorService _calculatorService;

    public CalculatorTool(
        ILogger<CalculatorTool> logger,
        ICalculatorService calculatorService)
    {
        _logger = logger;
        _calculatorService = calculatorService;
    }

    [Function(nameof(Add))]
    public double Add(
        [McpToolTrigger(
            "add",
            "Adds two numbers together")]
        ToolInvocationContext context)
    {
        _logger.LogInformation(
       "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<TwoNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");
        return _calculatorService.Add(
            request.A,
            request.B);
    }

    [Function(nameof(Subtract))]
    public double Subtract(
        [McpToolTrigger(
            "subtract",
            "Subtracts the second number from the first")]
        ToolInvocationContext context)
    {
        _logger.LogInformation(
       "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<TwoNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.Subtract(
            request.A,
            request.B);
    }

    [Function(nameof(Multiply))]
    public double Multiply(
    [McpToolTrigger(
        "multiply",
        "Multiplies two numbers")]
    ToolInvocationContext context)
    {

        _logger.LogInformation(
        "Arguments JSON: {Args}",JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<TwoNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.Multiply(
            request.A,
            request.B);
    }


    [Function(nameof(Divide))]
    public double Divide(
        [McpToolTrigger(
            "divide",
            "Divides one number by another")]
        ToolInvocationContext context)
    {

        _logger.LogInformation(
        "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<TwoNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        if (request.B == 0)
        {
            throw new ArgumentException(
                "Division by zero is not allowed.");
        }

        return _calculatorService.Divide(
            request.A,
            request.B);
    }

    [Function(nameof(Power))]
    public double Power(
        [McpToolTrigger(
            "power",
            "Raises a number to a specified exponent")]
        ToolInvocationContext context)
    {
        _logger.LogInformation(
       "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<PowerRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.Power(
            request.Number,
            request.Exponent);
    }

    [Function(nameof(SquareRoot))]
    public double SquareRoot(
        [McpToolTrigger(
            "square_root",
            "Calculates the square root of a number")]
        ToolInvocationContext context)
    {
        _logger.LogInformation(
      "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<SingleNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.SquareRoot(
            request.Value);
    }

    [Function(nameof(Percentage))]
    public double Percentage(
        [McpToolTrigger(
            "percentage",
            "Calculates what percentage a value is of a total")]
        ToolInvocationContext context)
    {
        _logger.LogInformation(
      "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<PercentageRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        if (request.Total == 0)
        {
            throw new ArgumentException(
                "Total cannot be zero.");
        }

        return _calculatorService.Percentage(
            request.Value,
            request.Total);
    }

    [Function(nameof(Average))]
    public double Average(
        [McpToolTrigger(
            "average",
            "Calculates the average of two numbers")]
        ToolInvocationContext context)
    {

        _logger.LogInformation(
        "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<TwoNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.Average(
            request.A,
            request.B);
    }

    [Function(nameof(Absolute))]
    public double Absolute(
        [McpToolTrigger(
            "absolute",
            "Returns the absolute value of a number")]
        ToolInvocationContext context)
    {
        _logger.LogInformation(
      "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<SingleNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.Absolute(
            request.Value);
    }

    [Function(nameof(Modulo))]
    public double Modulo(
     [McpToolTrigger(
        "modulo",
        "Returns the remainder after division")]
    ToolInvocationContext context)
    {
        _logger.LogInformation(
      "Arguments JSON: {Args}", JsonSerializer.Serialize(context.Arguments));

        var json = JsonSerializer.Serialize(context.Arguments);

        var request =
            JsonSerializer.Deserialize<TwoNumberRequest>(json)
            ?? throw new ArgumentException("Invalid request.");

        return _calculatorService.Modulo(
            request.A,
            request.B);
    }

}

public class SingleNumberRequest
{
    public double Value { get; set; }
}


public class TwoNumberRequest
{
    public double A { get; set; }

    public double B { get; set; }
}


public class PowerRequest
{
    public double Number { get; set; }

    public double Exponent { get; set; }
}

public class PercentageRequest
{
    public double Value { get; set; }

    public double Total { get; set; }
}