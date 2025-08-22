using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Developer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Developer(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public Developer() { }
        public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; }
    }

}
