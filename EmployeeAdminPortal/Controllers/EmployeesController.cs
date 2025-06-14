using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    //Localhost:5000/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult getAllEmployees()
        {
            var allEmployees = _dbContext.Employees.ToList();

            if (allEmployees == null || !allEmployees.Any())
            {
                return NotFound("No employees found.");
            }

            return Ok(allEmployees);

        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();
            return Ok("Employee save with successfully");
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, AddEmployeeDto addEmployeeDto)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            employee.Name = addEmployeeDto.Name;
            employee.Email = addEmployeeDto.Email;
            employee.Phone = addEmployeeDto.Phone;
            employee.Salary = addEmployeeDto.Salary;
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return Ok("Employee updated successfully");
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok("Employee deleted successfully");
        }
    }
}
