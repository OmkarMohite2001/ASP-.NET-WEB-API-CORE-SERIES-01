using EmployeeManagementAPI.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        public readonly string? _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Employee> GetAll()
        {
            var employees = new List<Employee>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_GetEmployees",connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(
                    new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),

                        FirstName = reader["FirstName"].ToString(),

                        LastName = reader["LastName"].ToString(),

                        Email = reader["Email"].ToString(),

                        Department = reader["Department"].ToString(),

                        Salary = Convert.ToDecimal(reader["Salary"]),

                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),

                        IsActive = Convert.ToBoolean(reader["IsActive"]),

                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });

            }

            return employees;
        }
        public Employee GetById(int id)
        {
            Employee employee = null;

            using var connection =
                new SqlConnection(_connectionString);

            using var command =
                new SqlCommand("usp_GetEmployeesById", connection);

            command.CommandType =
                System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@EmployeeId",
                id
            );

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = new Employee
                {
                    EmployeeId =
                        Convert.ToInt32(reader["EmployeeId"]),

                    FirstName =
                        reader["FirstName"].ToString(),

                    LastName =
                        reader["LastName"].ToString(),

                    Email =
                        reader["Email"].ToString(),

                    Department =
                        reader["Department"].ToString(),

                    Salary =
                        Convert.ToDecimal(reader["Salary"]),

                    JoiningDate =
                        Convert.ToDateTime(reader["JoiningDate"]),

                    IsActive =
                        Convert.ToBoolean(reader["IsActive"]),

                    CreatedDate =
                        Convert.ToDateTime(reader["CreatedDate"])
                };
            }

            return employee;
        }

        public int Create(Employee employee)
        {
            using var connection =
                new SqlConnection(_connectionString);

            using var command =
                new SqlCommand(
                    "usp_CreateEmployee",
                    connection);

            command.CommandType =
                System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@FirstName",
                employee.FirstName
            );

            command.Parameters.AddWithValue(
                "@LastName",
                employee.LastName
            );

            command.Parameters.AddWithValue(
                "@Email",
                employee.Email
            );

            command.Parameters.AddWithValue(
                "@Department",
                employee.Department
            );

            command.Parameters.AddWithValue(
                "@Salary",
                employee.Salary
            );

            command.Parameters.AddWithValue(
                "@JoiningDate",
                employee.JoiningDate
            );

            connection.Open();

            var result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(Employee employee)
        {
            using var connection =
                new SqlConnection(_connectionString);

            using var command =
                new SqlCommand(
                    "usp_UpdateEmployee",
                    connection);

            command.CommandType =
                System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@EmployeeId",
                employee.EmployeeId);

            command.Parameters.AddWithValue(
                "@FirstName",
                employee.FirstName);

            command.Parameters.AddWithValue(
                "@LastName",
                employee.LastName);

            command.Parameters.AddWithValue(
                "@Email",
                employee.Email);

            command.Parameters.AddWithValue(
                "@Department",
                employee.Department);

            command.Parameters.AddWithValue(
                "@Salary",
                employee.Salary);

            command.Parameters.AddWithValue(
                "@JoiningDate",
                employee.JoiningDate);

            command.Parameters.AddWithValue(
                "@IsActive",
                employee.IsActive);

            connection.Open();

            int rowsAffected =
                command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool Delete(int id)
        {
            using var connection =
                new SqlConnection(_connectionString);

            using var command =
                new SqlCommand(
                    "usp_DeleteEmployee",
                    connection);

            command.CommandType =
                System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@EmployeeId",
                id);

            connection.Open();

            int rowsAffected =
                command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}
