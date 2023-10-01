using System;

namespace EmployeeETL.ETL;

public record CsvRecord(int EmployeeID, string FirstName, string LastName, DateTime DateOfBirth, decimal GrossAnnualSalary);
