using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IRepository<Developer> _developerRepository;

        public DeveloperController(IRepository<Developer> developerRepository)
        {
            _developerRepository = developerRepository;
        }

        [HttpGet("get")]
        public IEnumerable<DeveloperDto> Get()
        {
            return _developerRepository.GetAll().Select(d => new DeveloperDto
            {
                Id = d.Id,
                Name = d.Name
            });
        }
        [HttpPost("add")]
        public ObjectResult Add(DeveloperDto dto)
        {
            _developerRepository.Add(new Developer(dto.Name));
            _developerRepository.SaveChanges();

            return Ok("Developer added successfully.");
        }



        [HttpPut("update")]
        public ObjectResult Update(Guid id, DeveloperDto dto)
        {
            var dev = _developerRepository.GetById(id);
            if (dev == null)
                return NotFound("Developer not found.");

            dev.Name = dto.Name;
            _developerRepository.SaveChanges();

            return Ok("Developer updated successfully.");
        }

        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
