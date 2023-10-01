using System;
using System.Threading.Tasks;
using EmployeeETL.Data;
using EmployeeETL.Jobs;

namespace EmployeeETL.WebAPI.IntegrationTests.setup;

public class DataSeeder
{
    private EmployeeETLContext _context;

    public DataSeeder(EmployeeETLContext context)
    {
        _context = context;
    }

    public async Task RemoveAllJobs()
    {
        _context.EtlJobs.RemoveRange(_context.EtlJobs);
        await _context.SaveChangesAsync();
    }

    public async Task<Guid> CreateSampleJob()
    {
        var etlJob = new EtlJob(Guid.NewGuid(), JobStatus.New, "1", DateTime.Now);
        await _context.EtlJobs.AddAsync(etlJob);
        await _context.SaveChangesAsync();
        return etlJob.Id;
    }
}