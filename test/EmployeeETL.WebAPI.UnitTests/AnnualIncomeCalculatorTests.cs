using EmployeeETL.ETL;
using FluentAssertions;
using Xunit;

namespace EmployeeETL.WebAPI.UnitTests;

public class AnnualIncomeCalculatorTests
{
    private AnnualIncomeCalculator _calculator;

    public AnnualIncomeCalculatorTests()
    {
        _calculator = new AnnualIncomeCalculator();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(10000, 9000)]
    [InlineData(40000, 29000)]
    public void CalculateAnnualIncomeCalculator(decimal grossAnnualSalary, decimal expectedAnnualIncome)
    {
        var computedAnnualIncome = _calculator.CalculateAnnualIncomeCalculator(grossAnnualSalary);
        computedAnnualIncome.Should().Be(expectedAnnualIncome);
    }
}