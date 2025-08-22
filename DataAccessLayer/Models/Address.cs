using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public Address(string street, string city, Guid warehouseId)
        {
            Id = Guid.NewGuid();
            Street = street;
            City = city;
            WarehouseId = warehouseId;
        }
        public Address() { }
    }
}
