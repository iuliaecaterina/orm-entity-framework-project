using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("get")]
        public IEnumerable<EmployeeDto> Get()
        {
            return _employeeRepository.GetAll().Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = e.FullName,
                CompanyId = e.CompanyId
            });
        }

        [HttpPost("add")]
        public ObjectResult Add(EmployeeDto dto)
        {
            _employeeRepository.Add(new Employee(dto.FullName, dto.CompanyId));
            _employeeRepository.SaveChanges();

            return Ok("Employee added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, EmployeeDto dto)
        {
            var emp = _employeeRepository.GetById(id);
            if (emp == null)
                return NotFound("Employee not found.");

            emp.FullName = dto.FullName;
            emp.CompanyId = dto.CompanyId;
            _employeeRepository.SaveChanges();

            return Ok("Employee updated successfully.");
        }

        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var emp = _employeeRepository.GetById(id);
            if (emp == null)
                return NotFound("Employee not found.");

            _employeeRepository.Remove(emp);
            _employeeRepository.SaveChanges();

            return Ok("Employee deleted successfully.");
        }
    }
}
