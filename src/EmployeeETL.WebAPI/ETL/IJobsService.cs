using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeETL.ETL;

public interface IJobsService {
    Task<IEnumerable<EtlJob>> GetAllJobsAsync();
    Task<EtlJob?> GetJobAsync(Guid id);
    Task<EtlJob> CreateNewCSVJobAsync(string csvFilePath);
    Task ProcessJobAsync(EtlJob job, CancellationToken token);
}
