using System;
using System.Threading.Tasks;
using EmployeeETL.ETL;

namespace EmployeeETL.Data;

public class EmployeeLoader : IEmployeeLoader
{
    private readonly EmployeeHRContext _context;

    public EmployeeLoader(EmployeeHRContext context)
    {
        _context = context;
    }

    public async Task LoadAsync(EmployeeRecord employee)
    {
        await _context.Employee.AddAsync(employee);
        await _context.SaveChangesAsync();
    }
}
