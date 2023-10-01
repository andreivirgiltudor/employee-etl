namespace EmployeeETL.ETL;

public class TransformService
{
    private readonly AnnualIncomeCalculator _annualIncomeCalculator;

    public TransformService(AnnualIncomeCalculator annualIncomeCalculator)
    {
        _annualIncomeCalculator = annualIncomeCalculator;
    }

    public EmployeeRecord Map(CsvRecord csvRecord)
    {
        var record = new EmployeeRecord
        {
            EmployeeID = csvRecord.EmployeeID,
            FirstName = csvRecord.FirstName,
            LastName = csvRecord.LastName,
            BirthDate = csvRecord.DateOfBirth,
            AnnualIncome = _annualIncomeCalculator.CalculateAnnualIncomeCalculator(csvRecord.GrossAnnualSalary)
        };

        return record;
    }
}
