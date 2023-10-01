using System;

namespace EmployeeETL.ETL;

public class EmployeeRecord
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public decimal AnnualIncome { get; set; }
}