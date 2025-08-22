using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> _companyRepository;

        public CompanyController(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet("get")]
        public IEnumerable<CompanyDto> Get()
        {
            return _companyRepository.GetAll().Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        [HttpPost("add")]
        public ObjectResult Add(CompanyDto dto)
        {
            _companyRepository.Add(new Company(dto.Name));
            _companyRepository.SaveChanges();

            return Ok("Company added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, CompanyDto dto)
        {
            var company = _companyRepository.GetById(id);
            if (company == null)
                return NotFound("Company not found.");

            company.Name = dto.Name;
            _companyRepository.SaveChanges();

            return Ok("Company updated successfully.");
        }

        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var company = _companyRepository.GetById(id);
            if (company == null)
                return NotFound("Company not found.");

            _companyRepository.Remove(company);
            _companyRepository.SaveChanges();

            return Ok("Company deleted successfully.");
        }
    }
}
