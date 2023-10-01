using System;

namespace EmployeeETL.Jobs;

public record CsvRecord(int EmployeeID, string FirstName, string LastName, DateTime DateOfBirth, decimal GrossAnnualSalary);
