using aspnetcore_entityframework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_entityframework.Controllers
{
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
        public IActionResult GetAllEmployees()
        {
            var allEmployees = _dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")] //set the route to accept id
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
                return NotFound();
            else
                return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeDto employeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary
            };
            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
                return NotFound();
            else
            {
                employee.Name = employeeDto.Name;
                employee.Email = employeeDto.Email;
                employee.Phone = employeeDto.Phone;
                employee.Salary = employeeDto.Salary;
                _dbContext.SaveChanges();
                return Ok(employee);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
                return NotFound();
            else
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return Ok(employee);
            }
        }
    }
}
