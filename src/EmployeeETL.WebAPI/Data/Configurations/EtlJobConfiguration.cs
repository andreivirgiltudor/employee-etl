using EmployeeETL.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeETL.Data.Configurations;

class EtlJobConfiguration : IEntityTypeConfiguration<EtlJob>
{
    public void Configure(EntityTypeBuilder<EtlJob> builder)
    {
    }
}

