using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ProjectDeveloper
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid DeveloperId { get; set; }
        public Developer Developer { get; set; }
    }
}
