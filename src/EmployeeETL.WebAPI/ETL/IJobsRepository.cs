using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeETL.ETL;

public interface IJobsRepository
{
    Task<IEnumerable<EtlJob>> GetAllJobsAsync();
    Task<EtlJob?> FindJobByIdAsync(Guid id);
    Task CreateAsync(EtlJob etlJob);
    Task UpdateAsync(EtlJob etlJob);
}

