using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; }


        public Project() { }

        public Project(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }
    }


}
