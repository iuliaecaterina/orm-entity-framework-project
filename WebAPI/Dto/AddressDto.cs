namespace WebAPI.Dto
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
