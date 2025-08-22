namespace WebAPI.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid CompanyId { get; set; }
    }
}
