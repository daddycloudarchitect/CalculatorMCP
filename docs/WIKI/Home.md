# Disclaimer

This repository is a Proof of Concept (POC) created for educational, demonstration, and learning purposes only.

The solution demonstrates how Azure Functions can be exposed as a Model Context Protocol (MCP) Server and consumed by Azure AI Foundry Agents. It is intended to showcase implementation concepts, development patterns, and integration approaches.

## Not Production Ready

This project is not intended for production use 

## Security Notice

No proprietary business logic, customer information, confidential data, or company intellectual property is included in this repository. Any names, values, configurations, or examples are provided solely for demonstration purposes.

Users are responsible for performing their own security, compliance, performance, and architecture reviews before using any part of this code in production environments.

## Warranty

This code is provided "AS IS", without warranty of any kind, express or implied. The author assumes no responsibility for any damages, losses, or issues arising from the use of this software.

## Microsoft Disclaimer

This project is an independent community sample and is not affiliated with, endorsed by, or supported by Microsoft.

# Project summary and file explanations

This repository hosts the CalculatorMCP function app (targeting .NET 10). It provides a small, well-scoped calculator service implemented as: an interface, a concrete service, and a set of MCP tool functions that expose calculator operations via the MCP Function extension.

Files (brief explanation)

**- CalculatorMCP/ICalculatorService.cs**
  - Defines the ICalculatorService interface: method signatures for basic arithmetic and helper operations (Add, Subtract, Multiply, Divide, Power, SquareRoot, Percentage, Average, Absolute, Modulo).

**- CalculatorMCP/CalculatorService.cs**
  - Concrete implementation of ICalculatorService. Each method performs the expected calculation and validates inputs (for example: checks for division/modulo by zero and non-negative input for square root). Uses System.Math where appropriate.

**- CalculatorMCP/CalculatorTools.cs**
  - Contains the CalculatorTool class which exposes the calculator operations as Azure Functions using the McpToolTrigger attribute (Microsoft.Azure.Functions.Worker.Extensions.Mcp).
  - Each public method is decorated with [Function("Name")] and [McpToolTrigger("tool_name", "description")] so the MCP host can surface these functions as tools.
  - Methods accept a ToolInvocationContext, log the incoming arguments (JSON), deserialize them into request DTOs, validate where needed, and call ICalculatorService to compute the result.
  - Request DTOs in this file:
	- SingleNumberRequest { double Value }
	- TwoNumberRequest { double A; double B }
	- PowerRequest { double Number; double Exponent }
	- PercentageRequest { double Value; double Total }

**- CalculatorMCP/Program.cs**
  - Function host startup. Builds the Functions application, configures OpenTelemetry/Azure Monitor exporter when an Application Insights connection string is present, and registers the DI mapping: ICalculatorService -> CalculatorService.

**MCP Server Tools summary**

The MCP toolset in this project maps simple calculator operations to named tools that can be invoked by an MCP-capable host. Available tool names (as declared in the McpToolTrigger attribute) and their inputs:

- add: expects TwoNumberRequest (A, B)
- subtract: TwoNumberRequest
- multiply: TwoNumberRequest
- divide: TwoNumberRequest (B must not be zero)
- power: PowerRequest (Number, Exponent)
- square_root: SingleNumberRequest (Value must be >= 0)
- percentage: PercentageRequest (Total must not be zero)
- average: TwoNumberRequest
- absolute: SingleNumberRequest
- modulo: TwoNumberRequest (B must not be zero)

Each tool method:
- Logs the raw argument JSON for debugging
- Deserializes the arguments into a typed request
- Validates inputs (where applicable)
- Forwards the operation to CalculatorService and returns a double result

_**How to run locally**_

1. Restore and build: dotnet restore && dotnet build
2. Start the Functions host from the project root (or via Visual Studio): dotnet run
3. When running, the MCP extension will register the named tools so an MCP client can discover/invoke them.

Note

- The McpToolTrigger attribute requires the MCP Functions extension; ensure the extension package is present in project dependencies.







