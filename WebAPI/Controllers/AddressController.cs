using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IRepository<Address> _addressRepository;

        public AddressController(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet("get")]
        public IEnumerable<AddressDto> Get()
        {
            return _addressRepository.GetAll().Select(a => new AddressDto
            {
                Id = a.Id,
                Street = a.Street,
                City = a.City,
                WarehouseId = a.WarehouseId
            });
        }

        [HttpPost("add")]
        public ObjectResult Add(AddressDto dto)
        {
            _addressRepository.Add(new Address(dto.Street, dto.City, dto.WarehouseId));
            _addressRepository.SaveChanges();

            return Ok("Address added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, AddressDto dto)
        {
            var address = _addressRepository.GetById(id);
            if (address == null)
                return NotFound("Address not found.");

            address.Street = dto.Street;
            address.City = dto.City;
            address.WarehouseId = dto.WarehouseId;

            _addressRepository.SaveChanges();
            return Ok("Address updated successfully.");
        }

        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var address = _addressRepository.GetById(id);
            if (address == null)
                return NotFound("Address not found.");

            _addressRepository.Remove(address);
            _addressRepository.SaveChanges();

            return Ok("Address deleted successfully.");
        }
    }
}
