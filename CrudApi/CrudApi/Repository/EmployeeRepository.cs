using CrudApi.Model;

namespace CrudApi.Repository
{
    public class EmployeeRepository
    {
        private List<Employee> employees = new List<Employee>()
        {
            new Employee
            {
                Id = 1,
                Name = "Omkar",
                Department = "IT",
                Salary = 35000
            },
            new Employee {
                Id = 2,
                Name = "Rahul",
                Department ="HR",
                Salary = 50000
            }
        };
        public List<Employee> GetAll()
        {
            return employees;
        }
        public void Add(Employee employee)
        {
            employees.Add(employee);
        }
        public Employee? GetById(int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }
        public bool Update(int id, Employee employee)
        {
            var existingEmployee = employees.FirstOrDefault(x=>x.Id == id);
            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Salary = employee.Salary;

            return true;
        }
        public bool Delete(int id)
        {
            var employee = employees.FirstOrDefault(x=>x.Id==id);
            if (employee == null)
            {
                return false;
            }
            employees.Remove(employee);
            return true;
        }
        public bool UpdateSalary(int id, Decimal salary)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return false;
            }
            employee.Salary = salary;
            return true;
        }
    }
}
