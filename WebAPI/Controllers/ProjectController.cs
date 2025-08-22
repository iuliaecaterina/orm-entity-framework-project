using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IRepository<Project> _projectRepository;

        public ProjectController(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet("get")]
        public IEnumerable<ProjectDto> Get()
        {
            return _projectRepository.GetAll().Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title
            });
        }

        [HttpPost("add")]
        public ObjectResult Add(ProjectDto dto)
        {
            _projectRepository.Add(new Project(dto.Title));
            _projectRepository.SaveChanges();

            return Ok("Project added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, ProjectDto dto)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
                return NotFound("Project not found.");

            project.Title = dto.Title;
            _projectRepository.SaveChanges();

            return Ok("Project updated successfully.");
        }

        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
                return NotFound("Project not found.");

            _projectRepository.Remove(project);
            _projectRepository.SaveChanges();

            return Ok("Project deleted successfully.");
        }
    }
}
