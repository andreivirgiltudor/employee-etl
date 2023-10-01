using System.Threading.Tasks;

namespace EmployeeETL.ETL;

public interface IEmployeeLoader
{
    Task LoadAsync(EmployeeRecord employee);
}