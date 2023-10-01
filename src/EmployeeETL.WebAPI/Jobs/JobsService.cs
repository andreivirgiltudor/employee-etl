using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace EmployeeETL.Jobs;

public class JobsService : IJobsService
{
    private readonly IJobsRepository _repository;

    public JobsService(IJobsRepository repository)
    {
        _repository = repository;
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

    public async Task ProcessJob(EtlJob job)
    {
        try
        {
            job.StartedProcessing();
            await _repository.UpdateAsync(job);
            using var csvFileStream = new StreamReader(job.FilePath);
            using var csvReader = new CsvReader(csvFileStream, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<CsvReader>();
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

