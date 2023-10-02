using EmployeeETL.Data.Configurations;
using EmployeeETL.ETL;
using Microsoft.EntityFrameworkCore;

namespace EmployeeETL.Data;

public class EmployeeHRContext : DbContext
{
    public DbSet<EmployeeRecord> Employee => Set<EmployeeRecord>();
    public EmployeeHRContext(DbContextOptions<EmployeeHRContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // We shouldn't create database starting from configuration as this might override HR system database.
        // We'll create HR system database with EF migrations just to simulate that we have a working database.
        base.OnModelCreating(modelBuilder);
        new EmployeeRecordConfiguration().Configure(modelBuilder.Entity<EmployeeRecord>());
    }
}