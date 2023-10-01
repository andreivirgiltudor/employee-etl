using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;

namespace EmployeeETL.ETL;

public class JobsService : IJobsService
{
    private readonly IJobsRepository _repository;
    private readonly TransformService _transformer;
    private readonly IEmployeeLoader _loader;

    public JobsService(IJobsRepository repository, TransformService transformService, IEmployeeLoader employeeLoader)
    {
        _repository = repository;
        _transformer = transformService;
        _loader = employeeLoader;
    }

    public async Task<IEnumerable<EtlJob>> GetAllJobsAsync()
    {
        return await _repository.GetAllJobsAsync();
    }

    public async Task<EtlJob?> GetJobAsync(Guid id)
    {
        return await _repository.FindJobByIdAsync(id);
    }

    public async Task<EtlJob> CreateNewCSVJobAsync(string csvFilePath)
    {
        var id = Guid.NewGuid();
        var status = JobStatus.New;
        var createdAt = DateTime.Now;
        var etlJob = new EtlJob(id, status, csvFilePath, createdAt);

        await _repository.CreateAsync(etlJob);
        return etlJob;
    }

    public async Task ProcessJobAsync(EtlJob job, CancellationToken token)
    {
        try
        {
            job.StartedProcessing();
            await _repository.UpdateAsync(job);
            using var csvFileStream = new StreamReader(job.FilePath);
            using var csvReader = new CsvReader(csvFileStream, CultureInfo.InvariantCulture);
            // Use cancelation token to cancel processing for records
            // when cancel signal recieved.
            foreach (var record in csvReader.GetRecords<CsvRecord>())
            {
                var employee = _transformer.Map(record);
                await _loader.LoadAsync(employee);
            }
            job.Processed();
            await _repository.UpdateAsync(job);
        }
        catch (Exception)
        {
            job.FailedProcessing();
            await _repository.UpdateAsync(job);
            throw;
        }
    }
}
