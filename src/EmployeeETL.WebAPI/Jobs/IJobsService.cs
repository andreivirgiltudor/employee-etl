using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeETL.Jobs;

public interface IJobsService {
    Task<IEnumerable<EtlJob>> GetAllJobsAsync();
    Task<EtlJob?> GetJobAsync(Guid id);
    Task<EtlJob> CreateNewCSVJobAsync(string csvFilePath);
    Task ProcessJob(EtlJob job);
}
