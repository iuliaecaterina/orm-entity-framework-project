using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IRepository<Warehouse> _warehouseRepository;

        public WarehouseController(IRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [HttpGet("get")]
        public IEnumerable<WarehouseDto> Get()
        {
            return _warehouseRepository.GetAll().Select(w => new WarehouseDto
            {
                Id = w.Id,
                Name = w.Name
            });
        }

        [HttpPost("add")]
        public ObjectResult Add(WarehouseDto dto)
        {
            _warehouseRepository.Add(new Warehouse(dto.Name));
            _warehouseRepository.SaveChanges();

            return Ok("Warehouse added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, WarehouseDto dto)
        {
            var warehouse = _warehouseRepository.GetById(id);
            if (warehouse == null)
                return NotFound("Warehouse not found.");

            warehouse.Name = dto.Name;
            _warehouseRepository.SaveChanges();

            return Ok("Warehouse updated successfully.");
        }

        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var warehouse = _warehouseRepository.GetById(id);
            if (warehouse == null)
                return NotFound("Warehouse not found.");

            _warehouseRepository.Remove(warehouse);
            _warehouseRepository.SaveChanges();

            return Ok("Warehouse deleted successfully.");
        }
    }
}
