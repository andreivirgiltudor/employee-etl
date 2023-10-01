using EmployeeETL.Data.Configurations;
using EmployeeETL.ETL;
using Microsoft.EntityFrameworkCore;

namespace EmployeeETL.Data;

public class EmployeeETLContext : DbContext
{
    public DbSet<EtlJob> EtlJobs => Set<EtlJob>();

    public EmployeeETLContext(DbContextOptions<EmployeeETLContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new EtlJobConfiguration().Configure(modelBuilder.Entity<EtlJob>());

    }
}

