using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeETL.ETL;
using Microsoft.EntityFrameworkCore;

namespace EmployeeETL.Data;

public class JobsRepository : IJobsRepository
{
    private readonly EmployeeETLContext _context;

    public JobsRepository(EmployeeETLContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EtlJob>> GetAllJobsAsync()
    {
        return await _context.EtlJobs
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EtlJob?> FindJobByIdAsync(Guid id)
    {
        return await _context.EtlJobs.FindAsync(id);
    }

    public async Task CreateAsync(EtlJob etlJob)
    {
        await _context.EtlJobs.AddAsync(etlJob);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EtlJob etlJob)
    {
        _context.Update(etlJob);
        await _context.SaveChangesAsync();
    }
}
