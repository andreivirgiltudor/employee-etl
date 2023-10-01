using System;

namespace EmployeeETL.Jobs;

public class EtlJob
{
    public Guid Id { get; private set; }
    public JobStatus Status { get; private set; }
    public string FilePath { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public EtlJob(Guid id, JobStatus status, string filePath, DateTime createdAt)
    {
        Id = id;
        Status = status;
        FilePath = filePath;
        CreatedAt = createdAt;
    }

    public void StartedProcessing()
    {
        Status = JobStatus.Processing;
    }

    public void Processed() {
        Status = JobStatus.Completed;
    }

    internal void FailedProcessing()
    {
        Status = JobStatus.Failed;
    }
};
