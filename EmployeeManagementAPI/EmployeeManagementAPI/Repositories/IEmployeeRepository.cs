using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        int Create(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
    }
}
