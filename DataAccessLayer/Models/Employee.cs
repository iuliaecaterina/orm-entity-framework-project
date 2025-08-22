using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }


        public Employee() { }

        public Employee(string fullName, Guid companyId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CompanyId = companyId;
        }
    }
}
