using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Company() { }

        public Company(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
