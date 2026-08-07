using CrudApi.Model;
using CrudApi.Repository;

namespace CrudApi.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;
        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }
        public List<Employee> GetEmployees()
        {
            return _repository.GetAll();
        }
        public void AddEmployee(Employee employee)
        {
            _repository.Add(employee);
        }
        public Employee? GetEmployeeById(int id) 
        {
            return _repository.GetById(id);
        }
        public bool UpdateEmployee(int id,Employee employee)
        {
            return _repository.Update(id, employee);
        }
        public bool DeleteEmployee(int id)
        {
            return _repository.Delete(id);
        }
        public bool UpdateSalary(int id,decimal salary)
        {
            return _repository.UpdateSalary(id,salary);
        }
    }
}
