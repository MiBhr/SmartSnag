using SmartSnag.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSnag.Domain.Entities
{
    public class DefectItem
    {
        
        public int DefectID { get; set; }
        public WorkPackage WorkPackage { get; set; }
        public Company Comnpany { get; set; }
        public DateTime RaisedOn { get; set; }
        public User RaisedBy { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string DefectCategory { get; set; }
        public string DefectStatus { get; set; }
    }
}
