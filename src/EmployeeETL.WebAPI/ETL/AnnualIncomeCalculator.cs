using System.Collections.Generic;
using System.Linq;

namespace EmployeeETL.ETL;

public class AnnualIncomeCalculator
{

    public decimal CalculateAnnualIncomeCalculator(decimal grossAnnualSalary)
    {
        if (grossAnnualSalary == 0)
            return 0;

        decimal salaryInBand = grossAnnualSalary;
        decimal annualTaxPaid = 0;

        var limits = new Dictionary<decimal, int>
        {
            { 20000, 40 },
            { 5000, 20 }
        };

        limits.ToList().ForEach(limit =>
        {
            if (salaryInBand > limit.Key)
            {
                annualTaxPaid += (salaryInBand - limit.Key) * limit.Value / 100;
                salaryInBand -= salaryInBand - limit.Key;
            }
        });

        return grossAnnualSalary - annualTaxPaid;
    }
}