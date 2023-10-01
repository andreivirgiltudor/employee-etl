using EmployeeETL.ETL;

namespace EmployeeETL.DTOs;

public static class EtlJobExtensions {
    public static EtlJobDto ToDto(this EtlJob job) {
        var id = job.Id;
        var status = job.Status.ToString();
        return new EtlJobDto(id, status);
    }
}