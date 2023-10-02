using EmployeeETL.ETL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeETL.Data.Configurations;

class EmployeeRecordConfiguration : IEntityTypeConfiguration<EmployeeRecord>
{
    public void Configure(EntityTypeBuilder<EmployeeRecord> builder)
    {
        builder.HasKey(x => x.EmployeeID);
        builder.Property(x => x.AnnualIncome).HasPrecision(10, 2);
    }
}

